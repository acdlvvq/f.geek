using fgeek.Entities.Interfaces;
using SQLite;

namespace fgeek.Entities
{
    [Table("keywords")]
    public class Keyword : IEntity
    {
        [PrimaryKey, Unique]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        public Keyword()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}
