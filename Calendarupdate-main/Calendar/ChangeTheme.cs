using System;
using System.Windows.Forms;

namespace Calendar
{
    public partial class ChangeTheme : Form
    {
        public ThemeEnum SelectedTheme { get; private set; }
        public ChangeTheme()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //custom method for case-insensitive enum parsing
        private static bool TryParseEnum<TEnum>(string value, out TEnum result) where TEnum : struct
        {
            if (int.TryParse(value, out int intValue))
            {
                // Check if the parsed integer is a valid enum value
                if (Enum.IsDefined(typeof(TEnum), intValue))
                {
                    result = (TEnum)(object)intValue;
                    return true;
                }
            }

            result = default(TEnum);
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (TryParseEnum(textBox1.Text, out ThemeEnum selectedTheme))
            {
                SelectedTheme = selectedTheme;
                MessageBox.Show("Your theme is changed successfully.");
            }
            else
            {
                MessageBox.Show("Invalid input or theme not found.");
                textBox1.Text = "";
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }
        //cancel button
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
