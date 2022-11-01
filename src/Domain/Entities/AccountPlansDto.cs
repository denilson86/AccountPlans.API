using System;

namespace Domain.Entities
{
    public class AccountPlansDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public TypePlans TypePlan { get; set; }
        public bool AcceptLauch { get; set; }

        public AccountPlansDto(string code, string planName, TypePlans typePlan, bool acceptLauch)
        {
            Code = code;
            PlanName = planName;
            TypePlan = typePlan;
            AcceptLauch = acceptLauch;
        }

        public static AccountPlansDto Create(string code, string planName, TypePlans typePlan, bool acceptLauch) =>
            new(code, planName, typePlan, acceptLauch);
    }
}
