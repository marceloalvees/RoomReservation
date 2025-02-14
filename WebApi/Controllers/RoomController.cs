using Application.UserCases.Rooms.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas a salas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor do RoomController.
        /// </summary>
        /// <param name="mediator">Instância do IMediator para mediar as solicitações.</param>
        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém a lista de todas as salas.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento para a operação assíncrona.</param>
        /// <returns>Uma lista de salas.</returns>
        [HttpGet]
        public async Task<IActionResult> GetRooms(CancellationToken cancellationToken)
        {
            var query = new GetRoomsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
