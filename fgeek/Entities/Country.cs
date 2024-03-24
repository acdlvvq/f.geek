using fgeek.Entities.Interfaces;
using SQLite;

namespace fgeek.Entities
{
    [Table("countries")]
    public class Country : IEntity
    {
        [PrimaryKey, Unique]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        public Country()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}
