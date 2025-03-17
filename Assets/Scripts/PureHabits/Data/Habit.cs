using System;
using System.Collections.Generic;

namespace PureHabits.Data
{
    [Serializable]
    public class Habit
    {
        public string Name;
        public string Desc;
        public int IconId;
        public DateTime CreateDate;
        public int Interval;
        public bool Positive;
        public List<MarkDate> MarkDates;
    }

    public class MarkDate
    {
        public DateTime DateTime;
        public bool Completed;
        public bool Marked;
    }
}