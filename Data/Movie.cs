namespace MovieTheatre.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;
  using Microsoft.EntityFrameworkCore;

  public partial class Movie
  {
    public int Id { get; set; }

    // Common fields
    public string Title { get; set; }

    public List<Session> Sessions { get; set; } = new List<Session>();
    
  }
}
