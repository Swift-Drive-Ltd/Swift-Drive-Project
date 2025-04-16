using System;
using System.Windows.Forms;

namespace DrivingLessonsBooking
{
    public partial class DisplaySortedBookingsForm : Form
    {
        private BookingLogic bookingSystem;
        private ListBox bookingsList;

        public DisplaySortedBookingsForm(BookingLogic booking)
        {
            bookingSystem = booking;
            InitializeComponent();
            DisplaySortedBookings();
        }

        private void InitializeComponent()
        {
            this.Text = "Sorted Bookings";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                RowStyles = {
                    new RowStyle(SizeType.Percent, 90),
                    new RowStyle(SizeType.Percent, 10)
                }
            };

            bookingsList = new ListBox
            {
                Dock = DockStyle.Fill
            };
            mainPanel.Controls.Add(bookingsList, 0, 0);

            Button btnClose = new Button
            {
                Text = "Close",
                Dock = DockStyle.Fill,
                Margin = new Padding(150, 5, 150, 5)
            };
            btnClose.Click += (s, e) => Close();
            mainPanel.Controls.Add(btnClose, 0, 1);

            this.Controls.Add(mainPanel);
        }

        private void DisplaySortedBookings()
        {
            // Replace the console output with ListBox items
            // This is a placeholder - you would need to modify BookingLogic
            // to return a collection of sorted bookings
            bookingsList.Items.Add("Sorted bookings will be displayed here");
            
            // Example implementation once BookingLogic is modified:
            // var sortedBookings = bookingSystem.GetSortedBookings();
            // foreach (var booking in sortedBookings)
            // {
            //     bookingsList.Items.Add(booking.ToString());
            // }
        }
    }
}
