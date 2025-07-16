using Library.Order.Application.Commands;
using Library.Order.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Library.Order.Domain.Exceptions;

namespace Library.Order.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Registra una nueva orden de compra.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // Usa un ID de usuario de ejemplo si no se provee. En un entorno real, vendría de la autenticación.
                if (command.UserId == Guid.Empty) command.UserId = Guid.Parse("f0e1d2c3-b4a5-6789-0123-456789abcdef");

                var response = (OrderResponse?)await _mediator.Send(command);
                return CreatedAtAction(nameof(GetOrderById), new { id = response!.OrderId }, response);
            }
            catch (ApplicationException ex) { return BadRequest(new { message = ex.Message }); }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear orden: {ex.Message} - {ex.StackTrace}");
                return StatusCode(500, new { message = "Ocurrió un error inesperado al registrar la orden." });
            }
        }

        /// <summary>
        /// Obtiene los detalles de una orden por su ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            Console.WriteLine($"Solicitud para obtener detalles de la orden: {id}");
            var order = await _mediator.Send(new CreateOrderCommand { OrderId = id });
            if (order == null) return NotFound(new { message = $"Orden con ID {id} no encontrada." });
            return Ok(order);
        }
    }

    public interface IMediator
    {
        Task<object?> Send(CreateOrderCommand command);
    }
}