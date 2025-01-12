using System.Runtime.Serialization;

namespace ToleranceTunnelParser
{
    [DataContract]
    public class ReplayRunnerConfiguration
    {
        [DataMember(Order = 0)]
        public string TenantId { get; set; }
        [DataMember(Order = 1)]
        public ProvisionConfiguration ProvisionConfiguration { get; set; }
        [DataMember(Order = 2)]
        public StreamingConfiguration StreamingConfiguration { get; set; }
        [DataMember(Order = 3)]
        public PlanConfiguration PlanConfiguration { get; set; }

        [DataMember(Order = 4)] public bool IsAutoMode { get; set; } = true;
    }

    [DataContract]
    public class ProvisionConfiguration
    {
        [DataMember]
        public RigProvisionConfiguration RigProvisionConfiguration { get; set; }
        [DataMember]
        public WellProvisionConfiguration WellProvisionConfiguration { get; set; }
        [DataMember]
        public WellboreProvisionConfiguration WellboreProvisionConfiguration { get; set; }
    }

    public class RigProvisionConfiguration
    {
        [DataMember]
        public string RigName { get; set; }
        [DataMember]
        public string RigId { get; set; }
        [DataMember]
        public string RigTemplateFile { get; set; }
    }

    [DataContract]
    public class WellProvisionConfiguration
    {
        [DataMember]
        public string WellWitsmlFile { get; set; }
        [DataMember]
        public WellCreateModel WellCreateModel { get; set; }
        [DataMember]
        public WellConfigModel WellConfigModel { get; set; }
        [DataMember]
        public bool AddTimeSuffixOnWellName { get; set; } = true;
        [DataMember]
        public bool AddTestScope { get; set; } = true;
        [DataMember]
        public string Algorithms { get; set; }
        [DataMember]
        public bool EnableBlobSave { get; set; }
        [DataMember]
        public bool? MultiWellboreEnabled { get; set; }
    }

