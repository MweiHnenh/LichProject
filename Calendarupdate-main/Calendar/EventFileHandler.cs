using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public static class EventFileHandler
{
    private static string EventsDirectory = "Events"; // Change this to your desired directory

    public static void SaveEvent(string date, string eventDescription)
    {
        string filePath = Path.Combine(EventsDirectory, $"{date}.txt");

        try
        {
            Directory.CreateDirectory(EventsDirectory);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(eventDescription);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving event: {ex.Message}");
        }
    }

    public static string LoadEvent(string date)
    {
        string filePath = Path.Combine(EventsDirectory, $"{date}.txt");

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
}
