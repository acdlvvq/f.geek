using fgeek.Entities.Interfaces;
using SQLite;

namespace fgeek.Entities
{
    [Table("genres")]
    public class Genre : IEntity
    {
        [PrimaryKey, Unique]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        public Genre()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}
