using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Calendar
{
    public partial class UserControlDays : UserControl, IDayView
    {
        //create more static variable for days
        public static string static_day;

        // create a connection string
        String connString = "server=localhost;user id=root;database=db_calendar"; // Add your MySQL password

        public UserControlDays()
        {
            InitializeComponent();
        }

        private void UserControlDays_Load(object sender, EventArgs e)
        {
            LunarCalendar cs = new LunarCalendar();

            int count = 0;

            // Phase 1: Handle the last days of the previous month
            int lastDaysOfMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1);
            int daysToAddFromPreviousMonth = lastDaysOfMonth - (int)DateTime.Now.DayOfWeek;

            for (int j = 0; j < daysToAddFromPreviousMonth; j++)
            {
                int[] currentLunarDate = cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j + 1).Day,
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j + 1).Month,
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j + 1).Year, 7);

                // Handle the case where the lunar month needs adjustment
                if (currentLunarDate[1] != cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j - 2).Day,
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j - 2).Month,
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j - 2).Year, 7)[1])
                {
                    currentLunarDate[1]--; // Adjust lunar month if needed
                }

                count++;

            }

            // Phase 2: Handle the days of the current month
            int daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int j = 0; j < daysInCurrentMonth; j++)
            {
                int[] currentLunarDate = cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(j + 1).Day,
                    DateTime.Now.AddDays(j + 1).Month,
                    DateTime.Now.AddDays(j + 1).Year, 7);

                // Handle the case where the lunar month needs adjustment
                if (currentLunarDate[1] != cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(j).Day,
                    DateTime.Now.AddDays(j).Month,
                    DateTime.Now.AddDays(j).Year, 7)[1])
                {
                    currentLunarDate[1]--; // Adjust lunar month if needed
                }

                count++;
            }

            // Phase 3: Handle the first days of the next month
            int daysToAddToNextMonth = (7 - count) % 7;

            for (int j = 0; j < daysToAddToNextMonth; j++)
            {
                int[] currentLunarDate = cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(daysInCurrentMonth + j + 1).Day,
                    DateTime.Now.AddDays(daysInCurrentMonth + j + 1).Month,
                    DateTime.Now.AddDays(daysInCurrentMonth + j + 1).Year, 7);

                if (currentLunarDate[1] != cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(daysInCurrentMonth + j).Day,
                    DateTime.Now.AddDays(daysInCurrentMonth + j).Month,
                    DateTime.Now.AddDays(daysInCurrentMonth + j).Year, 7)[1])
                {
                    currentLunarDate[1]--; // Adjust lunar month if needed
                }

                count++;

            }
        }


        public void SetBackground(int hidden)
        {
            if (hidden == 1)
            {
                this.BackColor = SystemColors.Control;
                this.BorderStyle = BorderStyle.None;
                this.lbDays.Text = "";
            }
            else
            {
                this.BackColor = Color.White;
                this.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        public void days(int numday, int month, int year)
        {
            lbDays.Text = numday + " ";
            LunarCalendar cs = new LunarCalendar();
            int[] lunarDate = cs.convertSolar2Lunar(numday, month, year,7);
            LunarDate.Text = $"{lunarDate[0]}/{lunarDate[1]}";
        }
        //create add event 
        private void UserControlDays_Click(object sender, EventArgs e)
        {
            static_day = lbDays.Text;
            //start timer if usercontrol is click
            timer1.Start();
            EventForm eventForm = new EventForm();
            eventForm.Show();

            //pass the current instance of UserControlDays to EventForm
            eventForm.HandleEvent(this);
        }
        //create a new method to display event
        public void DisplayEvent()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            String sql = "SELECT * FROM db_calendar where date = ?";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("date", Form2.static_year + "/" + Form2.static_month + "/" + UserControlDays.static_day + "-" + lbDays.Text);
            //MySqlDataReader reader = cmd.ExecuteReader();

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    lbEvent.Text = reader["event"].ToString();
                }
            }
            cmd.Dispose();
            conn.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //call the display method 
            DisplayEvent();
        }

        private void lbDays_Click(object sender, EventArgs e)
        {

        }

        private void LunarDate_Click(object sender, EventArgs e)
        {

        }
    }
}
