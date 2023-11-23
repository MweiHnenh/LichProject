using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Calendar
{
    public partial class UserControlDays : UserControl
    {
        private IEventContainer eventContainer;
        public static string static_day;
        public event EventHandler DayClicked;

        public void SetEventContainer(IEventContainer container)
        {
            eventContainer = container;
        }

        public UserControlDays()
        {
            InitializeComponent();
        }

        private void UserControlDays_Load(object sender, EventArgs e)
        {

        }
        public void SetBackground(int hidden)
        {
            if (hidden == 1)
            {
                this.BackColor = SystemColors.Control;
                this.BorderStyle = BorderStyle.None;
                this.lbDays.Text = " ";
                this.LunarDate.Text = " ";
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
            int[] lunarDate = cs.convertSolar2Lunar(numday, month, year, 7);
            LunarDate.Text = $"{lunarDate[0]}/{lunarDate[1]}";
        }

        private void UserControlDays_Click(object sender, EventArgs e)
        {
            // Check if an event exists for the selected date
            string selectedDate = $"{Form2.static_year}/{Form2.static_month}/{static_day}";
            string eventDescription = EventFileHandler.LoadEvent(selectedDate);

            if (!string.IsNullOrEmpty(eventDescription))
            {
                MessageBox.Show($"Event on {selectedDate}: {eventDescription}");
            }
        }

        public void DisplayEvent()
        {
            DayClicked?.Invoke(this, EventArgs.Empty);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DisplayEvent();
        }

        public void SetBackgroundForToday()
        {
            this.BackColor = Color.Gray;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public void lbDays_Click(object sender, EventArgs e)
        {

        }

        private void LunarDate_Click(object sender, EventArgs e)
        {

        }
        public static void SaveEvent(string eventDescription)
        {

        }
        public static string LoadEvent()
        {
            return ""; 
        }
    }
}