using System;
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
        //create a static variable that we can pass to another form for month and year
        public static int static_month, static_year;

        //defaut theme
        private ThemeEnum currentTheme = ThemeEnum.Ocean;
        public Form2()
        {
            InitializeComponent();
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

        public static DayOfWeek DesiredDayOfWeek = DateTime.Today.DayOfWeek;

        //close
        private void button3_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        //property for display-button
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

            static_month = month;//convert string to int
            static_year = year;//convert string to int
            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);

            LbMONTH.Text = monthname + " " + year;

            // Modify the first day of the month to start in exact day
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;

            int days = DateTime.DaysInMonth(year, month);

            int i = 1;
            int count = 0;

            //days in previous month
            int daysLastMonth = DateTime.DaysInMonth(year, DateTime.Now.Month - 1);

            //days in next month
            int daysNextMonth = DateTime.DaysInMonth(year, DateTime.Now.Month + 1);
            //MessageBox.Show($"{daysNextMonth}");

            //phase 1: the last day of previous month -> the first day of current month 
            if (i < (int)dayOfWeek)
            {
                //last days from previous months
                int lastdaysPrevMonth = (int)dayOfWeek - daysLastMonth;
                int datePrev = lastdaysPrevMonth * (-1);
                //MessageBox.Show($"{datePrev}");

                //adding previous month
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
            //phase 2: the ongoing day of current month -> the last day of current month
            for (int k = 1; k <= days; k++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(k, month, year);
                dayContainer.Controls.Add(ucdays);
                count++;
            }
            //phase 3: the last day of current month -> the first day of next month
            for (int j = 1; j <= daysNextMonth; j++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(j, month, year);
                ucdays.SetBackground(1);
                dayContainer.Controls.Add(ucdays);
                count++;
            }
        }

        //change theme
        private void button4_Click(object sender, EventArgs e)
        {
            //Open the ChangeThemeForm
            ChangeTheme changeThemeForm = new ChangeTheme();
            changeThemeForm.ShowDialog();

            //Handle the result from the ChangeThemeForm if needed
            if (changeThemeForm.DialogResult == DialogResult.OK)
            {
                //The user clicked "Save", update the theme based on the selected theme in ChangeThemeForm
                currentTheme = changeThemeForm.SelectedTheme;
                UpdateTheme();
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
                UpdateTheme(); //call method to update the theme
            }
        }
        //add method to update theme
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
        }
        
    }
}
