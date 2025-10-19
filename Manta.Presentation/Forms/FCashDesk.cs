using Manta.Application.Services;
using Manta.Presentation.Controls;
using Manta.Presentation.Services;
using Manta.Presentation.State;
using AppContext = Manta.Presentation.State.AppContext;

namespace Manta.Presentation.Forms;

public partial class FCashDesk : Form
{
    ParcelDeliveryService _deliveryService;
    public FCashDesk(ParcelDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
        InitializeComponent();
        CashDeskManager.ParcelsChanged += CashDesk_ParcelsChanged;
    }

    private void CashDesk_ParcelsChanged()
    {
        parcelsFlowPanel.Controls.Clear();
        foreach (var parcel in CashDeskManager.Parcels)
        {
            parcelsFlowPanel.Controls.Add(new ParcelCashDesk(parcel));
        }

        toPayLabel.Text = $"До сплати: {CashDeskManager.Parcels.Sum(x => x.AmountDue)}";
    }

    private void clearBtn_Click(object sender, EventArgs e)
    {
        CashDeskManager.Clear();
    }

    private async void payBtn_Click(object sender, EventArgs e)
    {
        if (CashDeskManager.Parcels.Count == 0) return;
        if (AppContext.CurrentUser == null)
        {
            MessageBox.Show("Виберіть аккаунт в налаштуваннях", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        foreach (var parcel in CashDeskManager.Parcels)
        {
            await _deliveryService.PayForParcel(parcel.Id);
            await _deliveryService.DeliverParcel(parcel.Id, AppContext.CurrentUser);
        }
        CashDeskManager.Clear();
        CashDeskManager.Complete();
    }
}