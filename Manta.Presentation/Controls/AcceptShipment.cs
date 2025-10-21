using Manta.Domain.Entities;
using Manta.Domain.Enums;

namespace Manta.Presentation.Controls;

public partial class AcceptShipment : UserControl
{
    public AcceptShipment(Parcel parcel)
    {
        InitializeComponent();
        idLabel.Text = parcel.Id.ToString();
        recipientNameLabel.Text = parcel.RecipientName.ToString();
        statusLabel.Text = parcel.CurrentStatus.Status.ToString();
        backPanel.BackColor = parcel.CurrentStatus.Status switch
        {
            EParcelStatus.ReadyForPickup => System.Drawing.Color.FromArgb(((int)((byte)221)), ((int)((byte)238)), ((int)((byte)200))),
            EParcelStatus.ShipmentCancelled => System.Drawing.Color.FromArgb(((int)((byte)255)), ((int)((byte)129)), ((int)((byte)91))),
            EParcelStatus.WrongLocation => System.Drawing.Color.FromArgb(((int)((byte)252)), ((int)((byte)191)), ((int)((byte)73))),
        };
    }
}