    public class WellCreateModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string TimeZone { get; set; }
        public string TestScope { get; set; }
        public string OrganizationId { get; set; }
    }

    [DataContract]
    public class WellConfigModel
    {
        [DataMember]
        public bool Archivable { get; set; }
        [DataMember]
        public int TimeToLive { get; set; } = 60;
        [DataMember]
        public int TimeToGhost { get; set; }
    }

    [DataContract]
    public class WellboreProvisionConfiguration
    {
        [DataMember]
        public string WellboreWitsmlTemplateFile { get; set; }
    }

    [DataContract]
    public class StreamingConfiguration
    {
        [DataMember] public DateTimeOffset StreamingStartTime { get; set; } = DateTimeOffset.MinValue;
        [DataMember] public DateTimeOffset StreamingStopTime { get; set; } = DateTimeOffset.MaxValue;
        [DataMember] public List<StreamingRange> StreamingSpeedConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> WellConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> WellboreConfiguration { get; set; }
        [DataMember] public JobSetupConfiguration JobSetupConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> WellboreGeometryConfiguration { get; set; }
        [DataMember] public List<ChannelStreamingConfiguration> ChannelConfiguration { get; set; }
        [DataMember] public List<BhaRunStreamingCommand> BhaRunConfiguration { get; set; }
        [DataMember] public string BhaRunSequenceFile { get; set; }
        [DataMember] public string TubularMappingFile { get; set; }
        [DataMember] public string TimeSeriesMappingFile { get; set; }
        [DataMember] public bool IsTrajectoryApiStreaming { get; set; }
        [DataMember] public List<TrajectoryStreamingConfiguration> TrajectoryConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> TrajectoryApiConfiguration { get; set; }
        [DataMember] public HeartbeatConfiguration HeartbeatConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> SensorOffsetTableStreamingConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> RelogStreamingConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> FluidsReportConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> RigModelConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> RigUtilizationConfiguration { get; set; }
        [DataMember] public IndexShiftConfiguration IndexShiftConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> WellboreStabilityConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> FormationTemperatureConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> GeomechanicsMemConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> DrillingParameterConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> RiskConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> WellboreMarkerSetConfiguration { get; set; }
        [DataMember] public List<ContextStreamingCommand> ToleranceTunnelConfiguration { get; set; }
        [DataMember] public List<TimeSeriesStreamingCommand> CustomizedTimeSeriesConfiguration { get; set; }
        [DataMember] public List<AlarmStreamingFolderCommand> AlarmFolderConfiguration { get; set; }
        [DataMember] public List<TimeSeriesStreamingFolderCommand> TimeSeriesFolderConfiguration { get; set; }
        [DataMember] public List<TimeObjectsStreamingFolderCommand> TimeObjectsFolderConfiguration { get; set; }
    }

    public class ContextStreamingFolderCommand
    {
        public string DataFolder { get; set; }
    }

    public class TimeObjectsStreamingFolderCommand : ContextStreamingFolderCommand
    {
        public string Type { get; set; }
    }
    public class TimeSeriesStreamingFolderCommand : ContextStreamingFolderCommand
    {
        public string Type { get; set; }
        public string SubType { get; set; }
    }

    public class AlarmStreamingFolderCommand : ContextStreamingFolderCommand
    {
        public string Type { get; set; }
    }

    public class IndexShiftConfiguration
    {
        public DateTimeOffset TargetStartTime { get; set; }
    }

    public class TimeSeriesStreamingCommand : ContextStreamingCommand
    {
        public string Type { get; set; }
        public string SubType { get; set; }
    }

    public class StreamingRange
    {
        public DateTimeOffset StartTime { get; set; }
        /// <summary>
        /// The replay time range covered in an iot message.
        /// </summary>
        public int SecondsPerMessage { get; set; }
        public double SecondsBetweenMessages { get; set; }
    }

    public class ContextStreamingCommand
    {
        public DateTimeOffset OperationTime { get; set; }
        public string FileName { get; set; }
    }

    [DataContract]
    public class ChannelStreamingConfiguration
    {
        [DataMember] public string CsvFileName { get; set; }
        [DataMember] public string ContainerType { get; set; } = "Well";
        [DataMember] public int RepeatTime { get; set; }
        [DataMember] public double DepthDeltaForProjection { get; set; }
        [DataMember] public InputWorkflow InputWorkflow { get; set; } = InputWorkflow.prismConnect;
        [DataMember] public bool IsSlowMode { get; set; }
        [DataMember] public int MaxChunkSize { get; set; } = 10;
        [DataMember] public string Version { get; set; }
        [DataMember] public string DataFolder { get; set; }
    }

    public class JobSetupConfiguration
    {
        public string JobName { get; set; }
        public string JobSetupFile { get; set; }
    }

    public class BhaRunStreamingCommand
    {
        public DateTimeOffset OperationTime { get; set; }
        public string BhaRunFileName { get; set; }
        public string TubularFileName { get; set; }
        public BhaStatus StatusBha { get; set; }
        public InputWorkflow InputWorkFlow { get; set; } = InputWorkflow.prismConnect;
    }

    public enum BhaStatus
    {
        //
        // Summary:
        //     final property
        final,
        //
        // Summary:
        //     progress property
        progress,
        //
        // Summary:
        //     plan property
        plan
    }

    public enum InputWorkflow
    {
        prismConnect = 0,
        rtc10 = 1,
        etp11 = 2
    }

    [DataContract]
    public class TrajectoryStreamingConfiguration
    {
        [DataMember]
        public string CsvWithTimeStamp { get; set; }
        [DataMember]
        public InputWorkflow InputWorkflow { get; set; } = InputWorkflow.prismConnect;
    }

    [DataContract]
    public class HeartbeatConfiguration
    {
        [DataMember]
        public DateTimeOffset StartTime { get; set; }
        [DataMember]
        public int TimeIntervalInSeconds { get; set; }
    }

    [DataContract]
    public class PlanConfiguration
    {
        [DataMember]
        public string PlanTrajectory { get; set; }

        [DataMember]
        public string ActivityPlanJson { get; set; }
    }
}
