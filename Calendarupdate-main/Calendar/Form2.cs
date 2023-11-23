using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Calendar
{
    public enum ThemeEnum
    {
        WildAnimal = 1,
        Forest = 2,
        Ocean = 3
    }

    public partial class Form2 : Form
    {
        int month, year;
        public static int static_month, static_year;
        //default theme
        private ThemeEnum currentTheme = ThemeEnum.Ocean;
        private bool userSelectedTheme = false;

        public Form2()
        {
            InitializeComponent();
            // Wire up the btnToday_Click method to the Click event of the button
            btnToday.Click += new EventHandler(this.btnToday_Click);

            DisplayCalendar(DateTime.Now.Month, DateTime.Now.Year);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            display_all(2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display_all(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            display_all(1);
        }

        private void LbMONTH_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        //Property for display button
        void display_all(int pre_next)
        {
            dayContainer.Controls.Clear();
            //case conditions to implement calendar
            if (pre_next == 2)
            {
                month = DateTime.Now.Month;
                year = DateTime.Now.Year;
            }

            if (pre_next == 0)
                month--;
            if (pre_next == 1)
                month++;

            if (month > 12)
            {
                month = 1;
                year++;
            }
            if (month <= 0)
            {
                month = 12;
                year--;
            }

            static_month = month;
            static_year = year;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);

            LbMONTH.Text = monthname + " " + year;
            //Modify the first day of the month to start in exact day
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;

            int days = DateTime.DaysInMonth(year, month);

            int i = 1;
            int count = 0;

            int daysLastMonth = DateTime.DaysInMonth(year, DateTime.Now.Month - 1);
            int daysNextMonth = DateTime.DaysInMonth(year, DateTime.Now.Month + 1);

            //Phase 1: Process the last day of last month and the first day of current day
            if (i < (int)dayOfWeek)
            {
                int lastdaysPrevMonth = (int)dayOfWeek - daysLastMonth;
                int datePrev = lastdaysPrevMonth * (-1);

                for (int y = datePrev; y < daysLastMonth; y++)
                {
                    UserControlDays ucdays = new UserControlDays();
                    ucdays.days(datePrev + 1, month, year);
                    ucdays.SetBackground(1);
                    dayContainer.Controls.Add(ucdays);
                    datePrev++;
                    count++;
                }
            }
            //Phase 2: Process the day of current month
            for (int k = 1; k <= days; k++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(k, month, year);
                dayContainer.Controls.Add(ucdays);
                count++;
            }
            //Phase 3: Process the last day of current month and the first day of next month
            for (int j = 1; j <= daysNextMonth; j++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(j, month, year);
                ucdays.SetBackground(1);
                dayContainer.Controls.Add(ucdays);
                count++;
            }

            ////////////////////////////////////////////////////////////////////////////////////
            ////////set condition for lunar calendar//////
            LunarCalendar cs = new LunarCalendar();

            int lastDaysOfMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1);
            int daysToAddFromPreviousMonth = lastDaysOfMonth - (int)DateTime.Now.DayOfWeek;
            //Phase 1
            for (int j = 0; j < daysToAddFromPreviousMonth; j++)
            {
                int[] currentLunarDate = cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j + 1).Day,
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j + 1).Month,
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j + 1).Year, 7);

                if (currentLunarDate[1] != cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j - 2).Day,
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j - 2).Month,
                    DateTime.Now.AddDays(-daysToAddFromPreviousMonth + j - 2).Year, 7)[1])
                {
                    currentLunarDate[1]--;
                }

                count++;

                UserControlDays ucdays = new UserControlDays();
                ucdays.SetBackground(1);
            }
            //Phase 2
            int daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int j = 0; j < daysInCurrentMonth; j++)
            {
                int[] currentLunarDate = cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(j + 1).Day,
                    DateTime.Now.AddDays(j + 1).Month,
                    DateTime.Now.AddDays(j + 1).Year, 7);

                if (currentLunarDate[1] != cs.convertSolar2Lunar(
                    DateTime.Now.AddDays(j).Day,
                    DateTime.Now.AddDays(j).Month,
                    DateTime.Now.AddDays(j).Year, 7)[1])
                {
                    currentLunarDate[1]--;
                }

                count++;
            }
            //Phase 3
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
                    currentLunarDate[1]--;
                }

                count++;

                UserControlDays ucdays = new UserControlDays();
                ucdays.SetBackground(1);
            }

            UpdateTheme();
        }
        //change theme
        private void button4_Click(object sender, EventArgs e)
        {
            ChangeTheme changeThemeForm = new ChangeTheme();
            changeThemeForm.ShowDialog();

            if (changeThemeForm.DialogResult == DialogResult.OK)
            {
                currentTheme = changeThemeForm.SelectedTheme;
                userSelectedTheme = true;
                UpdateTheme();
                userSelectedTheme = false;
            }
        }
        //property to get or set the current theme
        public ThemeEnum ThemeEnum
        {
            get
            {
                return currentTheme;
            }
            set
            {
                currentTheme = value;
                UpdateTheme();
            }
        }

        private void UpdateTheme()
        {
            //implement logic to update the theme based on currentTheme
            switch (currentTheme)
            {
                case ThemeEnum.WildAnimal:
                    this.BackgroundImage = Properties.Resources.bird;
                    break;
                case ThemeEnum.Forest:
                    this.BackgroundImage = Properties.Resources.forest;
                    break;
                case ThemeEnum.Ocean:
                    this.BackgroundImage = Properties.Resources.ocean;
                    break;
            }
            //implement condition to update theme based on month and allow users change theme like they want
            if (!userSelectedTheme)
            {
                int selectedMonth = static_month;

                switch (selectedMonth)
                {
                    case int month when (month >= 1 && month <= 3):
                        this.BackgroundImage = Properties.Resources.spring;
                        break;
                    case int month when (month >= 4 && month <= 6):
                        this.BackgroundImage = Properties.Resources.summer;
                        break;
                    case int month when (month >= 7 && month <= 9):
                        this.BackgroundImage = Properties.Resources.fall;
                        break;
                    case int month when (month >= 10 && month <= 12):
                        this.BackgroundImage = Properties.Resources.winter;
                        break;
                }
            }
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            display_all(2);
            //display the calendar for the current month and year
            DisplayCalendar(DateTime.Now.Month, DateTime.Now.Year);
        }

        //display the calendar for a specific month and year
        private void DisplayCalendar(int month, int year)
        {
            HighlightTodayBlock();
        }

        private void HighlightTodayBlock()
        {
            DateTime today = DateTime.Today;

            foreach (Control control in dayContainer.Controls)
            {
                if (control is UserControlDays)
                {
                    UserControlDays userControlDay = (UserControlDays)control;

                    string dayString = userControlDay.lbDays.Text.Trim();

                    int month = static_month;
                    int year = static_year;

                    if (dayString == today.Day.ToString() && month == today.Month && year == today.Year)
                    {
                        userControlDay.SetBackgroundForToday();
                    }
                    else
                    {
                        //userControlDay.SetBackground(2);
                    }
                }
            }
        }
    }
}
