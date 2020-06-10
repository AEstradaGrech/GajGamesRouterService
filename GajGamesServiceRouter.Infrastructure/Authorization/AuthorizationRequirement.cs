using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GajGamesServiceRouter.Infrastructure.Authorization
{
    public class AuthorizationRequirement : AuthorizationHandler<AuthorizationRequirement> , IAuthorizationRequirement
    {
        private readonly List<string> _scopes;

        public AuthorizationRequirement(List<string> scopes)
        {
            _scopes = scopes;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
        {
            var roleClaims = context.User
                                   .Identities
                                   .First()
                                   .Claims
                                   .Where(c => c.Type.Contains("role"))
                                   .ToList();

            roleClaims.ForEach(r =>
            {
                if (_scopes.Contains(r.Value))
                    context.Succeed(requirement);
            });

            return;           
        }
    }
}
