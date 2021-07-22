using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator _mediator;
    }
}
