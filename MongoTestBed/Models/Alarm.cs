namespace MongoTestBed.Models
{
    public class Alarm
    {
        public string? AlarmId { get; set; }
        public string? Category { get; set; }
        public bool IsCompleted { get; set; }
        public List<AlarmHistory>? Histories { get; set; }
    }

    public class AlarmHistory { 
        public string? HistoryId { get; set; }
        public string? Severity { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set;}
        public string? Description { get; set; }
    }
}
