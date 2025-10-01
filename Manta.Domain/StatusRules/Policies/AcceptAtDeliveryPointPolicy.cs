using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Implementations;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Policies;

public sealed class AcceptAtDeliveryPointPolicy : IParcelStatusRule
{
    private readonly WrongDeliveryPointRule _wrong = new ();
    private readonly ReturnRequestedRule _cancelToReturn = new ();
    private readonly ReadyForPickupRule _ready = new ();
    public RuleResult ShouldApply(RuleContext context)
    {
        if (context.DeliveryPoint is null)
            return RuleResult.Failed(ERuleResultError.ArgumentInvalid, "DeliveryPoint is required for acceptance policy.");

        var wrongDeliveryPoint = _wrong.ShouldApply(context);
        if (wrongDeliveryPoint.IsOk) return wrongDeliveryPoint;

        var canceledToReturnRequested = _cancelToReturn.ShouldApply(context);
        if (canceledToReturnRequested.IsOk) return canceledToReturnRequested;

        return _ready.ShouldApply(context);
    }
}