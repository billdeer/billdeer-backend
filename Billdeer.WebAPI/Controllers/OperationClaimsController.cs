using Billdeer.Business.Handlers.OperationClaims.Commands;
using Billdeer.Business.Handlers.OperationClaims.Queries;
using Billdeer.Core.Entities.Concrete;
using Billdeer.Core.Utilities.Results;
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
    public class OperationClaimsController : BaseApiController
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetOperationClaimQuery() { OperationClaimId = id };
            var result = await Mediator.Send(query);
            return SwitchMethod<OperationClaim, IDataResult<OperationClaim>>(result, "OperationClaims", "Get");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetOperationClaimsQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<OperationClaim>, IDataResult<IEnumerable<OperationClaim>>>(result, "OperationClaims", "GetAll");
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateOperationClaimCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod<OperationClaim, IDataResult<OperationClaim>>(result, "EntityExamples", "Add");
        }
    }
}
