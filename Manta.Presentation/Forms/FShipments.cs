using Manta.Domain.Entities;
using Manta.Infrastructure.Repositories;
using Manta.Presentation.Controls;

namespace Manta.Presentation.Forms;

public partial class FShipments : Form
{
    private IParcelRepository _parcelRepository;
    public FShipments(IParcelRepository parcelRepository)
    {
        _parcelRepository = parcelRepository;
        InitializeComponent();
        CashDesk.DeliveryCompleted += async () => await LoadDataAsync(searchTextBox.Text);
        Globals.DeliveryPointChangedEvent += async () => await LoadDataAsync(null);
    }
    private async Task LoadDataAsync(string search = null)
    {
        flowDataPanel.Controls.Clear();
        IEnumerable<Parcel> parcels;
        if (string.IsNullOrEmpty(search)) parcels = await _parcelRepository.GetByDeliveryPointIdAsync((int)Globals.CurrentDeliveryPointId!);
        else parcels = await _parcelRepository.GetByRecipientPhoneAsync(search);
        if (!parcels.Any())
        {
            flowDataPanel.Controls.Add(new Label { Text = "Посилки не знайдені", AutoSize = true });
            return;
        }
        foreach (var parcel in parcels)
        {
            Shipment shipment = new Shipment(parcel);
            flowDataPanel.Controls.Add(shipment);
        }
    }

    private async void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (Globals.CurrentDeliveryPointId != null)
            {
                await LoadDataAsync(searchTextBox.Text);
            }
            else
            {
                MessageBox.Show("Оберіть відділення в налаштуваннях", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}