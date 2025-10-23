using Manta.Domain.Entities;
using Manta.Domain.Enums;

namespace Manta.Presentation.Controls;

public partial class ShipmentToReturn : UserControl
{
    public Parcel Parcel { get; private set; }
    public delegate void CheckChangedHandler(ShipmentToReturn sender, bool isChecked);
    public event CheckChangedHandler CheckChanged;
    public event Action<Parcel> ShipmentClicked;
    
    private Color _defoultColor { get; init; }

    public ShipmentToReturn(Parcel parcel)
    {
        InitializeComponent();
        Parcel = parcel;
        _defoultColor = BackColor;
        idLabel.Text = Parcel.Id.ToString();
        recipientNameLabel.Text = Parcel.RecipientName.ToString();
        phoneNumberLabel.Text = Parcel.RecipientPhoneNumber.ToString();
        statusLabel.Text = Parcel.CurrentStatus.Status.ToString();

        foreach (Control control in Controls)
        {
            if (control is CheckBox) continue;
            control.Click += (s,e) => ShipmentClicked(Parcel);
            control.MouseEnter += (s,e) => BackColor = Color.White;
            control.MouseLeave += (s,e) => BackColor = _defoultColor;
        }

        check.CheckedChanged += (s, e) =>
        {
            CheckChanged?.Invoke(this, check.Checked);
        };
        if(parcel.CurrentStatus.Status is EParcelStatus.ReturnRequested or EParcelStatus.ReaddressRequested) check.Enabled = false;
    }

    public void SetChecked(bool value)
    {
        check.Checked = value; 
    }

    public bool IsChecked() => check.Checked;

}