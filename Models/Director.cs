using System;
using System.Collections.Generic;

namespace Lab4.Models;

public partial class Director
{
    public int PersonId { get; set; }

    public string DirectorName { get; set; } = null!;

    public int? BirthYear { get; set; }

    public int? NoOfFilms { get; set; }

    public int? NoOfAwards { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
