namespace LogAnalizerShared;
using System;
public class AlarmlogClient
{
    public int Id { get; set; }
    public DateTime TimeWhenLogged { get; set; }
    public DateTime LocalZoneTime { get; set; }
    public long SequenceNumber { get; set; }
    public string AlarmId { get; set; }
    public string AlarmClass { get; set; }
    public string Resource { get; set; }
    public string LoggedBy { get; set; }
    public string Reference { get; set; }
    public string PrevState { get; set; }
    public string LogAction { get; set; }
    public string FinalState { get; set; }
    public string AlarmMessage { get; set; }
    public DateTime GenerationTime { get; set; }
    public DateTime GenerationTimeUtc { get; set; }
    public string Project { get; set; }
    public LogWeekType WeekType { get; set; }
}