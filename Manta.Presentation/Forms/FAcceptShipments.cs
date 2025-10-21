using Manta.Application.Services;
using Manta.Domain.Entities;
using Manta.Infrastructure.Repositories;
using Manta.Presentation.Controls;
using Manta.Presentation.Services;
using Microsoft.IdentityModel.Tokens;

namespace Manta.Presentation.Forms;

public partial class FAcceptShipments : Form
{
    private ParcelDeliveryService _deliveryService;
    private IParcelRepository _parcelRepository;
    public FAcceptShipments(ParcelDeliveryService deliveryService, IParcelRepository parcelRepository)
    {
        InitializeComponent();
        _deliveryService = deliveryService;
        _parcelRepository = parcelRepository;
    }

    private async Task AcceptParcel()
    {
        if (acceptTextBox.Text.IsNullOrEmpty())
        {
            MessageBox.Show("Введіть id посилки", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (State.AppContext.CurrentUser == null)
        {
            MessageBox.Show("Виберіть аккаунт в налаштуваннях", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (State.AppContext.CurrentDeliveryPointId == null)
        {
            MessageBox.Show("Оберіть відділення в налаштуваннях", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            await _deliveryService.AcceptedAtDeliveryPoint((int)State.AppContext.CurrentDeliveryPointId!,
                Convert.ToInt32(acceptTextBox.Text), State.AppContext.CurrentUser);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        var parcel = await _parcelRepository.GetByIdAsync(Convert.ToInt32(acceptTextBox.Text));
        acceptedFlowPanel.Controls.Add(new AcceptShipment(parcel));
        State.AppContext.ShipmentChanged();
    }

    private async void acceptTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            await AcceptParcel();
        }
    }

    private void acceptTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }
}