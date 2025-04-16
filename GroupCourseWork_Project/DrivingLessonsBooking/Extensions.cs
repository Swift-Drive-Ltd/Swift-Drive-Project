using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DrivingLessonsBooking
{
    public static class Extensions
    {
        // A redirectable TextWriter that can be used to capture console output
        public class ConsoleOutputCapturer : TextWriter
        {
            private StringBuilder output = new StringBuilder();
            
            public override Encoding Encoding => Encoding.UTF8;
            
            public override void Write(char value)
            {
                output.Append(value);
            }
            
            public override void Write(string value)
            {
                output.Append(value);
            }
            
            public string GetOutput()
            {
                return output.ToString();
            }
            
            public void Clear()
            {
                output.Clear();
            }
        }
        
        // Helper method to execute a method that writes to console and capture the output
        public static string CaptureConsoleOutput(Action action)
        {
            ConsoleOutputCapturer capturer = new ConsoleOutputCapturer();
            TextWriter originalOutput = Console.Out;
            
            try
            {
                Console.SetOut(capturer);
                action();
                return capturer.GetOutput();
            }
            finally
            {
                Console.SetOut(originalOutput);
            }
        }
        
        // Helper method to populate a ListBox from a collection of items
        public static void PopulateListBox<T>(this ListBox listBox, IEnumerable<T> items, Func<T, string> displayTextSelector)
        {
            listBox.Items.Clear();
            foreach (var item in items)
            {
                listBox.Items.Add(displayTextSelector(item));
            }
            
            if (listBox.Items.Count == 0)
            {
                listBox.Items.Add("No items to display.");
            }
        }
    }
}
