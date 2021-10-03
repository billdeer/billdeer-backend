using Billdeer.Business.Handlers.Adverts.Commands;
using Billdeer.Business.Handlers.Adverts.Queries;
using Billdeer.Core.Utilities.Results;
using Billdeer.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billdeer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertsController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var query = new GetAdvertQuery() { Id = id };
            var result = await Mediator.Send(query);
            return SwitchMethod<Advert, IDataResult<Advert>>(result, "Adverts", "Get");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAdvertsQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAll");
        }

        [HttpGet("f{freelancerId}")]
        public async Task<IActionResult> GetAllByFreelancerId(long freelancerId)
        {
            var query = new GetAdvertsByFreelancerIdQuery() { FreelancerId = freelancerId };
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAllByFreelancerId");
        }

        [HttpGet("deleted/{id}")]
        public async Task<IActionResult> GetDeletedById(long id)
        {
            var query = new GetDeletedAdvertQuery() { Id = id};
            var result = await Mediator.Send(query);
            return SwitchMethod<Advert, IDataResult<Advert>>(result, "Adverts", "GetAllDeletedById");
        }

        [HttpGet("deleteds")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var query = new GetDeletedAdvertsQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAllDeleted");
        }

        [HttpGet("deleted/f{freelancerId}")]
        public async Task<IActionResult> GetAllDeletedByFreelancerId(long freelancerId)
        {
            var query = new GetDeletedAdvertsByFreelancerIdQuery() { FreelancerId = freelancerId};
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAllDeletedByFreelancerId");
        }

        [HttpGet("deactivated/{id}")]
        public async Task<IActionResult> GetDeactivatedById(long id)
        {
            var query = new GetDeactivatedAdvertQuery() { Id = id};
            var result = await Mediator.Send(query);
            return SwitchMethod<Advert, IDataResult<Advert>>(result, "Adverts", "GetDeactivatedById");
        }

        [HttpGet("deactivateds")]
        public async Task<IActionResult> GetAllDeactivated()
        {
            var query = new GetDeactivatedAdvertsQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAllDeactivated");
        }

        [HttpGet("deactivated/f{freelancerId}")]
        public async Task<IActionResult> GetAllDeactivatedByFreelancerId(long freelancerId)
        {
            var query = new GetDeactivatedAdvertsByFreelancerIdQuery() { FreelancerId = freelancerId};
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAllDeactivatedByFreelancerId");
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateAdvertCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod<Advert, IDataResult<Advert>>(result, "Adverts", "Add");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAdvertCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod<Advert, IDataResult<Advert>>(result, "Adverts", "Update");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteAdvertCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "Adverts", "Delete");
        }

        [HttpDelete("FakeDelete")]
        public async Task<IActionResult> FakeDeleteAsync([FromBody] FakeDeleteAdvertCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "Adverts", "FakeDelete");
        }

    }
}
