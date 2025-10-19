using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Presentation.Services;

namespace Manta.Presentation.Controls;

public partial class Shipment : UserControl
{
    public Parcel Parcel;
    public Shipment()
    {
        InitializeComponent();
        cashdeskBtn.Enabled = false;
        cashdeskBtn.Visible = false;
    }
    public Shipment(Parcel parcel)
    {
        InitializeComponent();
        Parcel = parcel;
        
        idLabel.Text = Parcel.Id.ToString();
        recipientNameLabel.Text = Parcel.RecipientName.ToString();
        phoneNumberLabel.Text = Parcel.RecipientPhoneNumber.ToString();
        statusLabel.Text = Parcel.CurrentStatus.Status.ToString();
        amountDueLabel.Text = Parcel.Paid
            ? $"{Parcel.AmountDue} - сплачено"
            : Parcel.AmountDue.ToString();
        if (Parcel.Paid)
            amountDueLabel.ForeColor = Color.DimGray;
        
        cashdeskBtn.Enabled = Parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup;
    }

    private void cashdeskBtn_Click(object sender, EventArgs e)
    {
        CashDeskManager.Add(Parcel);
    }
}