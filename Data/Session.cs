namespace MovieTheatre.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;
  using Microsoft.EntityFrameworkCore;

  public partial class Session
  {
    public int Id { get; set; }

    // Common fields
    public int MovieId { get; set; }

    public Movie Movie { get; set; }

    public DateTime Time { get; set; }

    public List<Ticket> Tickets { get; set; } = new List<Ticket>();
  }
}
