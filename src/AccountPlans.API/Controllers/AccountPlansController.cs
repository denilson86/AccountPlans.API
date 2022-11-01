using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountPlans.API.Controllers
{
    [Route("api/v1/accounts-plans")]
    [ApiController]
    public class AccountPlansController : Controller
    {
        private readonly IAccountPlansRules _accountPlansRules;

        public AccountPlansController(IAccountPlansRules accountPlansRules)
        {
            _accountPlansRules = accountPlansRules;
        }

        /// <summary>
        /// Método para inserir novo plano de contas
        /// </summary>
        /// <response code="200">Em casos de successo</response>
        /// <response code="400">Em casos de falha na requisição</response>
        /// <response code="500">Em casos erros inesperados</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddAccountPlanAsync(AccountPlansDto accountPlans)
        {
            var result = await _accountPlansRules.AddAccountPlanAsync(accountPlans);
            if (result.Contains("OK")) return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Método para Deletar plano de contas
        /// </summary>
        /// <response code="200">Em casos de successo</response>
        /// <response code="400">Em casos de falha na requisição</response>
        /// <response code="500">Em casos erros inesperados</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAccountPlanAsync(int id)
        {
            var result = await _accountPlansRules.DeleteAccountPlansAsync(id);
            if (result) return Ok(result);
            else
                return BadRequest("Erro ao deletar");
        }

        /// <summary>
        /// Método para retorna proximo codigo
        /// </summary>
        /// <response code="200">Em casos de successo</response>
        /// <response code="400">Em casos de falha na requisição</response>
        /// <response code="500">Em casos erros inesperados</response>
        [HttpGet]
        [Route("next-code")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<string> GetNextCode()
        {
            return await _accountPlansRules.GetNextCodeAsync();
        }

        /// <summary>
        /// Método para retorna listagem pano de contas
        /// </summary>
        /// <response code="200">Em casos de successo</response>
        /// <response code="400">Em casos de falha na requisição</response>
        /// <response code="500">Em casos erros inesperados</response>
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<AccountPlansDto>> GetListAccountPlanAsync([FromQuery] string? filter = null)
        {
            return await _accountPlansRules.GetListAccountPlansAsync(filter);
        }
    }
}
