﻿using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Operations
{
    public class AssignmentConflictDetector
    {
        private ICourseRepository courseRepository;

        public AssignmentConflictDetector(ICourseRepository courseRepo)
        {
            courseRepository = courseRepo;
        }

        public List<Conflict> AllConflicts()
        {
            var courseGroupByRoom = from course in courseRepository.Courses
                                    where course.AlreadyAssignedRoom
                                    group course by course.RoomAssignment;

            List<Conflict> conflicts = new List<Conflict>();
            foreach (var roomGroup in courseGroupByRoom)
            {
                List<Course> courses = roomGroup.ToList();

                List<int> indicesUsed = new List<int>();
                for (int i = 0; i < courses.Count; i++)
                {
                    List<Course> conflictingCourses = new List<Course>();
                    for (int j = i+1; j < courses.Count && !indicesUsed.Contains(j); j++)
                    {
                        bool conflictExists = ConflictBetweenCourses(courses[i], courses[j]);
                        if (conflictExists)
                        {
                            conflictingCourses.Add(courses[j]);
                            indicesUsed.Add(j);
                        }
                    }

                    if (conflictingCourses.Count != 0)
                    {
                        conflictingCourses.Add(courses[i]);
                        conflicts.Add(new Conflict(conflictingCourses));
                    }
                }
            }

            return conflicts;
        }

        private bool ConflictBetweenCourses(Course courseA, Course courseB)
        {
            bool candidate = courseA.MeetingDays?.Any(x => courseB.MeetingDays?.Contains(x) == true) == true;
            if (!candidate) return false;

            bool hasConflict = true;
            if (courseA.EndTime <= courseB.StartTime) hasConflict = false;
            else if (courseB.EndTime <= courseA.StartTime) hasConflict = false;

            return hasConflict;
        }


        /// <summary>
        /// Finds conflicts involving the <paramref name="courses"/> and the rest of the courses in the CourseRepo
        /// </summary>
        /// <param name="courses"></param>
        /// <returns></returns>
        public List<Conflict> ConflictsInvolvingCourses(List<Course> courses)
        {
            List<Conflict> allConflicts = AllConflicts();
            return allConflicts.FindAll(x => x.ConflictingCourses.Intersect(courses).FirstOrDefault() != null ? true : false);
        }


        /// <summary>
        /// Return conflicts solely among the <paramref name="courses"/>
        /// </summary>
        /// <param name="courses"></param>
        /// <returns></returns>
        public List<Conflict> ConflictsAmongCourses(List<Course> courses)
        {
            return new List<Conflict>();
        }

    }
}
