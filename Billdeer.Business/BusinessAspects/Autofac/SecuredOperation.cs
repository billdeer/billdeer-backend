using Billdeer.Business.Constants;
using Billdeer.Core.Extensions;
using Billdeer.Core.Utilities.Interceptors;
using Billdeer.Core.Utilities.IoC;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security;

namespace Billdeer.Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            //var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

            //if (userId == null) throw new SecurityException(Messages.AuthorizationsDenied);

            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationsDenied);
        }
    }
}
