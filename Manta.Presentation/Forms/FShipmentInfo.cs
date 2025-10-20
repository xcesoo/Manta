using Manta.Application.Services;
using Manta.Domain.Entities;
using Manta.Infrastructure.Repositories;
using Manta.Presentation.Controls;

namespace Manta.Presentation.Forms;

public partial class FShipmentInfo : Form
{
    private ParcelDeliveryService _deliveryService;
    public FShipmentInfo(Parcel parcel, ParcelDeliveryService deliveryService, IParcelRepository parcelRepository)
    {
        InitializeComponent();
        var shipmentInfo = new ShipmentInfo(parcel, deliveryService, parcelRepository);
        flowInfoPanel.Controls.Add(shipmentInfo);
    }
}