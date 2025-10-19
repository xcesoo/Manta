using Manta.Application.Services;
using Manta.Presentation.Controls;

namespace Manta.Presentation.Forms;

public partial class FCashDesk : Form
{
    ParcelDeliveryService _deliveryService;
    public FCashDesk(ParcelDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
        InitializeComponent();
        CashDesk.ParcelsChanged += CashDesk_ParcelsChanged;
    }

    private void CashDesk_ParcelsChanged()
    {
        parcelsFlowPanel.Controls.Clear();
        foreach (var parcel in CashDesk.Parcels)
        {
            parcelsFlowPanel.Controls.Add(new ParcelCashDesk(parcel));
        }

        toPayLabel.Text = $"До сплати: {CashDesk.Parcels.Sum(x => x.AmountDue)}";
    }

    private void clearBtn_Click(object sender, EventArgs e)
    {
        CashDesk.Clear();
    }

    private async void payBtn_Click(object sender, EventArgs e)
    {
        if (CashDesk.Parcels.Count == 0) return;
        if (Globals.CurrentUser == null)
        {
            MessageBox.Show("Виберіть аккаунт в налаштуваннях", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        foreach (var parcel in CashDesk.Parcels)
        {
            await _deliveryService.PayForParcel(parcel.Id);
            await _deliveryService.DeliverParcel(parcel.Id, Globals.CurrentUser);
        }
        CashDesk.Clear();
        CashDesk.OnDeliveryCompleted();
    }
}