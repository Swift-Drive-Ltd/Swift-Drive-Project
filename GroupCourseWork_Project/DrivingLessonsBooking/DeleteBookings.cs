using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class DeleteBookingForm : Form
    {
        private BookingLogic bookingSystem;
        
        // Using null-forgiving operator to indicate these will be initialized in InitializeComponent
        private ListView bookingsListView = null!;
        private Label lblSelectionStatus = null!;
        private Button btnDelete = null!;

        public DeleteBookingForm(BookingLogic booking)
        {
            bookingSystem = booking;
            InitializeComponent();
            LoadBookingsList();
        }

        private void InitializeComponent()
        {
            // Base form setup
            BaseFormHelper.SetupStandardForm(this, "Delete Booking", 800, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            // Main layout panel
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                RowStyles = {
                    new RowStyle(SizeType.Absolute, 60),   // Header
                    new RowStyle(SizeType.Percent, 100),   // Bookings list
                    new RowStyle(SizeType.Absolute, 80)    // Delete panel
                }
            };

            // Header panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(37, 57, 111)
            };

            Label headerLabel = new Label
            {
                Text = "Select a Booking to Delete",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            headerPanel.Controls.Add(headerLabel);

            // Bookings list panel
            GroupBox listGroupBox = BaseFormHelper.CreateStandardGroupBox("Available Bookings");
            listGroupBox.Dock = DockStyle.Fill;

            // Create ListView for tabular display of bookings
            bookingsListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = BaseFormHelper.RegularFont,
                BorderStyle = BorderStyle.FixedSingle,
                MultiSelect = false
            };

            // Add columns for each data element
            bookingsListView.Columns.Add("Booking ID", 100);
            bookingsListView.Columns.Add("Date", 110);
            bookingsListView.Columns.Add("Time", 70);
            bookingsListView.Columns.Add("Student", 140);
            bookingsListView.Columns.Add("Instructor", 140);
            bookingsListView.Columns.Add("Car", 120);
            bookingsListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            // Handle selection change
            bookingsListView.ItemSelectionChanged += BookingsListView_ItemSelectionChanged;

            listGroupBox.Controls.Add(bookingsListView);

            // Action panel for delete and cancel buttons
            Panel actionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 245, 250) // Light blue-gray background
            };
            
            // Status label showing currently selected booking
            lblSelectionStatus = new Label
            {
                Text = "No booking selected",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(20, 15),
                AutoSize = true
            };
            actionPanel.Controls.Add(lblSelectionStatus);

            // Button panel for alignment
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Width = 300,
                Height = 45,
                Location = new Point(480, 15),
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            // Delete button
            btnDelete = new Button
            {
                Text = "Delete Booking",
                Width = 140,
                Height = 40,
                Margin = new Padding(5),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(192, 0, 0), // Dark red
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false // Initially disabled until selection
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;
            
            // Add hover effects
            btnDelete.MouseEnter += (s, e) => {
                if (btnDelete.Enabled)
                    btnDelete.BackColor = Color.FromArgb(220, 0, 0); // Slightly brighter red
            };
            btnDelete.MouseLeave += (s, e) => {
                if (btnDelete.Enabled)
                    btnDelete.BackColor = Color.FromArgb(192, 0, 0); // Back to original dark red
            };
            
            buttonPanel.Controls.Add(btnDelete);

            // Cancel button
            Button btnCancel = new Button
            {
                Text = "Cancel",
                Width = 120,
                Height = 40,
                Margin = new Padding(5),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnCancel.Click += (s, e) => Close();
            
            // Add hover effects
            btnCancel.MouseEnter += (s, e) => {
                btnCancel.BackColor = Color.FromArgb(230, 230, 230);
            };
            btnCancel.MouseLeave += (s, e) => {
                btnCancel.BackColor = Color.FromArgb(240, 240, 240);
            };
            
            buttonPanel.Controls.Add(btnCancel);
            
            actionPanel.Controls.Add(buttonPanel);

            // Add all components to main panel
            mainPanel.Controls.Add(headerPanel, 0, 0);
            mainPanel.Controls.Add(listGroupBox, 0, 1);
            mainPanel.Controls.Add(actionPanel, 0, 2);

            this.Controls.Add(mainPanel);
        }

        private void LoadBookingsList()
        {
            bookingsListView.Items.Clear();
            
            try
            {
                List<Booking> bookings = GetAllBookingsFromDatabase();
                
                if (bookings.Count > 0)
                {
                    foreach (Booking booking in bookings)
                    {
                        string studentName = GetStudentName(booking.studentID);
                        string instructorName = GetInstructorName(booking.instructorID);
                        string carInfo = GetCarInfo(booking.carID);
                        
                        ListViewItem item = new ListViewItem(booking.BookingID);
                        item.SubItems.Add(booking.LessonDate.ToString("dd/MM/yyyy"));
                        item.SubItems.Add(booking.LessonDate.ToString("HH:mm"));
                        item.SubItems.Add(studentName);
                        item.SubItems.Add(instructorName);
                        item.SubItems.Add(carInfo);
                        
                        // Store the full booking object for later use
                        item.Tag = booking;
                        
                        bookingsListView.Items.Add(item);
                    }

                    // Apply alternating row colors
                    ApplyAlternatingRowColors();
                }
                else
                {
                    // No bookings found, add a placeholder item
                    ListViewItem placeholder = new ListViewItem("No bookings found");
                    bookingsListView.Items.Add(placeholder);
                    
                    // Disable delete button
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BookingsListView_ItemSelectionChanged(object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            // Update button state and selection status
            if (bookingsListView.SelectedItems.Count > 0 && bookingsListView.SelectedItems[0].Tag is Booking selectedBooking)
            {
                btnDelete.Enabled = true;
                lblSelectionStatus.Text = $"Selected: Booking {selectedBooking.BookingID} on {selectedBooking.LessonDate:dd/MM/yyyy}";
                lblSelectionStatus.ForeColor = Color.FromArgb(37, 57, 111);
            }
            else
            {
                btnDelete.Enabled = false;
                lblSelectionStatus.Text = "No booking selected";
                lblSelectionStatus.ForeColor = Color.FromArgb(80, 80, 80);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (bookingsListView.SelectedItems.Count == 0 || !(bookingsListView.SelectedItems[0].Tag is Booking selectedBooking))
            {
                MessageBox.Show("Please select a booking to delete.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string bookingId = selectedBooking.BookingID;
            string bookingDate = selectedBooking.LessonDate.ToString("dd/MM/yyyy HH:mm");
            string studentName = GetStudentName(selectedBooking.studentID);
            
            // Create a professional confirmation dialog
            using (ConfirmDeleteDialog confirmDialog = new ConfirmDeleteDialog(
                $"Booking #{bookingId}", 
                $"Date: {bookingDate}\nStudent: {studentName}"))
            {
                if (confirmDialog.ShowDialog() == DialogResult.Yes)
                {
                    if (bookingSystem.DeleteBooking(bookingId))
                    {
                        // Success message with professional styling
                        MessageBox.Show(
                            $"Booking #{bookingId} has been successfully deleted.",
                            "Booking Deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        
                        // Refresh the list after deletion
                        LoadBookingsList();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to delete the booking. It may have already been removed from the database.",
                            "Delete Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private List<Booking> GetAllBookingsFromDatabase()
        {
            List<Booking> bookingsList = new List<Booking>();
            
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    
                    string query = "SELECT * FROM Bookings ORDER BY LessonDate";
                    
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string bookingId = reader["BookingID"]?.ToString() ?? string.Empty;
                            string studentId = reader["StudentID"]?.ToString() ?? string.Empty;
                            string instructorId = reader["InstructorID"]?.ToString() ?? string.Empty;
                            string lessonDateStr = reader["LessonDate"]?.ToString() ?? string.Empty;
                            string carId = reader["CarID"]?.ToString() ?? string.Empty;
                            
                            DateTime lessonDate = DateTime.Now;
                            if (!string.IsNullOrEmpty(lessonDateStr))
                            {
                                DateTime.TryParse(lessonDateStr, out lessonDate);
                            }

                            Booking booking = new Booking(
                                bookingId,
                                studentId,
                                instructorId,
                                lessonDate,
                                carId
                            );
                            
                            bookingsList.Add(booking);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error in GetAllBookingsFromDatabase: {ex.Message}");
                throw; // Rethrow to be caught by calling method
            }
            
            return bookingsList;
        }

        private void ApplyAlternatingRowColors()
        {
            for (int i = 0; i < bookingsListView.Items.Count; i++)
            {
                bookingsListView.Items[i].BackColor = i % 2 == 0
                    ? Color.White
                    : Color.FromArgb(245, 245, 250); // Light blue-gray
            }
        }

        private string GetStudentName(string studentId)
        {
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT Name FROM Students WHERE StudentID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", studentId);
                        object? result = cmd.ExecuteScalar();
                        string fullName = result?.ToString() ?? studentId;
                        
                        // Format the name to get first and last only
                        string[] nameParts = fullName.Split(' ');
                        if (nameParts.Length >= 2)
                        {
                            return $"{nameParts[0]} {nameParts[nameParts.Length - 1]}";
                        }
                        return fullName;
                    }
                }
            }
            catch
            {
                return studentId; // Return ID if we can't get the name
            }
        }

        private string GetInstructorName(string instructorId)
        {
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT Name FROM Instructors WHERE InstructorID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", instructorId);
                        object? result = cmd.ExecuteScalar();
                        string fullName = result?.ToString() ?? instructorId;
                        
                        // Format the name to get first and last only
                        string[] nameParts = fullName.Split(' ');
                        if (nameParts.Length >= 2)
                        {
                            return $"{nameParts[0]} {nameParts[nameParts.Length - 1]}";
                        }
                        return fullName;
                    }
                }
            }
            catch
            {
                return instructorId; // Return ID if we can't get the name
            }
        }

        private string GetCarInfo(string carId)
        {
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT Model FROM Cars WHERE CarID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", carId);
                        object? result = cmd.ExecuteScalar();
                        return result?.ToString() ?? carId;
                    }
                }
            }
            catch
            {
                return carId; // Return ID if we can't get the model
            }
        }
    }

    // A custom confirmation dialog for delete operations
    public class ConfirmDeleteDialog : Form
    {
        public ConfirmDeleteDialog(string itemName, string itemDetails)
        {
            InitializeComponent(itemName, itemDetails);
        }

        private void InitializeComponent(string itemName, string itemDetails)
        {
            // Form setup
            this.Text = "Confirm Deletion";
            this.Size = new Size(450, 280);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // Panel for the warning icon and text
            Panel warningPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(255, 240, 240)
            };

            // Warning icon (using a label with a warning symbol)
            Label warningIcon = new Label
            {
                Text = "⚠",
                Font = new Font("Segoe UI", 24, FontStyle.Regular),
                ForeColor = Color.FromArgb(192, 0, 0),
                Size = new Size(50, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 15)
            };
            warningPanel.Controls.Add(warningIcon);

            // Warning title
            Label warningTitle = new Label
            {
                Text = "Are you sure you want to delete this booking?",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(192, 0, 0),
                AutoSize = true,
                Location = new Point(80, 15)
            };
            warningPanel.Controls.Add(warningTitle);

            // Warning subtitle
            Label warningSubtitle = new Label
            {
                Text = "This action cannot be undone.",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(80, 45)
            };
            warningPanel.Controls.Add(warningSubtitle);

            // Details panel
            Panel detailsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Item name label
            Label lblItemName = new Label
            {
                Text = itemName,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 57, 111),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            detailsPanel.Controls.Add(lblItemName);

            // Item details in a text box for better formatting
            TextBox txtDetails = new TextBox
            {
                Text = itemDetails,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(60, 60, 60),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Location = new Point(20, 50),
                Size = new Size(380, 60)
            };
            detailsPanel.Controls.Add(txtDetails);

            // Button panel
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(245, 245, 250)
            };

            // Cancel button
            Button btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.No,
                Width = 100,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                Location = new Point(230, 10)
            };
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            buttonPanel.Controls.Add(btnCancel);

            // Delete button
            Button btnDelete = new Button
            {
                Text = "Delete",
                DialogResult = DialogResult.Yes,
                Width = 100,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(192, 0, 0),
                ForeColor = Color.White,
                Location = new Point(340, 10)
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            buttonPanel.Controls.Add(btnDelete);

            this.Controls.Add(detailsPanel);
            this.Controls.Add(warningPanel);
            this.Controls.Add(buttonPanel);

            this.AcceptButton = btnDelete;
            this.CancelButton = btnCancel;
        }
    }
}
