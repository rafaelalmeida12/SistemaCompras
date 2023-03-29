using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using System.Linq;

namespace SistemaCompra.API.SolicitacaoCompra
{
    [ApiController]
    [Route("solicitacaoCompra")]
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("cadastro")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CadastrarCompra([FromBody] RegistrarCompraCommand registrarCompraCommand)
        {
            if (registrarCompraCommand.Itens.Count() == 0)
                return BadRequest("Insira pelo menos um item!");

            _mediator.Send(registrarCompraCommand);
            return Ok("cadastrado com sucesso!");
        }
    }
}
