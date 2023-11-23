using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Calendar
{
    public partial class UserControlDays : UserControl
    {
        public static string static_day;
        public event EventHandler DayClicked;

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
            string selectedDate = $"{Form2.static_year}/{Form2.static_month}/{static_day}";
            EventForm eventForm = new EventForm(selectedDate);
            eventForm.ShowDialog();
        }

        private string LoadEvent(string date)
        {
            string filePath = Path.Combine("Events", $"{date}.txt");

            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading event: {ex.Message}");
            }
            return string.Empty;
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

        public string GetEvent()
        {
            string selectedDate = $"{Form2.static_year}/{Form2.static_month}/{static_day}";
            return LoadEvent(selectedDate);
        }
    }
}
