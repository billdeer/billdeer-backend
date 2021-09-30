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

        [HttpGet("deleted/{id}")]
        public async Task<IActionResult> GetDeletedById(long id)
        {
            var query = new GetDeletedAdvertQuery() { Id = id};
            var result = await Mediator.Send(query);
            return SwitchMethod<Advert, IDataResult<Advert>>(result, "Adverts", "GetAllDeletedById");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAdvertsQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAll");
        }

        [HttpGet("u{userId}")]
        public async Task<IActionResult> GetAllByUserId(long userId)
        {
            var query = new GetAdvertsByUserIdQuery() { UserId = userId};
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAllByUserId");
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var query = new GetDeletedAdvertsQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAllDeleted");
        }

        [HttpGet("deleted/u{userId}")]
        public async Task<IActionResult> GetAllDeletedByUserId(long userId)
        {
            var query = new GetDeletedAdvertsByUserIdQuery() { UserId = userId};
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Advert>, IDataResult<IEnumerable<Advert>>>(result, "Adverts", "GetAllDeletedByUserId");
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
