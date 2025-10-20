using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Presentation.Services;

namespace Manta.Presentation.Controls;

public partial class Shipment : UserControl
{
    public Parcel Parcel;
    public event Action<Parcel> ShipmentClicked;

    private Color _defaultColor { get; init; }
    public Shipment()
    {
        InitializeComponent();
        cashdeskBtn.Enabled = false;
        cashdeskBtn.Visible = false;
    }
    public Shipment(Parcel parcel)
    {
        InitializeComponent();
        // this.MouseEnter += Shipment_MouseEnter;
        // this.MouseLeave += Shipment_MouseLeave;
        foreach (Control control in Controls)
        {
            if (control is Button) continue;
            control.Click += Shipment_Click;
            control.MouseEnter += Shipment_MouseEnter;
            control.MouseLeave += Shipment_MouseLeave;
        }
        Parcel = parcel;
        
        _defaultColor = BackColor;
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

    private void Shipment_Click(object sender, EventArgs e)
    {
        ShipmentClicked?.Invoke(Parcel);
    }

    private void Shipment_MouseEnter(object sender, EventArgs e) => BackColor = Color.White;

    private void Shipment_MouseLeave(object sender, EventArgs e) => BackColor = _defaultColor;
}