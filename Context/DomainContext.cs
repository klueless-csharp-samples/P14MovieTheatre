namespace MovieTheatre.Context
{
  using MovieTheatre.Data;
  using Microsoft.EntityFrameworkCore;

  public class DomainContext : DbContext
  {
    public DbSet<Movie> Movies { get; set; }

    public DbSet<Session> Sessions { get; set; }

    public DbSet<Ticket> Tickets { get; set; }
  }
}
