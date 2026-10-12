using System;
using System.Collections.Generic;

namespace Lab4.Models;

public partial class Actor
{
    public int PersonId { get; set; }

    public string ActorName { get; set; } = null!;

    public int? BirthYear { get; set; }

    public int? NoOfFilms { get; set; }

    public int? NoOfAwards { get; set; }
}
