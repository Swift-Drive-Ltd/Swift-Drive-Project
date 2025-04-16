using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class InstructorManagementForm : Form
    {
        private InstructorLogic instructorSystem;
        // Using null-forgiving operator to indicate these will be initialized in InitializeComponent
        private ListView instructorsListView = null!;
        private ComboBox cmbSortOrder = null!;
        private TextBox txtSearch = null!;
        private Button btnApplyFilter = null!;

        public InstructorManagementForm(InstructorLogic instructor)
        {
            instructorSystem = instructor;
            InitializeComponent();
            LoadInstructorsList();
        }

        private void InitializeComponent()
        {
            // Set up form with modern styling
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Instructor Management - Swift Drive";
            this.BackColor = Color.White;
            this.Padding = new Padding(20);

            // Main container
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                RowStyles = {
                    new RowStyle(SizeType.Percent, 90),
                    new RowStyle(SizeType.Percent, 10)
                },
                BackColor = Color.White
            };

            // Content panel with instructors list and action buttons
            TableLayoutPanel contentPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 2,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 75),
                    new ColumnStyle(SizeType.Percent, 25)
                },
                BackColor = Color.White
            };

            // Create header panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60,
                BackColor = Color.FromArgb(245, 245, 245)
            };

            Label titleLabel = new Label
            {
                Text = "Instructor Management",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 57, 111),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            headerPanel.Controls.Add(titleLabel);

            // Instructors list panel
            Panel listPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.White
            };

            // Create filter/search panel
            Panel filterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(250, 250, 250),
                Padding = new Padding(5)
            };

            // Add search box
            txtSearch = new TextBox
            {
                Width = 250,
                Height = 30,
                Location = new Point(10, 10),
                Font = new Font("Segoe UI", 10),
                PlaceholderText = "Search by ID or name..."
            };
            filterPanel.Controls.Add(txtSearch);

            // Add sort dropdown
            Label lblSort = new Label
            {
                Text = "Sort by:",
                Location = new Point(280, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            filterPanel.Controls.Add(lblSort);

            cmbSortOrder = new ComboBox
            {
                Width = 150,
                Height = 30,
                Location = new Point(340, 10),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSortOrder.Items.Add("Instructor ID (A-Z)");
            cmbSortOrder.Items.Add("Name (A-Z)");
            cmbSortOrder.Items.Add("Phone (A-Z)");
            cmbSortOrder.SelectedIndex = 0;
            filterPanel.Controls.Add(cmbSortOrder);

            // Add apply filter button
            btnApplyFilter = new Button
            {
                Text = "Apply Filter",
                Width = 120,
                Height = 30,
                Location = new Point(510, 10),
                BackColor = Color.FromArgb(37, 57, 111),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnApplyFilter.FlatAppearance.BorderSize = 0;
            btnApplyFilter.Click += (s, e) => ApplyFilters();
            filterPanel.Controls.Add(btnApplyFilter);

            // Create ListView with columns for instructor details
            instructorsListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                MultiSelect = false
            };
            instructorsListView.Columns.Add("Instructor ID", 120);
            instructorsListView.Columns.Add("Name", 200);
            instructorsListView.Columns.Add("Phone", 150);
            instructorsListView.ItemSelectionChanged += (s, e) => UpdateSelectionStatus();

            listPanel.Controls.Add(filterPanel);
            listPanel.Controls.Add(instructorsListView);
            filterPanel.Dock = DockStyle.Top;
            instructorsListView.Dock = DockStyle.Fill;

            // Action buttons panel with modern card-like style
            Panel actionsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 10, 10, 10),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            // Panel for the action buttons card
            Panel actionsCard = new Panel
            {
                Dock = DockStyle.Top,
                Height = 350,
                BackColor = Color.White
            };
            actionsCard.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, actionsCard.ClientRectangle,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid);
            };

            // Labels for the action panel
            Label actionsTitle = new Label
            {
                Text = "Instructor Actions",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20),
                ForeColor = Color.FromArgb(50, 50, 50)
            };
            actionsCard.Controls.Add(actionsTitle);

            // Create action buttons with consistent styling
            Button btnAdd = CreateActionButton("Add New Instructor", 20, 70);
            btnAdd.Click += btnAdd_Click;
            actionsCard.Controls.Add(btnAdd);

            Button btnSearch = CreateActionButton("Search Instructor", 20, 130);
            btnSearch.Click += btnSearch_Click;
            actionsCard.Controls.Add(btnSearch);

            Button btnDelete = CreateActionButton("Delete Selected Instructor", 20, 190);
            btnDelete.Click += btnDelete_Click;
            actionsCard.Controls.Add(btnDelete);

            Button btnRefresh = CreateActionButton("Refresh List", 20, 250);
            btnRefresh.Click += btnRefresh_Click;
            actionsCard.Controls.Add(btnRefresh);

            // Selection status panel
            Panel selectionPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 150, // Increased height for the table view
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            selectionPanel.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, selectionPanel.ClientRectangle,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid);
            };

            Label selectionTitle = new Label
            {
                Text = "Selected Instructor Details",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 15),
                ForeColor = Color.FromArgb(50, 50, 50)
            };
            selectionPanel.Controls.Add(selectionTitle);

            // Create a panel with auto-scroll capability to contain the instructor detail "table"
            Panel instructorDetailContainer = new Panel
            {
                AutoScroll = true,
                BorderStyle = BorderStyle.None,
                Location = new Point(20, 45),
                Size = new Size(300, 90),
                Name = "pnlInstructorDetailContainer"
            };
            selectionPanel.Controls.Add(instructorDetailContainer);

            // Create table-like layout for instructor details
            TableLayoutPanel instructorDetailsTable = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 4, // Accommodate Instructor ID, Name, Phone
                Size = new Size(280, 100),
                Dock = DockStyle.Top,
                AutoSize = true,
                Name = "tblInstructorDetails"
            };

            // Add column styles
            instructorDetailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            instructorDetailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));

            // Add row styles
            for (int i = 0; i < 4; i++)
            {
                instructorDetailsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            // Add header row with background color
            Label headerProperty = new Label
            {
                Text = "Property",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(37, 57, 111),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 1, 1),
                Padding = new Padding(5, 3, 0, 3)
            };
            instructorDetailsTable.Controls.Add(headerProperty, 0, 0);

            Label headerValue = new Label
            {
                Text = "Value",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(37, 57, 111),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 0, 1),
                Padding = new Padding(5, 3, 0, 3)
            };
            instructorDetailsTable.Controls.Add(headerValue, 1, 0);

            // Create property rows with alternating colors
            string[] propertyNames = { "Instructor ID", "Name", "Phone" };
            for (int i = 0; i < propertyNames.Length; i++)
            {
                Color rowColor = (i % 2 == 0) ? Color.FromArgb(245, 245, 250) : Color.White;
                
                Label propertyLabel = new Label
                {
                    Text = propertyNames[i],
                    Font = new Font("Segoe UI", 9),
                    BackColor = rowColor,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(0, 0, 1, 1),
                    Padding = new Padding(5, 3, 0, 3)
                };
                instructorDetailsTable.Controls.Add(propertyLabel, 0, i + 1);

                Label valueLabel = new Label
                {
                    Text = "Not selected",
                    Name = $"lblValue{i}",
                    Font = new Font("Segoe UI", 9),
                    BackColor = rowColor,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(0, 0, 0, 1),
                    Padding = new Padding(5, 3, 0, 3)
                };
                instructorDetailsTable.Controls.Add(valueLabel, 1, i + 1);
            }

            instructorDetailContainer.Controls.Add(instructorDetailsTable);
            
            // Add "No selection" message panel to show when no instructor is selected
            Panel noSelectionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = true,
                Name = "pnlNoSelection"
            };
            
            Label noSelectionLabel = new Label
            {
                Text = "No instructor selected. Select an instructor from the list to see details.",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            noSelectionPanel.Controls.Add(noSelectionLabel);
            instructorDetailContainer.Controls.Add(noSelectionPanel);
            
            actionsPanel.Controls.Add(actionsCard);
            actionsPanel.Controls.Add(selectionPanel);

            contentPanel.Controls.Add(listPanel, 0, 0);
            contentPanel.Controls.Add(actionsPanel, 1, 0);

            // Footer panel for close button
            Panel footerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(37, 57, 111),
                Padding = new Padding(10)
            };

            Button btnClose = new Button
            {
                Text = "Return to Dashboard",
                Height = 40,
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(57, 77, 131),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Close();
            // Center the button
            btnClose.Location = new Point((footerPanel.Width - btnClose.Width) / 2, (footerPanel.Height - btnClose.Height) / 2);
            footerPanel.Controls.Add(btnClose);
            footerPanel.Resize += (s, e) => {
                btnClose.Location = new Point((footerPanel.Width - btnClose.Width) / 2, (footerPanel.Height - btnClose.Height) / 2);
            };

            mainPanel.Controls.Add(headerPanel, 0, 0);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            mainPanel.Controls.Add(contentPanel, 0, 0);
            mainPanel.Controls.Add(footerPanel, 0, 1);

            this.Controls.Add(mainPanel);
        }

        private Button CreateActionButton(string text, int x, int y)
        {
            Button button = new Button
            {
                Text = text,
                Width = 220,
                Height = 45,
                Location = new Point(x, y),
                BackColor = Color.FromArgb(37, 57, 111),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter
            };
            button.FlatAppearance.BorderSize = 0;
            
            // Add hover effect
            button.MouseEnter += (s, e) => {
                button.BackColor = Color.FromArgb(57, 77, 131);
            };
            
            button.MouseLeave += (s, e) => {
                button.BackColor = Color.FromArgb(37, 57, 111);
            };
            
            return button;
        }

        private void LoadInstructorsList(string searchTerm = "", string sortBy = "InstructorID")
        {
            instructorsListView.Items.Clear();
            
            try
            {
                List<Instructor> instructors = GetAllInstructorsFromDatabase(searchTerm, sortBy);
                
                if (instructors.Count > 0)
                {
                    foreach (Instructor instructor in instructors)
                    {
                        ListViewItem item = new ListViewItem(instructor.InstructorID);
                        item.SubItems.Add(instructor.Name);
                        item.SubItems.Add(instructor.Phone);
                        item.Tag = instructor; // Store the instructor object for later use
                        instructorsListView.Items.Add(item);
                    }
                }
                else
                {
                    // No instructors found, add a placeholder item
                    ListViewItem placeholder = new ListViewItem("No instructors found");
                    placeholder.ForeColor = Color.Gray;
                    instructorsListView.Items.Add(placeholder);
                }
                
                // Apply alternating row colors
                ApplyAlternatingRowColors();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading instructors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Instructor> GetAllInstructorsFromDatabase(string searchTerm = "", string sortBy = "InstructorID")
        {
            List<Instructor> instructorsList = new List<Instructor>();
            
            try
            {
                using (var conn = new SQLiteConnection(instructorSystem.GetConnectionString()))
                {
                    conn.Open();
                    
                    string query = "SELECT * FROM Instructors";
                    
                    // Add search condition if provided
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " WHERE InstructorID LIKE @search OR Name LIKE @search OR Phone LIKE @search";
                    }
                    
                    // Add sorting
                    query += $" ORDER BY {sortBy}";
                    
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@search", $"%{searchTerm}%");
                        }
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string instructorId = reader["InstructorID"].ToString() ?? string.Empty;
                                string name = reader["Name"].ToString() ?? string.Empty;
                                string phone = reader["Phone"].ToString() ?? string.Empty;
                                
                                Instructor instructor = new Instructor(instructorId, name, phone);
                                instructorsList.Add(instructor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return instructorsList;
        }

        private void ApplyFilters()
        {
            string searchTerm = txtSearch.Text;
            string sortField = "InstructorID"; // Default sort field
            
            // Determine sort field based on selection
            switch (cmbSortOrder.SelectedIndex)
            {
                case 0: sortField = "InstructorID"; break;
                case 1: sortField = "Name"; break;
                case 2: sortField = "Phone"; break;
            }
            
            LoadInstructorsList(searchTerm, sortField);
        }

        private void ApplyAlternatingRowColors()
        {
            // Apply alternating row colors for better readability
            for (int i = 0; i < instructorsListView.Items.Count; i++)
            {
                instructorsListView.Items[i].BackColor = i % 2 == 0 
                    ? Color.White 
                    : Color.FromArgb(245, 245, 250); // Light blue-gray
            }
        }

        private void UpdateSelectionStatus()
        {
            // Find the panels by name
            Panel? instructorDetailContainer = Controls.Find("pnlInstructorDetailContainer", true).FirstOrDefault() as Panel;
            Panel? noSelectionPanel = Controls.Find("pnlNoSelection", true).FirstOrDefault() as Panel;
            
            if (instructorDetailContainer == null || noSelectionPanel == null)
                return;

            if (instructorsListView.SelectedItems.Count > 0 && instructorsListView.SelectedItems[0].Tag is Instructor selectedInstructor)
            {
                // Show the table and hide "no selection" message
                noSelectionPanel.Visible = false;

                // Update each value label with the instructor properties
                UpdateValueLabel(instructorDetailContainer, "lblValue0", selectedInstructor.InstructorID);
                UpdateValueLabel(instructorDetailContainer, "lblValue1", selectedInstructor.Name);
                UpdateValueLabel(instructorDetailContainer, "lblValue2", selectedInstructor.Phone);
            }
            else
            {
                // Show "no selection" message and hide values
                noSelectionPanel.Visible = true;
            }
        }

        private void UpdateValueLabel(Panel container, string labelName, string value)
        {
            foreach (Control control in container.Controls)
            {
                if (control is TableLayoutPanel table)
                {
                    foreach (Control tableControl in table.Controls)
                    {
                        if (tableControl is Label label && label.Name == labelName)
                        {
                            label.Text = value;
                            return;
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            using (var form = new AddInstructorForm(instructorSystem))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadInstructorsList(); // Refresh the list
                    
                    // Use custom message dialog
                    using (var successDialog = new CustomMessageDialog(
                        "Success",
                        "Instructor successfully added.",
                        MessageType.Success))
                    {
                        successDialog.ShowDialog();
                    }
                }
            }
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            // Replace with the modern search form similar to SearchCarForm
            using (var searchForm = new SearchInstructorForm(instructorSystem))
            {
                searchForm.ShowDialog();
                // Refresh the list in case any changes were made
                LoadInstructorsList();
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (instructorsListView.SelectedItems.Count == 0 || !(instructorsListView.SelectedItems[0].Tag is Instructor selectedInstructor))
            {
                // Enhanced message when no instructor is selected
                using (var noSelectionDialog = new CustomMessageDialog(
                    "No Instructor Selected",
                    "Please select an instructor from the list to delete.",
                    MessageType.Warning))
                {
                    noSelectionDialog.ShowDialog();
                }
                return;
            }

            string id = selectedInstructor.InstructorID;
            
            // Create a professional confirmation dialog for deletion
            using (var confirmDialog = new ConfirmDeleteInstructorDialog(
                selectedInstructor.Name,
                $"Instructor ID: {selectedInstructor.InstructorID}\nPhone: {selectedInstructor.Phone}"))
            {
                if (confirmDialog.ShowDialog() == DialogResult.Yes)
                {
                    if (instructorSystem.DeleteInstructor(id))
                    {
                        LoadInstructorsList(); // Refresh the list
                        
                        // Enhanced success message
                        using (var successDialog = new CustomMessageDialog(
                            "Deletion Successful",
                            $"The instructor '{selectedInstructor.Name}' has been successfully deleted.",
                            MessageType.Success))
                        {
                            successDialog.ShowDialog();
                        }
                    }
                    else
                    {
                        // Enhanced error message
                        using (var errorDialog = new CustomMessageDialog(
                            "Deletion Failed",
                            "The instructor could not be deleted. They may be assigned to existing lessons.",
                            MessageType.Error))
                        {
                            errorDialog.ShowDialog();
                        }
                    }
                }
            }
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbSortOrder.SelectedIndex = 0;
            LoadInstructorsList();
            
            // Show refresh confirmation
            using (var refreshDialog = new CustomMessageDialog(
                "List Refreshed", 
                "The instructor list has been refreshed successfully.",
                MessageType.Information))
            {
                refreshDialog.ShowDialog();
            }
        }

        private void btnDisplaySorted_Click(object? sender, EventArgs e)
        {
            // Instead of opening a new form, just sort the current list
            cmbSortOrder.SelectedIndex = 1; // Sort by Name
            ApplyFilters();
        }
    }
}
