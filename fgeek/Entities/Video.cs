using fgeek.Entities.Interfaces;
using SQLite;

namespace fgeek.Entities
{
    [Table("videos")]    
    public class Video : IEntity
    {
        [PrimaryKey, Unique]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("youtube_url")]
        public string YouTubeURL { get; set; }

        public Video() 
        {
            Id = 0;
            Name = YouTubeURL = string.Empty;
        }
    }
}
