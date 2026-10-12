using System;
using System.Collections.Generic;

namespace Lab4.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string MovieName { get; set; } = null!;

    public string? Genre { get; set; }

    public int? Year { get; set; }

    public decimal? ImdbRating { get; set; }

    public int? DirectorId { get; set; }

    public virtual Director? Director { get; set; }

    public virtual ICollection<MovieCharacter> Characters { get; set; } = new List<MovieCharacter>();
}
