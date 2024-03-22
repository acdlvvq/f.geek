using SQLite;
using System.Text;

namespace fgeek.Entities
{
    [Table("movie")]
    public class Movie : IEntity
    {
        [PrimaryKey, Unique]
        [Column("id")]
        public string Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("release_date")]
        public string ReleaseDate { get; set; }
        [Column("runtime")]
        public int Runtime { get; set; } // in minutes
        [Column("short_description")]
        public string ShortDescription { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("poster_url")]
        public string PosterURL { get; set; }
        [Column("genre_ids")]
        public byte[] GenreIds { get; set; }
        [Column("likes_count")]
        public int LikesCount { get; set; }

        public Movie()
        {
            Id = Title = ReleaseDate = ShortDescription = Description = PosterURL = string.Empty;
            Runtime = LikesCount = 0;
            GenreIds = [];
        }
        
    }
}
