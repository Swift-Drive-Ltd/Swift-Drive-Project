using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class SearchBookingForm : Form
    {
        private BookingLogic bookingSystem;
        
        // Using null-forgiving operator to indicate these will be initialized in InitializeComponent
        private TextBox txtBookingId = null!;
        private TextBox txtStudentName = null!;
        private TextBox txtInstructorName = null!;
        private ListView resultsListView = null!;
        private Label lblResultsCount = null!;

        public SearchBookingForm(BookingLogic booking)
        {
            bookingSystem = booking;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Base form setup
            BaseFormHelper.SetupStandardForm(this, "Search Bookings", 800, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            // Main layout panel
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                RowStyles = {
                    new RowStyle(SizeType.Absolute, 180),  // Search criteria panel
                    new RowStyle(SizeType.Percent, 100),   // Results
                    new RowStyle(SizeType.Absolute, 50)    // Bottom buttons
                }
            };

            // Search criteria group box
            GroupBox searchCriteriaGroup = BaseFormHelper.CreateStandardGroupBox("Search Criteria (Fill any field)");
            searchCriteriaGroup.Dock = DockStyle.Fill;

            // Search fields panel
            TableLayoutPanel searchFieldsPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 2,
                Padding = new Padding(10),
                RowStyles = {
                    new RowStyle(SizeType.Percent, 25),
                    new RowStyle(SizeType.Percent, 25),
                    new RowStyle(SizeType.Percent, 25),
                    new RowStyle(SizeType.Percent, 25)
                },
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 30),
                    new ColumnStyle(SizeType.Percent, 70)
                }
            };

            // Add search fields with labels
            searchFieldsPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Booking ID:"), 0, 0);
            txtBookingId = BaseFormHelper.CreateStandardTextBox();
            searchFieldsPanel.Controls.Add(txtBookingId, 1, 0);

            searchFieldsPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Student Name:"), 0, 1);
            txtStudentName = BaseFormHelper.CreateStandardTextBox();
            searchFieldsPanel.Controls.Add(txtStudentName, 1, 1);

            searchFieldsPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Instructor Name:"), 0, 2);
            txtInstructorName = BaseFormHelper.CreateStandardTextBox();
            searchFieldsPanel.Controls.Add(txtInstructorName, 1, 2);

            // Improved Search button with better visibility
            Button btnSearch = BaseFormHelper.CreateStandardButton("Search", btnSearch_Click);
            btnSearch.Dock = DockStyle.None;
            btnSearch.Width = 150;  // Increased width
            btnSearch.Height = 40;  // Increased height
            btnSearch.Font = new Font(btnSearch.Font.FontFamily, 11, FontStyle.Bold);  // Larger, bold font
            btnSearch.BackColor = Color.FromArgb(37, 57, 111);  // Darker blue background
            btnSearch.ForeColor = Color.White;  // White text for better contrast
            btnSearch.Margin = new Padding(0, 10, 0, 0);  // Added top margin
            btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(57, 77, 131);  // Lighter blue on hover
            searchFieldsPanel.Controls.Add(btnSearch, 1, 3);

            searchCriteriaGroup.Controls.Add(searchFieldsPanel);
            mainPanel.Controls.Add(searchCriteriaGroup, 0, 0);

            // Results group
            GroupBox resultsGroup = BaseFormHelper.CreateStandardGroupBox("Search Results");
            resultsGroup.Dock = DockStyle.Fill;

            // Results layout panel
            TableLayoutPanel resultsPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                RowStyles = {
                    new RowStyle(SizeType.Percent, 100),
                    new RowStyle(SizeType.Absolute, 30)
                }
            };

            // Results list view
            resultsListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = BaseFormHelper.RegularFont,
                BorderStyle = BorderStyle.FixedSingle,
                MultiSelect = false
            };

            // Add columns to results listview
            resultsListView.Columns.Add("Booking ID", 80);
            resultsListView.Columns.Add("Date", 100);
            resultsListView.Columns.Add("Time", 70);
            resultsListView.Columns.Add("Student", 130);
            resultsListView.Columns.Add("Instructor", 130);
            resultsListView.Columns.Add("Car", 110);
            resultsListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            
            // Double-click to show full details
            resultsListView.DoubleClick += resultsListView_DoubleClick;

            resultsPanel.Controls.Add(resultsListView, 0, 0);

            // Results count label
            lblResultsCount = new Label
            {
                Text = "No search performed yet",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = BaseFormHelper.RegularFont,
                ForeColor = Color.FromArgb(100, 100, 100),
                Padding = new Padding(5, 0, 0, 0)
            };
            resultsPanel.Controls.Add(lblResultsCount, 0, 1);

            resultsGroup.Controls.Add(resultsPanel);
            mainPanel.Controls.Add(resultsGroup, 0, 1);

            // Bottom buttons panel
            FlowLayoutPanel bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10),
                WrapContents = false
            };

            // Close button
            Button btnClose = BaseFormHelper.CreateStandardButton("Close", (s, e) => Close());
            btnClose.Width = 120;
            btnClose.Height = 35;
            bottomPanel.Controls.Add(btnClose);

            mainPanel.Controls.Add(bottomPanel, 0, 2);

            this.Controls.Add(mainPanel);
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            string bookingId = txtBookingId.Text.Trim();
            string studentName = txtStudentName.Text.Trim();
            string instructorName = txtInstructorName.Text.Trim();

            // Check if any search criteria is provided
            if (string.IsNullOrEmpty(bookingId) && string.IsNullOrEmpty(studentName) && string.IsNullOrEmpty(instructorName))
            {
                MessageBox.Show("Please enter at least one search criteria.", 
                    "Search Criteria Needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Perform the search
            List<Booking> searchResults = SearchBookings(bookingId, studentName, instructorName);
            DisplaySearchResults(searchResults);
        }

        private List<Booking> SearchBookings(string bookingId, string studentName, string instructorName)
        {
            List<Booking> results = new List<Booking>();

            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();

                    // Build the query based on provided criteria
                    string query = "SELECT b.* FROM Bookings b";
                    
                    // Join tables if needed for name searches
                    if (!string.IsNullOrEmpty(studentName) || !string.IsNullOrEmpty(instructorName))
                    {
                        if (!string.IsNullOrEmpty(studentName))
                        {
                            query += " LEFT JOIN Students s ON b.StudentID = s.StudentID";
                        }
                        
                        if (!string.IsNullOrEmpty(instructorName))
                        {
                            query += " LEFT JOIN Instructors i ON b.InstructorID = i.InstructorID";
                        }
                    }
                    
                    // Start WHERE clause
                    bool hasCondition = false;
                    
                    if (!string.IsNullOrEmpty(bookingId))
                    {
                        query += " WHERE b.BookingID LIKE @bookingId";
                        hasCondition = true;
                    }
                    
                    if (!string.IsNullOrEmpty(studentName))
                    {
                        query += hasCondition ? " OR" : " WHERE";
                        query += " s.Name LIKE @studentName";
                        hasCondition = true;
                    }
                    
                    if (!string.IsNullOrEmpty(instructorName))
                    {
                        query += hasCondition ? " OR" : " WHERE";
                        query += " i.Name LIKE @instructorName";
                    }
                    
                    // Add an order by clause
                    query += " ORDER BY b.LessonDate";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        // Add parameters with wildcards for flexible matching
                        if (!string.IsNullOrEmpty(bookingId))
                        {
                            cmd.Parameters.AddWithValue("@bookingId", $"%{bookingId}%");
                        }
                        
                        if (!string.IsNullOrEmpty(studentName))
                        {
                            cmd.Parameters.AddWithValue("@studentName", $"%{studentName}%");
                        }
                        
                        if (!string.IsNullOrEmpty(instructorName))
                        {
                            cmd.Parameters.AddWithValue("@instructorName", $"%{instructorName}%");
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create booking objects from the results
                                string id = reader["BookingID"]?.ToString() ?? string.Empty;
                                string studentId = reader["StudentID"]?.ToString() ?? string.Empty;
                                string instructorId = reader["InstructorID"]?.ToString() ?? string.Empty;
                                string lessonDateStr = reader["LessonDate"]?.ToString() ?? string.Empty;
                                string carId = reader["CarID"]?.ToString() ?? string.Empty;

                                DateTime lessonDate = DateTime.Now;
                                if (!string.IsNullOrEmpty(lessonDateStr))
                                {
                                    DateTime.TryParse(lessonDateStr, out lessonDate);
                                }

                                Booking booking = new Booking(id, studentId, instructorId, lessonDate, carId);
                                results.Add(booking);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return results;
        }

        private void DisplaySearchResults(List<Booking> results)
        {
            resultsListView.Items.Clear();
            
            if (results.Count == 0)
            {
                lblResultsCount.Text = "No bookings found matching your criteria.";
                lblResultsCount.ForeColor = Color.FromArgb(150, 30, 30); // Dark red
                return;
            }

            // Update results count
            lblResultsCount.Text = $"Found {results.Count} booking(s) matching your criteria.";
            lblResultsCount.ForeColor = Color.FromArgb(0, 120, 0); // Dark green

            // Populate results
            foreach (Booking booking in results)
            {
                // Get related data
                string studentName = GetName(booking.studentID, "Students");
                string instructorName = GetName(booking.instructorID, "Instructors");
                string carInfo = GetCarInfo(booking.carID);

                ListViewItem item = new ListViewItem(booking.BookingID);
                item.SubItems.Add(booking.LessonDate.ToString("dd/MM/yyyy"));
                item.SubItems.Add(booking.LessonDate.ToString("HH:mm"));
                item.SubItems.Add(studentName);
                item.SubItems.Add(instructorName);
                item.SubItems.Add(carInfo);
                
                // Store the full booking object for later use
                item.Tag = booking;
                
                resultsListView.Items.Add(item);
            }

            // Apply alternating row colors
            ApplyAlternatingRowColors();
        }

        private void ApplyAlternatingRowColors()
        {
            for (int i = 0; i < resultsListView.Items.Count; i++)
            {
                resultsListView.Items[i].BackColor = i % 2 == 0
                    ? Color.White
                    : Color.FromArgb(245, 245, 250); // Light blue-gray
            }
        }

        private string GetName(string id, string table)
        {
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand($"SELECT Name FROM {table} WHERE {table.TrimEnd('s')}ID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        object? result = cmd.ExecuteScalar();
                        
                        string fullName = result?.ToString() ?? id;
                        if (fullName != id)
                        {
                            // Format the name to get first and last only
                            string[] nameParts = fullName.Split(' ');
                            if (nameParts.Length >= 2)
                            {
                                return $"{nameParts[0]} {nameParts[nameParts.Length - 1]}";
                            }
                        }
                        return fullName;
                    }
                }
            }
            catch
            {
                return id;
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
                return carId;
            }
        }

        private void resultsListView_DoubleClick(object? sender, EventArgs e)
        {
            // Show detailed booking information when double-clicking a result
            if (resultsListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = resultsListView.SelectedItems[0];
                
                if (selectedItem.Tag is Booking booking)
                {
                    DisplayDetailedBookingInfo(booking);
                }
            }
        }

        private void DisplayDetailedBookingInfo(Booking booking)
        {
            // Get full detailed information
            string studentName = GetFullName(booking.studentID, "Students");
            string instructorName = GetFullName(booking.instructorID, "Instructors");
            string carDetails = GetFullCarDetails(booking.carID);

            // Create a nicely formatted detailed view
            string detailMessage = 
                $"Booking Details:\n\n" +
                $"Booking ID: {booking.BookingID}\n\n" +
                $"Date: {booking.LessonDate:dd MMMM yyyy}\n" +
                $"Time: {booking.LessonDate:HH:mm}\n\n" +
                $"Student: {studentName}\n" +
                $"Instructor: {instructorName}\n" +
                $"Car: {carDetails}\n";

            MessageBox.Show(detailMessage, "Booking Details", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetFullName(string id, string table)
        {
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand($"SELECT * FROM {table} WHERE {table.TrimEnd('s')}ID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string? name = reader["Name"]?.ToString();
                                
                                // Add email for students or phone for instructors
                                if (name != null)
                                {
                                    if (table == "Students" && reader["Email"] != null)
                                    {
                                        string? email = reader["Email"]?.ToString();
                                        return email != null ? $"{name} ({email})" : name;
                                    }
                                    else if (table == "Instructors" && reader["Phone"] != null)
                                    {
                                        string? phone = reader["Phone"]?.ToString();
                                        return phone != null ? $"{name} ({phone})" : name;
                                    }
                                    
                                    return name;
                                }
                            }
                        }
                    }
                }
            }
            catch { /* Silently handle errors */ }
            
            return id;
        }

        private string GetFullCarDetails(string carId)
        {
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT Model, Type, LicensePlate FROM Cars WHERE CarID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", carId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string? model = reader["Model"]?.ToString();
                                string? type = reader["Type"]?.ToString();
                                string? licensePlate = reader["LicensePlate"]?.ToString();
                                
                                if (model != null && type != null && licensePlate != null)
                                {
                                    return $"{model} ({type}) - {licensePlate}";
                                }
                                else if (model != null)
                                {
                                    return model;
                                }
                            }
                        }
                    }
                }
            }
            catch { /* Silently handle errors */ }
            
            return carId;
        }
    }
}
