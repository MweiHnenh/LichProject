using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Calendar
{
    public partial class EventForm : Form
    {
        // create a connection string
        String connString = "server=localhost;user id=root;database=db_calendar"; // Add your MySQL password
        //Create a database using by xampp

        public EventForm()
        {
            InitializeComponent();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //MySqlConnection conn = new MySqlConnection(connString);//option not supported, parameter name:ss1 mode
            //conn.Open();
            //String sql = "INSERT INTO db_calendar(date,event)values(?,?)";
            //MySqlCommand cmd = conn.CreateCommand();
            //cmd.CommandText = sql;
            //cmd.Parameters.AddWithValue("date",textDate.Text);
            //cmd.Parameters.AddWithValue("event",textEvent.Text);
            //cmd.ExecuteNonQuery();
            //MessageBox.Show("Your event is saved successfully.");
            //cmd.Dispose();
            //conn.Close();

            using (MySqlConnection conn = new MySqlConnection(connString)) //option not supported, parameter name:ss1 mode
            {
                try
                {
                    conn.Open();
                    String sql = "INSERT INTO db_calendar(date, event) VALUES (@date, @event)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@date", textDate.Text);
                    cmd.Parameters.AddWithValue("@event", textEvent.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Your event is saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void EventForm_Load(object sender, EventArgs e)
        {
            //call the static variable declare
            textDate.Text = Form2.static_year + "/" + Form2.static_month + "/" + UserControlDays.static_day;
        }

        public void HandleEvent(IDayView dayView)
        {
            // Handle event based on the specific day view
            dayView.DisplayEvent();
        }
    }
}
