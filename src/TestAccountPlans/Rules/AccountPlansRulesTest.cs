using AutoFixture;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Rules;
using Infra.Interfaces;
using Moq;
using Xunit;

namespace TestAccountPlans.Rules
{
    public class AccountPlansRulesTest
    {
        private readonly Fixture Fixture;
        private readonly Mock<IAccountPlansRules> MockIAccountPlansRules;
        private readonly Mock<IUnitOfWork> MockIUnitOfWork;
        private readonly Mock<IAccountPlanRepository> MockIAccountPlanRepository;
        private readonly AccountPlansDto PostDto;
        private readonly AccountPlansRules accountPlansRules;

        public AccountPlansRulesTest()
        {
            MockIUnitOfWork = new Mock<IUnitOfWork>();
            MockIAccountPlansRules = new Mock<IAccountPlansRules>();
            PostDto = Fixture.Create<AccountPlansDto>();

            accountPlansRules = new AccountPlansRules(
                    MockIUnitOfWork.Object,
                    MockIAccountPlanRepository.Object);
        }

        [Fact(DisplayName = "AddAccountPlanAsync - Success")]
        public async Task<string> AddAccountPlanAsync_Success()
        {
            var accountPlansResponse = AccountPlansDto.Create("1.1", "Receita teste", TypePlans.Receita, true);

            MockIAccountPlanRepository.Setup(x => x.GetAccountPlansByCodeAsync(MockIUnitOfWork.Object.DbConnection, "1.1")).ReturnsAsync(accountPlansResponse);

            var result = (string)await accountPlansRules.AddAccountPlanAsync(PostDto);
            return result;
        }

        [Fact(DisplayName = "AddAccountPlanAsync - Bad request Code already registered")]
        public async Task<string> AddAccountPlanAsync_BadRequestCodeAlreadyRegistered()
        {
            var accountPlansResponse = AccountPlansDto.Create("1.0", "Receita teste", TypePlans.Receita, true);

            MockIAccountPlanRepository.Setup(x => x.GetAccountPlansByCodeAsync(MockIUnitOfWork.Object.DbConnection, "1.0")).ReturnsAsync(accountPlansResponse);

            var result = (string)await accountPlansRules.AddAccountPlanAsync(PostDto);
            return result;
        }
    }
}
