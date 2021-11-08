using Billdeer.Business.Handlers.AdvertPackages.Commands;
using Billdeer.Business.Handlers.AdvertPackages.Queries;
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
    public class AdvertPackagesController : BaseApiController
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var query = new GetAdvertPackageQuery() { Id = id };
            var result = await Mediator.Send(query);
            return SwitchMethod<AdvertPackage, IDataResult<AdvertPackage>>(result, "AdvertPackages", "Get");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAdvertPackagesQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<AdvertPackage>, IDataResult<IEnumerable<AdvertPackage>>>(result, "AdvertPackages", "GetAll");
        }

        [HttpGet("a{advertId}")]
        public async Task<IActionResult> GetAllByAdvertId(long advertId)
        {
            var query = new GetAdvertPackagesByAdvertIdQuery() { AdvertId = advertId};
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<AdvertPackage>, IDataResult<IEnumerable<AdvertPackage>>>(result, "AdvertPackages", "GetAllByAdvertId");
        }

        [HttpGet("deleted/{id}")]
        public async Task<IActionResult> GetDeletedById(long id)
        {
            var query = new GetDeletedAdvertPackageQuery() { Id = id };
            var result = await Mediator.Send(query);
            return SwitchMethod<AdvertPackage, IDataResult<AdvertPackage>>(result, "AdvertPackages", "GetAllDeletedById");
        }

        [HttpGet("deleteds")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var query = new GetDeletedAdvertPackagesQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<AdvertPackage>, IDataResult<IEnumerable<AdvertPackage>>>(result, "AdvertPackages", "GetAllDeleted");
        }

        [HttpGet("deleted/a{advertId}")]
        public async Task<IActionResult> GetAllDeletedByAdvertId(long advertId)
        {
            var query = new GetDeletedAdvertPackagesByAdvertIdQuery() { AdvertId = advertId };
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<AdvertPackage>, IDataResult<IEnumerable<AdvertPackage>>>(result, "AdvertPackages", "GetAllDeletedByAdvertId");
        }

        [HttpGet("deactivated/{id}")]
        public async Task<IActionResult> GetDeactivatedById(long id)
        {
            var query = new GetDeactivatedAdvertPackageQuery() { Id = id };
            var result = await Mediator.Send(query);
            return SwitchMethod<AdvertPackage, IDataResult<AdvertPackage>>(result, "AdvertPackages", "GetDeactivatedById");
        }

        [HttpGet("deactivateds")]
        public async Task<IActionResult> GetAllDeactivated()
        {
            var query = new GetDeactivatedAdvertPackagesQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<AdvertPackage>, IDataResult<IEnumerable<AdvertPackage>>>(result, "Adverts", "GetAllDeactivated");
        }

        [HttpGet("deactivated/a{advertId}")]
        public async Task<IActionResult> GetAllDeactivatedByAdvertId(long advertId)
        {
            var query = new GetDeactivatedAdvertPackagesByAdvertIdQuery() { AdvertId = advertId };
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<AdvertPackage>, IDataResult<IEnumerable<AdvertPackage>>>(result, "Adverts", "GetAllDeactivatedByAdvertId");
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateAdvertPackageCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod<AdvertPackage, IDataResult<AdvertPackage>>(result, "AdvertPackages", "AddAsync");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAdvertPackageCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod<AdvertPackage, IDataResult<AdvertPackage>>(result, "AdvertPackages", "UpdateAsync");
        }


        [HttpPut("Deactivated")]
        public async Task<IActionResult> UpdateDeactivatedAsync([FromBody] UpdateDeactivateAdvertPackageCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "AdvertPackages", "UpdateDeactivatedAsync");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteAdvertPackageCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "AdvertPackages", "Delete");
        }

        [HttpDelete("FakeDelete")]
        public async Task<IActionResult> FakeDeleteAsync([FromBody] FakeDeleteAdvertPackageCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "AdvertPackages", "FakeDelete");
        }


    }
}
