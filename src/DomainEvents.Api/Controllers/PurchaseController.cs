using AutoMapper;
using DomainEvents.Api.ViewModels;
using DomainEvents.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DomainEvents.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PurchaseController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<string> Get([FromQuery] PurchaseViewModel purchase)
        {
            var makePurchaseCommand = _mapper.Map<MakePurchaseCommand>(purchase);

            var response = await _mediator.Send(makePurchaseCommand);

            return response;
        }
    }
}
