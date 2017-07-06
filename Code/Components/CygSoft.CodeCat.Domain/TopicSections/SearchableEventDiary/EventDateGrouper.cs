using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.TopicSections.SearchableEventDiary
{
    internal static class EventDateGrouper
    {
        // Adapted from...
        // https://www.codeproject.com/Articles/35440/How-to-mimic-Outlook-group-naming

        public static string GetGroup(DateTime actualDateTime, DateTime eventDateTime)
        {
            int eventWeekDay = (Convert.ToInt32(eventDateTime.DayOfWeek) + 6) % 7;
            int actualWeekDay = (Convert.ToInt32(actualDateTime.DayOfWeek) + 6) % 7;
            int timeSpan = (actualDateTime.Date - eventDateTime.Date).Days;

            if (timeSpan == 0)
                return "Today";

            if (timeSpan == 1)
                return "Yesterday";

            if (timeSpan == -1)
                return "Tomorrow";

            // same week 
            if ((timeSpan > 1) && (timeSpan <= actualWeekDay))
                return GetDayOfWeek(eventWeekDay);

            // previous / next weeks
            for (int index = 0; index <= 3; index++)
            {
                if ((timeSpan > actualWeekDay + index * 7) && (timeSpan <= actualWeekDay + (index + 1) * 7))
                {
                    return GetPreviousWeek(index);
                }
            }
            
            if (actualDateTime.Month - eventDateTime.Month == 1 && actualDateTime.Year == eventDateTime.Year)
                return "Last Month";

            return "Older";
        }

        private static string GetPreviousWeek(int index)
        {
            int lookupIndex = index > 5 ? 5 : index;
            string[] periods = new string[] { "Last Week", "2 Weeks Ago", "3 Weeks Ago", "4 Weeks Ago"};
            return periods[lookupIndex];
        }

        private static string GetDayOfWeek(int eventDay)
        {
            string[] weekDays = new string[] { "Monday","Tuesday","Wednesday","Thursday","Friday", "Saturday", "Sunday" };
            return weekDays[eventDay];
        }

        public class EventPeriod
        {
            public readonly string Title;
            public readonly int Ordinal;

            internal EventPeriod(int ordinal, string title)
            {
                this.Ordinal = ordinal;
                this.Title = title;
            }
        }

        public static string[] Groups
        {
            get
            {
                return new string[]
                {
                    "Today",
                    "Yesterday",
                    "Tomorrow",
                    "Sunday",
                    "Saturday",
                    "Friday",
                    "Thursday",
                    "Wednesday",
                    "Tuesday",
                    "Monday",
                    "Last Week",
                    "2 Weeks Ago",
                    "3 Weeks Ago",
                    "4 Weeks Ago",
                    "Last Month",
                    "Older",
                };
            }
        }

        //public static EventPeriod[] Groups()
        //{
        //    return new EventPeriod[]
        //    {
        //        new EventPeriod(0, "Today"),
        //        new EventPeriod(1, "Yesterday"),
        //        new EventPeriod(2, "Tomorrow"),
        //        new EventPeriod(3, "Sunday"),
        //        new EventPeriod(4, "Saturday"),
        //        new EventPeriod(5, "Friday"),
        //        new EventPeriod(6, "Thursday"),
        //        new EventPeriod(7, "Wednesday"),
        //        new EventPeriod(8, "Tuesday"),
        //        new EventPeriod(9, "Monday"),
        //        new EventPeriod(10, "Last Week"),
        //        new EventPeriod(11, "2 Weeks Ago"),
        //        new EventPeriod(12, "3 Weeks Ago"),
        //        new EventPeriod(13, "4 Weeks Ago"),
        //        new EventPeriod(14, "Last Month"),
        //        new EventPeriod(15, "Older"),
        //    };
        //}

        //private static int FindGroup(DateTime actualDateTime, DateTime eventDateTime)
        //{
        //    DateTime dtSource = eventDateTime;
        //    DateTime dtRef = actualDateTime;

        //    // Because week starts on Monday in France
        //    int dtWeekDay = (Convert.ToInt32(dtSource.DayOfWeek) + 6) % 7;
        //    int dtRefWeekDay = (Convert.ToInt32(dtRef.DayOfWeek) + 6) % 7;

        //    int timeSpan = (dtRef.Date - dtSource.Date).Days;

        //    if (timeSpan == 0)
        //        return 0;  // same day
        //    if (timeSpan == 1)
        //        return -1; // day before

        //    if (timeSpan == -1)
        //        return 1; // day after

        //    // same week 
        //    if ((timeSpan > 1) && (timeSpan <= dtRefWeekDay))
        //        return dtWeekDay - 8;

        //    if ((-timeSpan > 1) && (-timeSpan <= 6 - dtRefWeekDay))
        //        return dtWeekDay + 2;

        //    // previous / next weeks
        //    for (int index = 0; index <= 3; index++)
        //    {
        //        if ((timeSpan > dtRefWeekDay + index * 7) &&
        //            (timeSpan <= dtRefWeekDay + (index + 1) * 7))
        //            if (dtRef.Month == dtSource.Month)
        //                return -(9 + index);
        //            else
        //                return -13;
        //        if ((-timeSpan > 6 - dtRefWeekDay + index * 7) &&
        //            (-timeSpan <= 6 - dtRefWeekDay + (index + 1) * 7))
        //            if (dtRef.Month == dtSource.Month)
        //                return (9 + index);
        //            else
        //                return 13;
        //    }

        //    if (Math.Abs(dtSource.Month - dtRef.Month) == 1)
        //        return Math.Sign(timeSpan) * (-13);
        //    return Math.Sign(timeSpan) * (-14);

        //}
    }
}
