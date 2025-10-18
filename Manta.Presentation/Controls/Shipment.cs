using Manta.Domain.Entities;

namespace Manta.Presentation.Controls;

public partial class Shipment : UserControl
{
    public Shipment(Parcel parcel)
    {
        InitializeComponent();
        idLabel.Text = parcel.Id.ToString();
        recipientNameLabel.Text = parcel.RecipientName.ToString();
        phoneNumberLabel.Text = parcel.RecipientPhoneNumber.ToString();
        amountDueLabel.Text = parcel.AmountDue.ToString();
    }
    
}