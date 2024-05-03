namespace Graduation_Project.Domain.Entity.UserDomain
{
    public class TimeSession
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string AmPm { get; set; }

        public TimeSession(int hour, int minute, string amPm)
        {
            Hour = hour;
            Minute = minute;
            AmPm = amPm;
        }

        public override string ToString()
        {
            return $"{Hour:00}:{Minute:00} {AmPm}";
        }

        public static TimeSession Parse(string time)
        {
            var parts = time.Split(':', ' ');
            int hour = int.Parse(parts[0]);
            int minute = int.Parse(parts[1]);
            string amPm = parts[2];
            return new TimeSession(hour, minute, amPm);
        }



    }
}
