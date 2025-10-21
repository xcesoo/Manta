using Manta.Application.Services;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Infrastructure.Repositories;
using Manta.Presentation.Services;
using Manta.Presentation.State;

namespace Manta.Presentation.Controls;

public partial class ShipmentInfo : UserControl
{
    private Parcel Parcel;
    private ParcelDeliveryService _deliveryService;
    private IParcelRepository _parcelRepository;
    public ShipmentInfo(Parcel parcel, ParcelDeliveryService deliveryService, IParcelRepository parcelRepository)
    {
        InitializeComponent();
        Parcel = parcel;
        _deliveryService = deliveryService;
        _parcelRepository = parcelRepository;
        ShipmentInfo_Load();
        ChangeStatusService.OnStatusChanged += async () => await Reload();
    }

    private void ShipmentInfo_Load()
    {
        id.Text = $"Номер замовлення - {Parcel.Id}";
        weight.Text = $"Вага - {Parcel.Weight} кг.";
        storage.Text = Parcel.Storage is DateTime storageDate
            ? $"Зберігання до - {storageDate.ToLocalTime():dd.MM.yyyy}"
            : "";        currentStatus.Text = $"{Parcel.CurrentStatus.Status}";
        createdAt.Text = $"Створена - {Parcel.StatusHistory.First(s => s.Status == EParcelStatus.Processing).ChangedAt.ToLocalTime()}";
        recipientPhoneNumber.Text = $"{Parcel.RecipientPhoneNumber}";
        recipientName.Text = $"{Parcel.RecipientName}";
        foreach (var status in Parcel.StatusHistory) statusHistoryFlowPanel.Controls.Add(new StatusHistory(status));
        amoutDue.Text = $"Сума оплати - {Parcel.AmountDue}";
        paid.Text = Parcel.Paid ? "Оплата - сплачено" : "Оплата - очікується";
        senderName.Text =
            $"Імʼя відправника - {Parcel.StatusHistory.First(s => s.Status == EParcelStatus.Processing).ChangedBy.Name}";
        senderEmail.Text = 
            $"Email відправника - {Parcel.StatusHistory.First(s => s.Status == EParcelStatus.Processing).ChangedBy.Email}";
        cashDeskBtn.Enabled = Parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup;
        shipmentCancel.Enabled = Parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup;
    }

    private void cashDeskBtn_Click(object sender, EventArgs e)
    {
        CashDeskManager.Add(Parcel);
    }

    private async void shipmentCancel_Click(object sender, EventArgs e)
    {
        if (State.AppContext.CurrentUser == null)
        {
            MessageBox.Show("Виберіть аккаунт в налаштуваннях", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        await _deliveryService.CancelParcel(Parcel.Id, State.AppContext.CurrentUser);
        await Reload(); 
        ChangeStatusService.ShipmentChangedStatus();
    }

    private async Task Reload()
    {
        Parcel = await _parcelRepository.GetByIdAsync(Parcel.Id);
        ShipmentInfo_Load();
    }
}