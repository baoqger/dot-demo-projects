namespace MongoTestBed.Models
{
    public class AlarmStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string AlarmsCollectionName { get; set; } = null!;
    }
}
