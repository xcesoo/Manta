    using manta.Domain.Entities;
    using manta.Domain.Enums;
    using manta.Domain.Interfaces;
    using manta.Domain.StatusRules;
    using System.Collections.Generic;

    namespace manta.Domain.Services;

    public class ParcelStatusService
    {
        private readonly List<IParcelStatusRule> _rules = RuleLoader.LoadAllRules;

        public void UpdateStatus(Parcel parcel, User changedBy, DeliveryPoint deliveryPoint)
        {
            if (parcel == null) throw new ArgumentNullException(nameof(parcel));
            if (deliveryPoint == null) throw new ArgumentNullException(nameof(deliveryPoint));

            foreach (var rule in _rules)
            {
                if(rule.ShouldApply(parcel, deliveryPoint, out EParcelStatus newStatus)
                   && newStatus != parcel.CurrentStatus.Status)
                {
                    parcel.ChangeStatus(newStatus, changedBy ?? SystemUser.Instance);
                    break;
                }
            }
        }

        public void ApplyRule<T>(Parcel parcel, User changedBy, DeliveryPoint deliveryPoint) 
            where T : IParcelStatusRule
        {
            var rule = _rules.OfType<T>().FirstOrDefault();
            if (rule == null) throw new Exception($"Rule {typeof(T).Name} does not exist");
            if (rule.ShouldApply(parcel, deliveryPoint, out EParcelStatus newStatus)
                && newStatus != parcel.CurrentStatus.Status)
            {
                parcel.ChangeStatus(newStatus, changedBy ?? SystemUser.Instance);
            }
        }
    }