﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Operations
{
    /// <summary>
    /// This file search parameters, calculate duration for courses.
    /// </summary>
    public struct SearchParameters
    {
        public IEnumerable<DayOfWeek> MeetingDays;
        public TimeSpan StartTime;
        public TimeSpan EndTime;
        public TimeSpan Duration;
        public int Capacity;
        /// <summary>
        /// Calculate duration for the class.
        /// </summary>
        /// <param name="meetingDays"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="capacity"></param>
        /// <param name="duration"></param>
        public SearchParameters(IEnumerable<DayOfWeek> meetingDays, TimeSpan startTime, TimeSpan endTime, int capacity = int.MaxValue, TimeSpan duration = new TimeSpan())
        {
            MeetingDays = meetingDays;
            StartTime = startTime;
            EndTime = endTime;
            Capacity = capacity;

            if (duration.TotalMinutes == 0) Duration = endTime - startTime;
            else Duration = duration;
        }


    }
}
