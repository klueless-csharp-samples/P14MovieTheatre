namespace MovieTheatre.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;
  using Microsoft.EntityFrameworkCore;

  public partial class Ticket
  {
    public int Id { get; set; }

    // Common fields
    public int SessionId { get; set; }

    public Session Session { get; set; }

    public string Name { get; set; }
  }
}
