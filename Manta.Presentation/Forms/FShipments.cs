using Manta.Domain.Entities;



using Manta.Presentation.Controls;
using Manta.Presentation.Services;
using Manta.Presentation.State;
using AppContext = Manta.Presentation.State.AppContext;

namespace Manta.Presentation.Forms;

public partial class FShipments : Form
{
    private readonly ParcelSearchService _searchService;
    private readonly Func<Parcel, bool> _filter;
    public event Action<Parcel> ShipmentOpenRequested;
    public FShipments(ParcelSearchService parcelSearchService, Func<Parcel, bool>? filter = null)
    {
        _searchService = parcelSearchService;
        _filter = filter ?? (_ => true);
        InitializeComponent();
        
        AppContext.DeliveryPointChangedEvent += async () => await LoadDataAsync(searchTextBox.Text);
        AppContext.ShipmentChangedEvent += async () => await LoadDataAsync(searchTextBox.Text);
        ChangeStatusService.OnStatusChanged += async () => await LoadDataAsync(searchTextBox.Text);
        LoadDataAsync();
    }
    private async Task LoadDataAsync(string? search = null)
    {
        flowDataPanel.Controls.Clear();
        if (AppContext.CurrentDeliveryPointId == null)
        {
            flowDataPanel.Controls.Add(new Label { Text = "Оберіть відділення в налаштуваннях", AutoSize = true });
            return;
        }
        var parcels = await _searchService.GetParcelsAsync((int)AppContext.CurrentDeliveryPointId, search);
        parcels = parcels.Where(_filter);
        if (!parcels.Any())
        {
            flowDataPanel.Controls.Add(new Label { Text = "Посилки не знайдені", AutoSize = true });
            return;
        }
        parcels = parcels.OrderByDescending(p => p.CurrentStatus.ChangedAt).ToList();
        foreach (var parcel in parcels)
        {
            var shipment = new Shipment(parcel);
            flowDataPanel.Controls.Add(shipment);
            shipment.ShipmentClicked += parcel => ShipmentOpenRequested?.Invoke(parcel);
        }
    }

    private async void searchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
                await LoadDataAsync(searchTextBox.Text);
    }
    
}