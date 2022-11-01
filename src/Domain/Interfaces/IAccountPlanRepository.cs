using Domain.Entities;
using Dapper;
using System.Data;

namespace Domain.Interfaces
{
    public interface IAccountPlanRepository
    {
        Task<AccountPlansDto> AddAccountPlanAsync(AccountPlansDto accountPlans, IDbConnection connection, IDbTransaction? transaction);
        Task<bool> DeleteAccountPlansAsync(IDbConnection connection, int id);
        Task<AccountPlansDto?> GetAccountPlansByCodeAsync(IDbConnection connection, string code);
        Task<List<AccountPlansDto>> GetListAccountPlansAsync(IDbConnection connection, string? filter = null);
        Task<string> GetNextCodeAsync(IDbConnection connection);
    }
}
