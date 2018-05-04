﻿using ClassroomAssignment.Model;
using ClassroomAssignment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassroomAssignment.Windows
{
    /// <summary>
    /// Interaction logic for AssignmentPage.xaml
    /// </summary>
    public partial class AssignmentPage : Page
    {
        private AssignmentViewModel viewModel;

        public AssignmentPage(List<Course> courses)
        {

            InitializeComponent();
            viewModel = new AssignmentViewModel(courses);

            DataContext = viewModel;

            CourseDetailControl.SetCourse(viewModel.CurrentCourse);
            AvailableRoomsListView.ItemsSource = viewModel.AvailableRooms;
            RoomSchedule.RoomScheduled = viewModel.CurrentRoom;
            RoomSchedule.CoursesForRoom = viewModel.CoursesForSelectedRoom;
            RoomSchedule.AvailableScheduleSlots = viewModel.AvailableSlots;
        }



        private void AssignCoursesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Course course = AssignCoursesListView.SelectedItem as Course;

            CourseDetailControl.SetCourse(course);

        }

        
    }
}
