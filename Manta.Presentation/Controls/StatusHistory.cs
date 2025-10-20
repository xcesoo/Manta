using Manta.Domain.ValueObjects;

namespace Manta.Presentation.Controls;

public partial class StatusHistory : UserControl
{
    private ParcelStatus _parcelStatus;
    public StatusHistory(ParcelStatus parcelStatus)
    {
        InitializeComponent();
        _parcelStatus = parcelStatus;
        status.Text = $"{parcelStatus.Status}";
        changedById.Text = $"{parcelStatus.ChangedBy.Id}";
        changedByEmail.Text = $"{parcelStatus.ChangedBy.Email}";
        changedByName.Text = $"{parcelStatus.ChangedBy.Name}";
        changedAt.Text = $"{parcelStatus.ChangedAt.ToLocalTime()}"; 
        changedByRole.Text = $"{parcelStatus.ChangedBy.Role}";
    }
}