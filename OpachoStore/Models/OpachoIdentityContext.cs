using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OpachoStore.Models
{
    public class OpachoIdentityContext : IdentityDbContext<IdentityUser>
    {
        public OpachoIdentityContext(DbContextOptions<OpachoIdentityContext> options) : base(options) { }
    }
}
