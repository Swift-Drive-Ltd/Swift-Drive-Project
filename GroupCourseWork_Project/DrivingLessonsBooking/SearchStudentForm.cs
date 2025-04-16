using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class SearchStudentForm : Form
    {
        private StudentLogic studentSystem;
        
        // Using null-forgiving operator to indicate these will be initialized in InitializeComponent
        private TextBox txtStudentId = null!;
        private TextBox txtName = null!;
        private TextBox txtEmail = null!;
        private ListView resultsListView = null!;
        private Label lblResultsCount = null!;

        public SearchStudentForm(StudentLogic student)
        {
            studentSystem = student;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Base form setup
            BaseFormHelper.SetupStandardForm(this, "Search Students", 800, 500);
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
            searchFieldsPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Student ID:"), 0, 0);
            txtStudentId = BaseFormHelper.CreateStandardTextBox();
            searchFieldsPanel.Controls.Add(txtStudentId, 1, 0);

            searchFieldsPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Name:"), 0, 1);
            txtName = BaseFormHelper.CreateStandardTextBox();
            searchFieldsPanel.Controls.Add(txtName, 1, 1);

            searchFieldsPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Email:"), 0, 2);
            txtEmail = BaseFormHelper.CreateStandardTextBox();
            searchFieldsPanel.Controls.Add(txtEmail, 1, 2);

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
            resultsListView.Columns.Add("Student ID", 120);
            resultsListView.Columns.Add("Name", 200);
            resultsListView.Columns.Add("Email", 300);
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
            string studentId = txtStudentId.Text.Trim();
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Check if any search criteria is provided
            if (string.IsNullOrEmpty(studentId) && string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter at least one search criteria.", 
                    "Search Criteria Needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Perform the search
            List<Student> searchResults = SearchStudents(studentId, name, email);
            DisplaySearchResults(searchResults);
        }

        private List<Student> SearchStudents(string studentId, string name, string email)
        {
            List<Student> results = new List<Student>();

            try
            {
                using (var conn = new SQLiteConnection(studentSystem.GetConnectionString()))
                {
                    conn.Open();

                    // Build the query based on provided criteria
                    string query = "SELECT * FROM Students WHERE 1=0"; // Start with a condition that returns no records
                    
                    // Add search conditions
                    if (!string.IsNullOrEmpty(studentId))
                    {
                        query = "SELECT * FROM Students WHERE StudentID LIKE @studentId";
                    }
                    
                    if (!string.IsNullOrEmpty(name))
                    {
                        if (query.Contains("WHERE 1=0"))
                            query = "SELECT * FROM Students WHERE Name LIKE @name";
                        else
                            query += " OR Name LIKE @name";
                    }
                    
                    if (!string.IsNullOrEmpty(email))
                    {
                        if (query.Contains("WHERE 1=0"))
                            query = "SELECT * FROM Students WHERE Email LIKE @email";
                        else
                            query += " OR Email LIKE @email";
                    }
                    
                    // Add an order by clause
                    query += " ORDER BY Name";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        // Add parameters with wildcards for flexible matching
                        if (!string.IsNullOrEmpty(studentId))
                        {
                            cmd.Parameters.AddWithValue("@studentId", $"%{studentId}%");
                        }
                        
                        if (!string.IsNullOrEmpty(name))
                        {
                            cmd.Parameters.AddWithValue("@name", $"%{name}%");
                        }
                        
                        if (!string.IsNullOrEmpty(email))
                        {
                            cmd.Parameters.AddWithValue("@email", $"%{email}%");
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create student objects from the results
                                string id = reader["StudentID"]?.ToString() ?? string.Empty;
                                string studentName = reader["Name"]?.ToString() ?? string.Empty;
                                string studentEmail = reader["Email"]?.ToString() ?? string.Empty;

                                Student student = new Student(id, studentName, studentEmail);
                                results.Add(student);
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

        private void DisplaySearchResults(List<Student> results)
        {
            resultsListView.Items.Clear();
            
            if (results.Count == 0)
            {
                lblResultsCount.Text = "No students found matching your criteria.";
                lblResultsCount.ForeColor = Color.FromArgb(150, 30, 30); // Dark red
                return;
            }

            // Update results count
            lblResultsCount.Text = $"Found {results.Count} student(s) matching your criteria.";
            lblResultsCount.ForeColor = Color.FromArgb(0, 120, 0); // Dark green

            // Populate results
            foreach (Student student in results)
            {
                ListViewItem item = new ListViewItem(student.StudentID);
                item.SubItems.Add(student.Name);
                item.SubItems.Add(student.Email);
                
                // Store the full student object for later use
                item.Tag = student;
                
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

        private void resultsListView_DoubleClick(object? sender, EventArgs e)
        {
            // Show detailed student information when double-clicking a result
            if (resultsListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = resultsListView.SelectedItems[0];
                
                if (selectedItem.Tag is Student student)
                {
                    DisplayDetailedStudentInfo(student);
                }
            }
        }

        private void DisplayDetailedStudentInfo(Student student)
        {
            // Create a nicely formatted detailed view
            string detailMessage = 
                $"Student Details:\n\n" +
                $"Student ID: {student.StudentID}\n" +
                $"Name: {student.Name}\n" +
                $"Email: {student.Email}\n";

            MessageBox.Show(detailMessage, "Student Details", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
