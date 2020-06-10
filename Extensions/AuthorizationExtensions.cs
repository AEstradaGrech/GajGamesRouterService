using System;
using System.Collections;
using System.Collections.Generic;
using GajGamesServiceRouter.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace GajGamesServiceRouter.Extensions
{
    public static class AuthorizationExtensions
    {
        public static AuthorizationPolicyBuilder RequireScope(this AuthorizationPolicyBuilder builder, List<string> scopes)
        {
            builder.AddRequirements(new AuthorizationRequirement(scopes));

            return builder;
        }

        public static IServiceCollection SetAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.SetAnonymousTokenPolicy()
                       .SetEmployeePolicy()
                       .SetCustomersPolicy();
            });

            return services;
        }

        public static AuthorizationOptions SetAnonymousTokenPolicy(this AuthorizationOptions options)
        {
            var scopes = new List<string> { "anonymous", "pr-bronze", "pr-silver", "pr-gold", "empolyee" };

            options.AddPolicy("Anonymous", policy => policy.RequireScope(scopes));

            return options;
        }

        private static AuthorizationOptions SetEmployeePolicy(this AuthorizationOptions options)
        {
            var scopes = new List<string> { "employee" };

            options.AddPolicy("Employees", policy => policy.RequireScope(scopes));

            return options;
        }

        private static AuthorizationOptions SetCustomersPolicy(this AuthorizationOptions options)
        {
            var scopes = new List<string> { "employee" , "pr-bronze", "pr-silver", "pr-gold" };

            options.AddPolicy("Customers", policy => policy.RequireScope(scopes));

            return options;
        }



    }
}
