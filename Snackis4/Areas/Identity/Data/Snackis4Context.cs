using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Snackis4.Areas.Identity.Data;

namespace Snackis4.Data;
public class Snackis4Context : IdentityDbContext<Snackis4User>
{
    public Snackis4Context(DbContextOptions<Snackis4Context> options)
        : base(options)
    {
    }
    public DbSet<Models.Category> Category { get; set; } = default!;
    public DbSet<Models.Subcategory> Subcategory { get; set; } = default!;
    public DbSet<Models.Post> Post { get; set; } = default!;
    public DbSet<Models.Comment> Comments { get; set; } = default!;

    public DbSet<Models.PrivateMessages> PrivateMessages { get; set; } = default!;


}

//protected override void OnModelCreating(ModelBuilder builder)
//{
//    base.OnModelCreating(builder);
//    // Customize the ASP.NET Identity model and override the defaults if needed.
//    // For example, you can rename the ASP.NET Identity table names and more.
//    // Add your customizations after calling base.OnModelCreating(builder);
//}