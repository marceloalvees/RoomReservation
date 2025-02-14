using Application.UserCases.Reservations.Commands.Save;
using Application.UserCases.Reservations.Commands.Update;
using Application.UserCases.Reservations.Queries.GetByUserEmail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar as reservas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor do ReservationController.
        /// </summary>
        /// <param name="mediator">Instância do IMediator para mediar os comandos e consultas.</param>
        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma nova reserva.
        /// </summary>
        /// <param name="command">Comando para salvar a reserva.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Retorna a resposta da operação.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveReservationCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Atualiza uma reserva existente.
        /// </summary>
        /// <param name="command">Comando para atualizar a reserva.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Retorna a resposta da operação.</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateReservationCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Obtém as reservas de um usuário pelo email.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Retorna a lista de reservas do usuário.</returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> GetReservations(string email, CancellationToken cancellationToken)
        {
            var query = new GetReservationsByEmailQuery { Email = email };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
