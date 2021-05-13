namespace MovieTheatre.Models.Tickets
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;
  using Microsoft.EntityFrameworkCore;
  using MovieTheatre.Data;

  public partial class Create
  {
    public int SessionId { get; set; }

    public Session Session { get; set; }

    public string Name { get; set; }
  }
}
