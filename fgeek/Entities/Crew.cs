using fgeek.Entities.Interfaces;
using SQLite;

namespace fgeek.Entities
{
    [Table("crew")]
    public class Crew : IEntity
    {
        [PrimaryKey, Unique]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("department")]
        public string Department { get; set; } 
        [Column("job")]
        public string Job { get; set; }

        public Crew()
        {
            Id = 0;
            Name = Department = Job = string.Empty;
        }
    }
}
