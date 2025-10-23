using Manta.Application.Services;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Infrastructure.Repositories;
using Manta.Presentation.Controls;

namespace Manta.Presentation.Forms;

public partial class FToReturn : Form
{
    private IParcelRepository _parcelRepository;
    private ParcelDeliveryService _deliveryService;
    private List<Parcel> _selectedParcels = new();
    private List<ShipmentToReturn> _parcelsControls = new();
    public Action<Parcel>? ShipmentOpenRequested;
    public FToReturn(IParcelRepository parcelRepository, ParcelDeliveryService parcelDeliveryService)
    {
        InitializeComponent();
        _parcelRepository = parcelRepository;
        _deliveryService = parcelDeliveryService;
        LoadParcels();
    }

    private async Task LoadParcels()
    {
        flowDataPanel.Controls.Clear();
        _selectedParcels.Clear();
        _parcelsControls.Clear();
        if (State.AppContext.CurrentDeliveryPointId == null)
        {
            flowDataPanel.Controls.Add(new Label() {Text = "Оберіть відділення в налаштуваннях", AutoSize = true});
            return;
        }
        var parcels = await _parcelRepository.GetByDeliveryPointIdAsync((int)State.AppContext.CurrentDeliveryPointId!);
        parcels = parcels.Where(p => p.CurrentStatus.Status is EParcelStatus.WrongLocation
            or EParcelStatus.ShipmentCancelled or EParcelStatus.ReaddressRequested or EParcelStatus.StorageExpired or EParcelStatus.ReturnRequested)
            .ToList();
        selectAll.CheckedChanged += HeaderSelectAll;

        foreach (var parcel in parcels)
        {
            var shipmentControl = new ShipmentToReturn(parcel);
            shipmentControl.CheckChanged += ShipmentControlCheckChanged;
            flowDataPanel.Controls.Add(shipmentControl);
            if(parcel.CurrentStatus.Status != EParcelStatus.ReaddressRequested && parcel.CurrentStatus.Status != EParcelStatus.ReturnRequested) _parcelsControls.Add(shipmentControl);
            shipmentControl.ShipmentClicked += (p) => ShipmentOpenRequested?.Invoke(p);
        }
    }

    private void HeaderSelectAll(object sender, EventArgs e)
    {
        foreach (var control in _parcelsControls)
        {
            control.SetChecked(selectAll.Checked);
        }
        _selectedParcels.Clear();
        if (selectAll.Checked)
        {
            _selectedParcels.AddRange(_parcelsControls.Select(x => x.Parcel));
        }
    }
    private void ShipmentControlCheckChanged(object sender, bool isChecked)
    {
        if (sender is ShipmentToReturn shipment)
        {
            if (isChecked) _selectedParcels.Add(shipment.Parcel);
            else _selectedParcels.Remove(shipment.Parcel);
        }
    }

    private async void returnRequest_Click(object sender, EventArgs e)
    {
        if (State.AppContext.CurrentUser == null)
        {
            MessageBox.Show("Виберіть аккаунт в налаштуваннях", "MantaException", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }
        foreach (var parcel in _selectedParcels)
        {
            if (parcel.CurrentStatus.Status is EParcelStatus.WrongLocation)
            {
                await _deliveryService.ReaddressParcel(parcel.Id, State.AppContext.CurrentUser);
                continue;           
            }
            await _deliveryService.ReturnRequestParcels(State.AppContext.CurrentUser, parcel.Id);
        }
        selectAll.Checked = false;
        LoadParcels();
    }
}