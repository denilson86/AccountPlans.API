using Domain.Entities;
using Domain.Interfaces;
using Dapper;
using System.Data;

namespace Domain.Repository
{
    public class AccountPlanRepository : IAccountPlanRepository
    {
        public async Task<AccountPlansDto> AddAccountPlanAsync(AccountPlansDto accountPlans, IDbConnection connection, IDbTransaction? transaction)
        {
            var query = @"
                INSERT INTO dbo.ACCOUNT_PLANS (
                    ,CodePlan
                    ,NamePlan
                    ,TypePlan
                    ,AcceptLauch
                    ,IsDeleted

                )
                OUTPUT INSERTED.*
                VALUES (
                    GETDATE()
                    ,@code
                    ,@planName
                    ,@typePlan
                    ,@acceptLauch
                    ,0
                )";

            var parameters = new
            {
                accountPlans.Code,
                accountPlans.PlanName,
                accountPlans.TypePlan,
                accountPlans.AcceptLauch
            };
            var result = await connection.QuerySingleAsync<AccountPlansDto>(query, parameters, transaction);
            return result;
        }

        public async Task<bool> DeleteAccountPlansAsync(IDbConnection connection, int id)
        {
            var query = @"
                UPDATE dbo.ACCOUNT_PLANS
                    SET IsDeleted = 1
                WHERE id = @id)";

            (await connection.QueryAsync<AccountPlansDto>(query, new { id = id })).SingleOrDefault();
            return true;
        }

        public async Task<AccountPlansDto?> GetAccountPlansByCodeAsync(IDbConnection connection, string code)
        {
            var query = @"
                SELECT CodePlan
                FROM dbo.ACCOUNT_PLANS
                WHERE Code = @code)";

            var result = (await connection.QueryAsync<AccountPlansDto>(query, new { code = code })).FirstOrDefault();
            return result == null ? null : result;
        }

        public async Task<List<AccountPlansDto>> GetListAccountPlansAsync(IDbConnection connection, string? filter = null)
        {
            var parameter = new object { };
            var query = @"
                SELECT 
                    ,CodePlan
                    ,NamePlan
                    ,TypePlan
                    ,AcceptLauch
                FROM dbo.ACCOUNT_PLANS
                WHERE IsDeleted = 0)";

            //Caso Filtro preechido - Query contem filtros
            if (!string.IsNullOrEmpty(filter))
            {
                if (ValidateString(filter))
                    parameter = new { code = filter };
                else
                    parameter = new { planName = filter };

                var result = (await connection.QueryAsync<AccountPlansDto>(query, parameter)).ToList();
                return result;
            }
            else 
            {
                var result = (await connection.QueryAsync<AccountPlansDto>(query)).ToList();
                return result;
            }
        }

        public async Task<string> GetNextCodeAsync(IDbConnection connection)
        {
            var query = @"
                SELECT MAX(Code) AS CodePlan
                FROM dbo.ACCOUNT_PLANS)";
            var result = (await connection.QueryAsync<decimal>(query)).FirstOrDefault();
            return (result++).ToString();
        }

        private bool ValidateString(string value)
        {
            if (decimal.TryParse(value, out var eval))
                return true;
            else
                return false;
        }
    }
}
