using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WAI.API.DataAccess.Entities;

namespace WAI.API.DataAccess;

public class DataContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }

    public DbSet<GameMember> GameMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().Property(u => u.Id).ValueGeneratedNever();

        builder.Entity<Game>()
            .HasMany(g => g.Users)
            .WithMany(u => u.Games)
            .UsingEntity<GameMember>(
                j => j
                    .HasOne(pt => pt.User)
                    .WithMany(p => p.GameMembers)
                    .HasForeignKey(pt => pt.UserId),
                j => j
                    .HasOne(pt => pt.Game)
                    .WithMany(t => t.GameMembers)
                    .HasForeignKey(pt => pt.GameId),
                j => j.HasKey(gm => new {gm.UserId, gm.GameId}));
    }
}