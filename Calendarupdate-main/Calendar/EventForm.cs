using System;
using System.IO;
using System.Windows.Forms;

namespace Calendar
{
    public partial class EventForm : Form
    {
        private string eventDescription = string.Empty;

        public EventForm(string date)
        {
            InitializeComponent();
            textDate.Text = date;

            // Load the event if it exists
            eventDescription = LoadEvent(date);
            textEvent.Text = eventDescription;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string date = textDate.Text;
            string updatedEventDescription = textEvent.Text;

            SaveEvent(date, updatedEventDescription);
            MessageBox.Show("Your event is saved successfully.");
            this.Close();
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

        private void SaveEvent(string date, string updatedEventDescription)
        {
            string filePath = Path.Combine("Events", $"{date}.txt");

            try
            {
                Directory.CreateDirectory("Events");

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine(updatedEventDescription);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving event: {ex.Message}");
            }
        }

        private void textDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void textEvent_TextChanged(object sender, EventArgs e)
        {

        }

        private void EventForm_Load(object sender, EventArgs e)
        {
            // Set the date based on the selected day
            textDate.Text = $"{Form2.static_year}/{Form2.static_month}/{UserControlDays.static_day}";
        }
    }
}
