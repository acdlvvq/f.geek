using fgeek.Entities.Interfaces;
using SQLite;
using System.Text;

namespace fgeek.Entities
{
    [Table("movies")]
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
        [Column("tagline")]
        public string Tagline { get; set; }
        [Column("budget")]
        public long Budget { get; set; }
        [Column("revenue")]
        public long Revenue { get; set; }
        [Column("rated")]
        public string Rated { get; set; } // age permission
        [Column("poster_url")]
        public string PosterURL { get; set; }
        [Column("genre_ids")]
        public byte[] GenreIds { get; set; }
        [Column("likes_count")]
        public int LikesCount { get; set; }
        [Column("comments_ids")]
        public byte[] CommentIds { get; set; }
        [Column("keyword_ids")]
        public byte[] KeywordIds { get; set; }
        [Column("production_countries_ids")]
        public byte[] ProductionCountriesIds { get; set; } 
        [Column("cast_ids")]
        public byte[] CastIds { get; set; } 
        [Column("crew_ids")]
        public byte[] CrewIds { get; set; }
        [Column("video_ids")]
        public byte[] VideoIds { get; set; } 
        [Column("imdb_rating")]
        public int IMDBRating { get; set; }
        [Column("rotten_tomatoes_rating")]
        public int RottenTomatoesRating { get; set; }
        [Column("metacritic_rating")]
        public int MetacriticRating { get; set; }

        public Movie()
        {
            Id = Title = ReleaseDate = ShortDescription = Description = PosterURL = Tagline = Rated = string.Empty;
            Runtime = LikesCount = RottenTomatoesRating = MetacriticRating = 0;
            GenreIds = CommentIds = KeywordIds = ProductionCountriesIds = CastIds = CrewIds = VideoIds = [];
        }
    }
}
