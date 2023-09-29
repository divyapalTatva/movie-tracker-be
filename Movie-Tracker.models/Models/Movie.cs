using System;
using System.Collections.Generic;

namespace Movie_Tracker.models.Models
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int GenreId { get; set; }
        public string Description { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string Writer { get; set; } = null!;
        public string LeadActor { get; set; } = null!;
        public long Budget { get; set; }
        public long GrossIndia { get; set; }
        public long GrossWorld { get; set; }
        public long Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Genre Genre { get; set; } = null!;
    }
}
