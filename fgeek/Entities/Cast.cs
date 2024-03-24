using fgeek.Entities.Interfaces;
using SQLite;

namespace fgeek.Entities
{
    [Table("cast")]
    public class Cast : IEntity
    {
        [PrimaryKey, Unique]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("character")]
        public string Character { get; set; }
        [Column("order")]
        public int Order { get; set; }

        public Cast()
        {
            Id = Order = 0;
            Name = Character = string.Empty;
        }
    }
}
