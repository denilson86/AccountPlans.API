using Domain.Entities;
using Domain.Interfaces;
using Infra.Interfaces;

namespace Domain.Rules
{
    public class AccountPlansRules : IAccountPlansRules
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountPlanRepository _accountPlanRepository;

        public AccountPlansRules(IUnitOfWork unitOfWork, IAccountPlanRepository accountPlanRepository)
        {
            _unitOfWork = unitOfWork;
            _accountPlanRepository = accountPlanRepository;
        }

        public async Task<string> AddAccountPlanAsync(AccountPlansDto newAccountPlans)
        {
            try
            {
                _unitOfWork.Open();
                _unitOfWork.Begin();

                var accountPlan = await _accountPlanRepository.GetAccountPlansByCodeAsync(_unitOfWork.DbConnection, newAccountPlans.Code);
                if (accountPlan != null)
                {
                    if (accountPlan.Code == newAccountPlans.Code)
                    {
                        return "Código do Plano já cadastrado";
                    }
                    //A conta que aceita lançamentos não pode ter contas filhas
                    else if (accountPlan.AcceptLauch && validateDecimal(accountPlan.Code, newAccountPlans.Code))
                    {
                        return "Esse plano de contas não pode ter contas filhas";
                    }
                    //A conta que não aceita lançamentos pode ser pai de outras contas
                    else if (!accountPlan.AcceptLauch)
                        return "Esse plano de contas não aceita lançamentos";
                }

                var result = await _accountPlanRepository.AddAccountPlanAsync(newAccountPlans, _unitOfWork.DbConnection, _unitOfWork.Transaction);
                _unitOfWork.Commit();
                _unitOfWork.Close();

                return "OK";
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteAccountPlansAsync(int id)
        {
            try
            {
                _unitOfWork.Open();
                var result = await _accountPlanRepository.DeleteAccountPlansAsync(_unitOfWork.DbConnection, id);
                _unitOfWork.Close();

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<AccountPlansDto>> GetListAccountPlansAsync(string? filter)
        {
            _unitOfWork.Open();
            var result = await _accountPlanRepository.GetListAccountPlansAsync(_unitOfWork.DbConnection, filter);
            _unitOfWork.Close();

            return result;
        }

        public async Task<string> GetNextCodeAsync()
        {
            _unitOfWork.Open();
            var result = await _accountPlanRepository.GetNextCodeAsync(_unitOfWork.DbConnection);
            _unitOfWork.Close();

            return validateLastDecimal(result);
        }

        private bool validateDecimal(string codePlan1, string codePlan2)
        {
            var n1 = codePlan1.Split(".");
            var n2 = codePlan2.Split(".");
            if (n1[2] == n2[2])
                return true;
            else
                return false;
        }

        private string validateLastDecimal(string codePlan)
        {
            int number;
            var n1 = codePlan.Split(".");
            int.TryParse(n1[2], out number);

            if (number > 999)
                return $"{n1[0]}.{n1[1]}";
            else
                return codePlan;
        }
    }
}
