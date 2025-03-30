namespace LogAnalizerServer.Models;


public enum LogWeekType
{
    Week1 = 1,
    Week2 = 2,
    Week3 = 3,
    Week4 = 4,
    // Добавляй по мере необходимости
}

    public class AlarmLog
    {
        public int Id { get; set; } 

        public DateTime TimeWhenLogged { get; set; }
        public DateTime LocalZoneTime { get; set; }

        public long SequenceNumber { get; set; }
        public string AlarmId { get; set; } 
        public string AlarmClass { get; set; } = null!;
        public string Resource { get; set; } = null!;
        public string LoggedBy { get; set; } = null!;
        public string Reference { get; set; } = null!;

        public string PrevState { get; set; } = null!;
        public string LogAction { get; set; } = null!;
        public string FinalState { get; set; } = null!;

        public string AlarmMessage { get; set; } = null!;
        public DateTime GenerationTime { get; set; }
        public DateTime GenerationTimeUtc { get; set; }

        public string Project { get; set; } = null!;
        public LogWeekType WeekType { get; set; } // Добавили тип недели
    }
