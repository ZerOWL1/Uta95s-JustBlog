using System.Security.Claims;
using System.Threading.Tasks;
using FA.JustBlog.Core.Models.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FA.JustBlog.Core.Models.AppIdentities
{
    public class AppUserIdentity : IdentityUser
    {
        public string Avatar { get; set; }
        public string External { get; set; }
        public Status Status { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsyncTask(UserManager<AppUserIdentity> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            //write add claims here 

            return userIdentity;
        }

    }
}