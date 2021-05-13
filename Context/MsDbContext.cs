namespace MovieTheatre.Context
{
  using Microsoft.EntityFrameworkCore;

  public class MsDbContext : DomainContext
  {
    public MsDbContext()
    {
    }

    public MsDbContext(DbContextOptions<MsDbContext> options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=localhost,1433; Database=P14;User=sa; Password=Hambaro3");
  }
}
