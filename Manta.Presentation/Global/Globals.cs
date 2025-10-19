using Manta.Domain.Entities;

namespace Manta.Presentation;

public static class Globals
{
    public static User? CurrentUser { get; set; } = null;
    public static int? CurrentDeliveryPointId { get; set; } = null;
}