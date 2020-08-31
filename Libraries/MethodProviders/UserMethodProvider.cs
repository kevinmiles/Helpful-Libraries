using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Scripting;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;

namespace Lombiq.HelpfulLibraries.Libraries.MethodProviders
{
    public class UserMethodProvider : IGlobalMethodProvider
    {
        private static GlobalMethod GetUserIdByUserName = new GlobalMethod
        {
            Name = "getUserIdByUserName",
            Method = serviceProvider => (Func<string, string>)(email =>
            {
                var userService = serviceProvider.GetRequiredService<IUserService>();
                var user = userService.GetUserAsync(email).Result as User;
                var userId = user.Id.ToTechnicalString();

                return userId;
            })
        };

        public IEnumerable<GlobalMethod> GetMethods()
        {
            yield return GetUserIdByUserName;
        }
    }
}