using System.Data.Entity.ModelConfiguration;
using FA.JustBlog.Core.Models.AppIdentities;

namespace FA.JustBlog.Core.Models.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<AppUserIdentity>
    {
        public UserConfiguration()
        {
            
        }
    }
}