using Microsoft.AspNet.Identity.EntityFramework;

namespace PrisonApplication.Models.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name) : base(name)
        {
        }
    }
}