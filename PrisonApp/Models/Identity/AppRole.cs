using Microsoft.AspNet.Identity.EntityFramework;

namespace PrisonApp.Models.Identity
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {
        }

        public AppRole(string name) : base(name)
        {
        }
    }
}