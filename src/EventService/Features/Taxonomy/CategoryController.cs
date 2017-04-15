using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EventService.Features.Core;
using static EventService.Features.Taxonomy.GetCategoriesQuery;

namespace EventService.Features.Taxonomy
{
    [Authorize]
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetCategoriesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetCategoriesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        protected readonly IMediator _mediator;
    }
}
