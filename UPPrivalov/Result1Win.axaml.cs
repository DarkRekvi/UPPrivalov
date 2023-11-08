using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPPrivalov;

public partial class Result1Win : Window
{
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    
    private MySqlConnection _connection;
    public Result1Win()
    {
        InitializeComponent();
        CountBlock.Text = getResults().ToString();
        AmountBlock.Text = getAmount().ToString() + " руб";
    }

    private int getResults()
    {
        DB db = new DB();
        using (db.getConnection())
        {
            db.OpenConnection();
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM Payments", db.getConnection()))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                db.CloseConnection();
                return count;
            }
            
        }
    }

    private double getAmount()
    {
        double sum = 0;
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT Sum FROM Payments", _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            sum += reader.GetDouble("Sum");
        }
        _connection.Close();
        return sum;
    }
    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }
}