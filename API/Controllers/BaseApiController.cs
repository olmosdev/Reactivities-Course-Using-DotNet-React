using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        // Private backing field to cache the Mediator instance once it's resolved.
        private IMediator? _mediator;

        // Protected property to allow derived controllers to access Mediator easily.
        // This pattern (Service Locator via HttpContext) is used to avoid injecting IMediator 
        // into the constructor of every single controller that inherits from BaseApiController.
        protected IMediator Mediator => 
            // The ??= (null-coalescing assignment) operator checks if _mediator is null.
            // If it is, it attempts to resolve the IMediator service from the request's service container.
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
                // If the service is not registered in the DI container, throw an exception.
                ?? throw new InvalidOperationException("IMediator service is unavailable");
    }
}
