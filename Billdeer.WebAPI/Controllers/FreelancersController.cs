using Billdeer.Business.Handlers.Freelancers.Commands;
using Billdeer.Business.Handlers.Freelancers.Queries;
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
    public class FreelancersController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var query = new GetFreelancerQuery() { Id = id };
            var result = await Mediator.Send(query);
            return SwitchMethod<Freelancer, IDataResult<Freelancer>>(result, "Freelancers", "Get");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetFreelancersQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Freelancer>, IDataResult<IEnumerable<Freelancer>>>(result, "Freelancers", "GetAll");
        }

        [HttpGet("deleted/{id}")]
        public async Task<IActionResult> GetDeletedById(long id)
        {
            var query = new GetDeletedFreelancerQuery() { Id = id };
            var result = await Mediator.Send(query);
            return SwitchMethod<Freelancer, IDataResult<Freelancer>>(result, "Freelancers", "GetAllDeletedById");
        }

        [HttpGet("deleteds")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var query = new GetDeletedFreelancersQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Freelancer>, IDataResult<IEnumerable<Freelancer>>>(result, "Freelancers", "GetAllDeleted");
        }

        [HttpGet("deleted/u{userId}")]
        public async Task<IActionResult> GetDeletedByFreelancerId(long userId)
        {
            var query = new GetDeletedFreelancerByUserIdQuery() { UserId = userId };
            var result = await Mediator.Send(query);
            return SwitchMethod<Freelancer, IDataResult<Freelancer>>(result, "Freelancers", "GetDeletedByUserId");
        }

        [HttpGet("deactivated/{id}")]
        public async Task<IActionResult> GetDeactivatedById(long id)
        {
            var query = new GetDeactivatedFreelancerQuery() { Id = id };
            var result = await Mediator.Send(query);
            return SwitchMethod<Freelancer, IDataResult<Freelancer>>(result, "Freelancers", "GetAllDeletedById");
        }

        [HttpGet("deactivateds")]
        public async Task<IActionResult> GetAllDeactivated()
        {
            var query = new GetDeactivatedFreelancersQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<Freelancer>, IDataResult<IEnumerable<Freelancer>>>(result, "Freelancers", "GetAllDeleted");
        }

        [HttpGet("deactivated/u{userId}")]
        public async Task<IActionResult> GetDeactivatedByFreelancerId(long userId)
        {
            var query = new GetDeactivatedFreelancerByUserIdQuery() { UserId = userId };
            var result = await Mediator.Send(query);
            return SwitchMethod<Freelancer, IDataResult<Freelancer>>(result, "Freelancers", "GetDeactivatedByUserId");
        }

        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateFreelancerCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod<Freelancer, IDataResult<Freelancer>>(result, "Freelancer", "Add");
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateFreelancerCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod<Freelancer, IDataResult<Freelancer>>(result, "Freelancers", "Update");
        }

        /// <summary>
        /// UpdateDeactivatedAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Deactivated")]
        public async Task<IActionResult> UpdateDeactivatedAsync([FromBody] UpdateDeactivatedFreelancerCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "Freelancers", "UpdateDeactivatedAsync");
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteFreelancerCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "Freelancers", "Delete");
        }
        
        /// <summary>
        /// DeleteByUserIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("UserId")]
        public async Task<IActionResult> DeleteByUserIdAsync([FromBody] DeleteFreelancerByUserIdCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "Freelancers", "Delete");
        }

        /// <summary>
        /// FakeDeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("FakeDelete")]
        public async Task<IActionResult> FakeDeleteAsync([FromBody] FakeDeleteFreelancerCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "Freelancers", "FakeDelete");
        }

        /// <summary>
        /// FakeDeleteByUserIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("FakeDeleteByUserId")]
        public async Task<IActionResult> FakeDeleteByUserIdAsync([FromBody] FakeDeleteFreelancerByUserIdCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "Freelancers", "FakeDelete");
        }



    }
}
