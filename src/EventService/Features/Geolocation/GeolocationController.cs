using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EventService.Features.Core;

using static EventService.Features.Geolocation.GetAddressFromLatitudeAndLongitudeQuery;

namespace EventService.Features.Geolocation
{
    [Authorize]
    [RoutePrefix("api/geolocation")]
    public class GeolocationController : ApiController
    {
        public GeolocationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Route("getAddress")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetAddressFromLatitudeAndLongitudeResponse))]
        public async Task<IHttpActionResult> GetAddress([FromUri]GetAddressFromLatitudeAndLongitudeRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        protected readonly IMediator _mediator;
    }
}
