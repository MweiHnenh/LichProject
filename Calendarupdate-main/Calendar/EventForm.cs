using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Calendar
{
    public partial class EventForm : Form, IEventContainer
    {
        private Dictionary<string, string> events = new Dictionary<string, string>();

        public EventForm()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Get the date from the text box
            string date = textDate.Text;

            // Get the event description from the text box
            string eventDescription = textEvent.Text;

            // Save the event using the existing method in UserControlDays
            UserControlDays.static_day = date;
            UserControlDays.SaveEvent(eventDescription);

            MessageBox.Show("Your event is saved successfully.");
        }

        private void EventForm_Load(object sender, EventArgs e)
        {
            // Set the date based on the selected day
            textDate.Text = Form2.static_year + "/" + Form2.static_month + "/" + UserControlDays.static_day;

            // Load the event using the existing method in UserControlDays
            UserControlDays.static_day = textDate.Text;
            string eventDescription = UserControlDays.LoadEvent();

            // Check if an event exists for the selected date
            if (!string.IsNullOrEmpty(eventDescription))
            {
                // If an event exists, display it
                textEvent.Text = eventDescription;
            }
        }

        public void AddEvent(string date, string eventName)
        {
            if (events.ContainsKey(date))
            {
                events[date] = eventName;
                MessageBox.Show("Your event is updated successfully.");
            }
            else
            {
                events.Add(date, eventName);
                MessageBox.Show("Your event is saved successfully.");
            }
        }

        public string GetEvent(string date)
        {
            return events.ContainsKey(date) ? events[date] : null;
        }

        private void textDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void textEvent_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
