using Manta.Domain.Entities;
using Manta.Domain.Enums;

namespace Manta.Presentation.Controls;

public partial class Shipment : UserControl
{
    private Parcel _parcel;
    public Shipment()
    {
        InitializeComponent();
        cashdeskBtn.Enabled = false;
        cashdeskBtn.Visible = false;
    }
    public Shipment(Parcel parcel)
    {
        InitializeComponent();
        _parcel = parcel;
        idLabel.Text = _parcel.Id.ToString();
        recipientNameLabel.Text = _parcel.RecipientName.ToString();
        phoneNumberLabel.Text = _parcel.RecipientPhoneNumber.ToString();
        statusLabel.Text = _parcel.CurrentStatus.Status.ToString();
        if (_parcel.CurrentStatus.Status != EParcelStatus.ReadyForPickup)
        {
            cashdeskBtn.Enabled = false;
        }
        if (_parcel.Paid)
        {
            amountDueLabel.Text = $"{_parcel.AmountDue} - сплачено";
            amountDueLabel.ForeColor = Color.DimGray;
        }
        else
        {
            amountDueLabel.Text = _parcel.AmountDue.ToString();
        }
    }

    private void cashdeskBtn_Click(object sender, EventArgs e)
    {
        CashDesk.AddParcel(_parcel);
    }
}