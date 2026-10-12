using System;
using System.Collections.Generic;

namespace Lab4.Models;

public partial class MovieCharacter
{
    public int CharacterId { get; set; }

    public string CharacterName { get; set; } = null!;

    public int? Age { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
