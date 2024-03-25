using fgeek.Entities.Interfaces;
using SQLite;

namespace fgeek.Entities
{
    [Table("likes")]
    public class Like : IEntity
    {
        [PrimaryKey, Unique]
        [Column("id")]
        public int Id { get; set; }
        [Column("date_time")]
        public DateTime DateTime { get; set; }
        [Column("target_type")]
        public string TargetType { get; set; }
        [Column("target_id")]
        public string TargetId { get; set; }

        public Like()
        {
            Id = 0;
            DateTime = DateTime.MinValue;
            TargetType = TargetId = string.Empty;
        }

        public Like(int id, DateTime dateTime, string targetType, string targetId)
        {
            Id = id;
            DateTime = dateTime;
            TargetType = targetType;
            TargetId = targetId;
        }
    }
}
