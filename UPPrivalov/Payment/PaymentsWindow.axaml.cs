using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPPrivalov;

public partial class PaymentsWindow : Window
{
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    
    private List<PaymentClass> _payments;
    private MySqlConnection _connection;
    private string fulltable = "SELECT Payment_ID, Course, Student, Sum, Payments.Payment_Type, Payment_Types.Payment_Type_Name FROM Payments " +
                               "JOIN Payment_Types on Payments.Payment_Type = Payment_Types.Payment_Type_Name";
    
    public PaymentsWindow()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _payments = new List<PaymentClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new PaymentClass()
            {
                PaymentID = reader.GetInt32("Payment_ID"),
                CourseName = reader.GetString("Course"),
                Student = reader.GetString("Student"),
                CourseSum = reader.GetDouble("Sum"),
                Payment_type = reader.GetString("Payment_Type_Name")
            };
            _payments.Add(current);
        }
        _connection.Close();
        PaymentsDataGrid.ItemsSource = _payments;
    }

    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }
}