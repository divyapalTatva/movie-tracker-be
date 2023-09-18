using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Tracker_Common.ViewModels
{
    public class MovieVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string LeadActor { get; set; }
        public long Budget { get; set; }
        public long GrossIndia { get; set; }
        public long GrossWorld { get; set; }
        public long Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
