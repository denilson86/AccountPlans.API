using Moq;
using Xunit;
using Domain.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using AccountPlans.API.Controllers;
using Infra.Interfaces;

namespace TestAccountPlans.Controllers
{
    public class AccountPlansTestController
    {
        private readonly Mock<IAccountPlansRules> MockIAccountPlansRules;
        private readonly AccountPlansDto PostDto;
        private readonly AccountPlansController accountPlansController;

        public AccountPlansTestController()
        {
            MockIAccountPlansRules = new Mock<IAccountPlansRules>();
            accountPlansController = new AccountPlansController(
                    MockIAccountPlansRules.Object);
        }


        [Fact(DisplayName = "AddAccountPlanAsync - Success")]
        public async Task AddAccountPlanAsync_Success()
        {
            MockIAccountPlansRules.Setup(x => x.AddAccountPlanAsync(It.IsAny<AccountPlansDto>())).Verifiable();

            var result = (ObjectResult)await accountPlansController.AddAccountPlanAsync(PostDto);

            result.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "AddAccountPlanAsync - Return bad request")]
        public async Task AddAccountPlanAsync_ReturnBadRequest()
        {
            PostDto.Code = null;
            MockIAccountPlansRules.Setup(x => x.AddAccountPlanAsync(It.IsAny<AccountPlansDto>())).Verifiable();

            var result = (BadRequestResult)await accountPlansController.AddAccountPlanAsync(PostDto);

            result.StatusCode.Should().Be(400);
        }

        [Fact(DisplayName = "DeleteAccountPlansAsync - Success")]
        public async Task DeleteAccountPlansAsync_Success()
        {
            int id = 2;
            MockIAccountPlansRules.Setup(x => x.DeleteAccountPlansAsync(id)).Verifiable();

            var result = (ObjectResult)await accountPlansController.DeleteAccountPlanAsync(id);

            result.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "DeleteAccountPlansAsync - Return bad request")]
        public async Task DeleteAccountPlansAsync_ReturnBadRequest()
        {
            int id = 999;
            MockIAccountPlansRules.Setup(x => x.DeleteAccountPlansAsync(id)).Verifiable();

            var result = (BadRequestResult)await accountPlansController.DeleteAccountPlanAsync(id);

            result.StatusCode.Should().Be(400);
        }


        [Fact(DisplayName = "GetListAccountPlanAsync - Success")]
        public async Task GetListAccountPlanAsync_Success()
        {
            MockIAccountPlansRules.Setup(x => x.GetListAccountPlansAsync(String.Empty)).Verifiable();

            var result = await accountPlansController.GetListAccountPlanAsync(string.Empty);

            result.Should().AllBeAssignableTo<AccountPlansDto>();
        }
    }
}
