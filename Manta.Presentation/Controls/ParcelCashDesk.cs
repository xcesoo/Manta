using Manta.Domain.Entities;

namespace Manta.Presentation.Controls;

public partial class ParcelCashDesk : UserControl
{
    private Parcel _parcel;
    public ParcelCashDesk(Parcel parcel)
    {
        InitializeComponent();
        _parcel = parcel;
        parcelIdLabel.Text += _parcel.Id;
        toPayLabel.Text += _parcel.AmountDue;
    }

    private void deleteBtn_Click(object sender, EventArgs e)
    {
        CashDesk.RemoveParcel(_parcel);
    }
}