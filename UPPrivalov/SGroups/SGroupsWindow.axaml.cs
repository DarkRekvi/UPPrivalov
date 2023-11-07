using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using UPPrivalov.Students;

namespace UPPrivalov.SGroups;

public partial class SGroupsWindow : Window
{
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    
    private List<SGroupClass> _groups;
    private MySqlConnection _connection;

    private string fulltable =
        "SELECT Group_ID, Group_Name, SGroups.Pedagogue, Teachers.TeacherName, Max_Students, SGroups.Course, Courses.Course_Name FROM SGroups " +
        "JOIN Teachers on SGroups.Pedagogue = Teachers.Teacher_ID " +
        "JOIN Courses on SGroups.Course = Courses.Course_ID";
    
    public SGroupsWindow()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _groups = new List<SGroupClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new SGroupClass()
            {
                GroupID = reader.GetInt32("Group_ID"),
                GroupName = reader.GetString("Group_Name"),
                GroupTeacher = reader.GetString("TeacherName"),
                MaxStudents = reader.GetInt32("Max_Students"),
                Course = reader.GetString("Course_Name")
            };
            _groups.Add(current);
        }
        _connection.Close();
        SGroupsDataGrid.ItemsSource = _groups;
    }
    
    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }
    
    private void Add(object? sender, RoutedEventArgs e)
    {
        SGroupAddWindow mwin = new SGroupAddWindow();
        mwin.Show();
        this.Close();
    }
}