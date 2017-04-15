using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EventService.Features.Core;
using static EventService.Features.Taxonomy.GetTagsQuery;

namespace EventService.Features.Taxonomy
{
    [Authorize]
    [RoutePrefix("api/tag")]
    public class TagController : ApiController
    {
        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetTagsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetTagsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
