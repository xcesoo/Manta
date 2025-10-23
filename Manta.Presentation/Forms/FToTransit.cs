using Manta.Application.Services;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;
using Manta.Infrastructure.Repositories;
using Manta.Presentation.Controls;
using Microsoft.IdentityModel.Tokens;

namespace Manta.Presentation.Forms;

public partial class FToTransit : Form
{
    private IParcelRepository _parcelRepository;
    private ParcelDeliveryService _deliveryService;
    private IDeliveryVehicleRepository _deliveryVehicleRepository;
    private List<Parcel> _toTransitParcles = new();
    private List<Parcel> _selectedParcels = new();
    private List<ShipmentToReturn> _parcelsControls = new();
    private DeliveryVehicle? _selectedVehicle;
    public FToTransit(IParcelRepository parcelRepository, ParcelDeliveryService deliveryService, IDeliveryVehicleRepository deliveryVehicleRepository)
    {
        InitializeComponent();
        _parcelRepository = parcelRepository;
        _deliveryService = deliveryService;
        _deliveryVehicleRepository = deliveryVehicleRepository;
    }

    private async Task LoadDataAsync(string? search = null)
    {
        flowDataPanel.Controls.Clear();
        _toTransitParcles.Clear();
        _selectedParcels.Clear();
        if (State.AppContext.CurrentDeliveryPointId == null)
        {
            flowDataPanel.Controls.Add(new Label() {Text = "Оберіть відділення в налаштуваннях", AutoSize = true});
            return;
        }
        

        if (vehicleSearch.Text.IsNullOrEmpty()) return;
        var vehicle = await _deliveryVehicleRepository.GetByIdAsync(search);
        if (vehicle == null)
        {
            MessageBox.Show("Автомобіль не зареєстрований в системі", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        _selectedVehicle = vehicle;
        var parcels = await _parcelRepository.GetByDeliveryPointIdAsync((int)State.AppContext.CurrentDeliveryPointId!);
        _toTransitParcles = parcels.Where(p => p.CurrentStatus.Status is EParcelStatus.ReaddressRequested or EParcelStatus.ReturnRequested)
            .Where(p => p.CurrentLocationDeliveryPointId ==  State.AppContext.CurrentDeliveryPointId!)
            .ToList();
        selectAll.CheckedChanged += HeaderSelectAll;
        foreach (var parcel in _toTransitParcles)
        {
            var shipmentControl = new ShipmentToReturn(parcel);
            shipmentControl.CheckChanged += ShipmentControlCheckChanged;
            flowDataPanel.Controls.Add(shipmentControl);
            _parcelsControls.Add(shipmentControl);
        }
    }

    private async void vehicleSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            await LoadDataAsync(vehicleSearch.Text);
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

    private async void toTransit_Click(object sender, EventArgs e)
    {
        if (State.AppContext.CurrentUser == null)
        {
            MessageBox.Show("Виберіть аккаунт в налаштуваннях", "MantaException", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        foreach (var parcel in _selectedParcels)
        {
            await _deliveryService.LoadInDeliveryVehicle(_selectedVehicle.Id, parcel.Id, State.AppContext.CurrentUser);
        }
        await LoadDataAsync(vehicleSearch.Text);
    }
}