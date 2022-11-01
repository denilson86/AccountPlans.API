using Domain.Entities;
using System;

namespace Domain.Interfaces
{
    public interface IAccountPlansRules
    {
        Task<string> AddAccountPlanAsync(AccountPlansDto accountPlans);
        Task<bool> DeleteAccountPlansAsync(int id);
        Task<List<AccountPlansDto>> GetListAccountPlansAsync(string? filter);
        Task<string> GetNextCodeAsync();
    }
}
