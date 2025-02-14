using Application.UserCases.Users.Commands.Save;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar as operações relacionadas aos usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor da classe UserController.
        /// </summary>
        /// <param name="mediator">Instância do IMediator para mediar os comandos.</param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para salvar um novo usuário.
        /// </summary>
        /// <param name="command">Comando contendo as informações do usuário a ser salvo.</param>
        /// <returns>Retorna uma resposta com o resultado da operação.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
