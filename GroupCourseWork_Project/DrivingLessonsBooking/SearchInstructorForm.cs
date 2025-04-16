using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class SearchInstructorForm : Form
    {
        private InstructorLogic instructorSystem;
        
        // Using null-forgiving operator to indicate these will be initialized in InitializeComponent
        private TextBox txtInstructorId = null!;
        private TextBox txtName = null!;
        private TextBox txtPhone = null!;
        private ListView resultsListView = null!;
        private Label lblResultsCount = null!;

        public SearchInstructorForm(InstructorLogic instructor)
        {
            instructorSystem = instructor;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Base form setup
            BaseFormHelper.SetupStandardForm(this, "Search Instructors", 800, 500);
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
                ColumnCount = 4,
                Padding = new Padding(10),
                RowStyles = {
                    new RowStyle(SizeType.Percent, 25),
                    new RowStyle(SizeType.Percent, 25),
                    new RowStyle(SizeType.Percent, 25),
                    new RowStyle(SizeType.Percent, 25)
                },
                ColumnStyles = {
                    new ColumnStyle(SizeType.Absolute, 120), // Label width
                    new ColumnStyle(SizeType.Percent, 50),   // Text field width
                    new ColumnStyle(SizeType.Absolute, 120), // Label width
                    new ColumnStyle(SizeType.Percent, 50)    // Text field width
                }
            };

            // Instructor ID search field
            Label lblInstructorId = new Label
            {
                Text = "Instructor ID:",
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill
            };
            searchFieldsPanel.Controls.Add(lblInstructorId, 0, 0);

            txtInstructorId = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };
            searchFieldsPanel.Controls.Add(txtInstructorId, 1, 0);

            // Name search field
            Label lblName = new Label
            {
                Text = "Name:",
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill
            };
            searchFieldsPanel.Controls.Add(lblName, 2, 0);

            txtName = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };
            searchFieldsPanel.Controls.Add(txtName, 3, 0);

            // Phone search field
            Label lblPhone = new Label
            {
                Text = "Phone:",
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill
            };
            searchFieldsPanel.Controls.Add(lblPhone, 0, 1);

            txtPhone = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };
            searchFieldsPanel.Controls.Add(txtPhone, 1, 1);

            // Search and reset buttons row
            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };
            searchFieldsPanel.Controls.Add(buttonsPanel, 0, 3);
            searchFieldsPanel.SetColumnSpan(buttonsPanel, 4);

            // Reset button
            Button btnReset = new Button
            {
                Text = "Reset",
                Width = 120,
                Height = 35,
                Margin = new Padding(5),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnReset.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnReset.Click += (s, e) => 
            {
                // Clear all search fields
                txtInstructorId.Text = "";
                txtName.Text = "";
                txtPhone.Text = "";
            };
            
            // Add hover effects
            btnReset.MouseEnter += (s, e) => {
                btnReset.BackColor = Color.FromArgb(230, 230, 230);
            };
            btnReset.MouseLeave += (s, e) => {
                btnReset.BackColor = Color.FromArgb(240, 240, 240);
            };
            
            buttonsPanel.Controls.Add(btnReset);
            
            // Search button
            Button btnSearch = new Button
            {
                Text = "Search",
                Width = 120,
                Height = 35,
                Margin = new Padding(5),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(37, 57, 111),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += btnSearch_Click;
            
            // Add hover effects
            btnSearch.MouseEnter += (s, e) => {
                btnSearch.BackColor = Color.FromArgb(57, 77, 131); // Lighter blue
            };
            btnSearch.MouseLeave += (s, e) => {
                btnSearch.BackColor = Color.FromArgb(37, 57, 111); // Back to original blue
            };
            
            buttonsPanel.Controls.Add(btnSearch);

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
            resultsListView.Columns.Add("Instructor ID", 120);
            resultsListView.Columns.Add("Name", 250);
            resultsListView.Columns.Add("Phone", 180);
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
            
            // Set focus to the first field
            this.Load += (s, e) => txtInstructorId.Focus();
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            string instructorId = txtInstructorId.Text.Trim();
            string name = txtName.Text.Trim();
            string phone = txtPhone.Text.Trim();

            // Check if any search criteria is provided
            if (string.IsNullOrEmpty(instructorId) && string.IsNullOrEmpty(name) && 
                string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please enter at least one search criterion.", 
                    "Search Criteria Needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Perform the search
            List<Instructor> searchResults = SearchInstructors(instructorId, name, phone);
            DisplaySearchResults(searchResults);
        }

        private List<Instructor> SearchInstructors(string instructorId, string name, string phone)
        {
            List<Instructor> results = new List<Instructor>();

            try
            {
                using (var conn = new SQLiteConnection(instructorSystem.GetConnectionString()))
                {
                    conn.Open();

                    // Build the query based on provided criteria
                    string query = "SELECT * FROM Instructors WHERE 1=1"; // Start with a condition that's always true
                    
                    // Add search conditions
                    if (!string.IsNullOrEmpty(instructorId))
                        query += " AND InstructorID LIKE @instructorId";
                    
                    if (!string.IsNullOrEmpty(name))
                        query += " AND Name LIKE @name";
                    
                    if (!string.IsNullOrEmpty(phone))
                        query += " AND Phone LIKE @phone";
                    
                    // Add an order by clause
                    query += " ORDER BY Name";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        // Add parameters with wildcards for flexible matching
                        if (!string.IsNullOrEmpty(instructorId))
                            cmd.Parameters.AddWithValue("@instructorId", $"%{instructorId}%");
                        
                        if (!string.IsNullOrEmpty(name))
                            cmd.Parameters.AddWithValue("@name", $"%{name}%");
                        
                        if (!string.IsNullOrEmpty(phone))
                            cmd.Parameters.AddWithValue("@phone", $"%{phone}%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create instructor objects from the results
                                string id = reader["InstructorID"]?.ToString() ?? string.Empty;
                                string instructorName = reader["Name"]?.ToString() ?? string.Empty;
                                string instructorPhone = reader["Phone"]?.ToString() ?? string.Empty;

                                Instructor instructor = new Instructor(id, instructorName, instructorPhone);
                                results.Add(instructor);
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

        private void DisplaySearchResults(List<Instructor> results)
        {
            resultsListView.Items.Clear();
            
            if (results.Count == 0)
            {
                lblResultsCount.Text = "No instructors found matching your criteria.";
                lblResultsCount.ForeColor = Color.FromArgb(150, 30, 30); // Dark red
                return;
            }

            // Update results count
            lblResultsCount.Text = $"Found {results.Count} instructor(s) matching your criteria.";
            lblResultsCount.ForeColor = Color.FromArgb(0, 120, 0); // Dark green

            // Populate results
            foreach (Instructor instructor in results)
            {
                ListViewItem item = new ListViewItem(instructor.InstructorID);
                item.SubItems.Add(instructor.Name);
                item.SubItems.Add(instructor.Phone);
                
                // Store the full instructor object for later use
                item.Tag = instructor;
                
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
            // Show detailed instructor information when double-clicking a result
            if (resultsListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = resultsListView.SelectedItems[0];
                
                if (selectedItem.Tag is Instructor instructor)
                {
                    DisplayDetailedInstructorInfo(instructor);
                }
            }
        }

        private void DisplayDetailedInstructorInfo(Instructor instructor)
        {
            // Create a nicely formatted detailed view
            string detailMessage = 
                $"Instructor Details:\n\n" +
                $"Instructor ID: {instructor.InstructorID}\n" +
                $"Name: {instructor.Name}\n" +
                $"Phone: {instructor.Phone}\n";

            MessageBox.Show(detailMessage, "Instructor Details", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
