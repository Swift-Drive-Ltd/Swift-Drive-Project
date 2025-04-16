using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class SearchCarForm : Form
    {
        private CarLogic carSystem;
        
        // Using null-forgiving operator to indicate these will be initialized in InitializeComponent
        private TextBox txtCarId = null!;
        private TextBox txtModel = null!;
        private ComboBox cmbType = null!;
        private TextBox txtLicensePlate = null!;
        private ListView resultsListView = null!;
        private Label lblResultsCount = null!;

        public SearchCarForm(CarLogic car)
        {
            carSystem = car;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Base form setup
            BaseFormHelper.SetupStandardForm(this, "Search Cars", 800, 500);
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

            // Car ID search field
            Label lblCarId = new Label
            {
                Text = "Car ID:",
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill
            };
            searchFieldsPanel.Controls.Add(lblCarId, 0, 0);

            txtCarId = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };
            searchFieldsPanel.Controls.Add(txtCarId, 1, 0);

            // Model search field
            Label lblModel = new Label
            {
                Text = "Model:",
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill
            };
            searchFieldsPanel.Controls.Add(lblModel, 2, 0);

            txtModel = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };
            searchFieldsPanel.Controls.Add(txtModel, 3, 0);

            // Type search field
            Label lblType = new Label
            {
                Text = "Type:",
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill
            };
            searchFieldsPanel.Controls.Add(lblType, 0, 1);

            cmbType = new ComboBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(5),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbType.Items.Add(""); // Empty option for "any"
            cmbType.Items.Add("Manual");
            cmbType.Items.Add("Automatic");
            cmbType.SelectedIndex = 0;
            searchFieldsPanel.Controls.Add(cmbType, 1, 1);

            // License Plate search field
            Label lblLicensePlate = new Label
            {
                Text = "License Plate:",
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill
            };
            searchFieldsPanel.Controls.Add(lblLicensePlate, 2, 1);

            txtLicensePlate = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };
            searchFieldsPanel.Controls.Add(txtLicensePlate, 3, 1);

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
                txtCarId.Text = "";
                txtModel.Text = "";
                cmbType.SelectedIndex = 0;
                txtLicensePlate.Text = "";
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
            resultsListView.Columns.Add("Car ID", 120);
            resultsListView.Columns.Add("Model", 200);
            resultsListView.Columns.Add("Type", 150);
            resultsListView.Columns.Add("License Plate", 180);
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
            this.Load += (s, e) => txtCarId.Focus();
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            string carId = txtCarId.Text.Trim();
            string model = txtModel.Text.Trim();
            string type = cmbType.SelectedIndex > 0 ? cmbType.SelectedItem?.ToString() ?? "" : "";
            string licensePlate = txtLicensePlate.Text.Trim();

            // Check if any search criteria is provided
            if (string.IsNullOrEmpty(carId) && string.IsNullOrEmpty(model) && 
                string.IsNullOrEmpty(type) && string.IsNullOrEmpty(licensePlate))
            {
                MessageBox.Show("Please enter at least one search criterion.", 
                    "Search Criteria Needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Perform the search
            List<Car> searchResults = SearchCars(carId, model, type, licensePlate);
            DisplaySearchResults(searchResults);
        }

        private List<Car> SearchCars(string carId, string model, string type, string licensePlate)
        {
            List<Car> results = new List<Car>();

            try
            {
                using (var conn = new SQLiteConnection(carSystem.GetConnectionString()))
                {
                    conn.Open();

                    // Build the query based on provided criteria
                    string query = "SELECT * FROM Cars WHERE 1=1"; // Start with a condition that's always true
                    
                    // Add search conditions
                    if (!string.IsNullOrEmpty(carId))
                        query += " AND CarID LIKE @carId";
                    
                    if (!string.IsNullOrEmpty(model))
                        query += " AND Model LIKE @model";
                    
                    if (!string.IsNullOrEmpty(type))
                        query += " AND Type = @type";
                    
                    if (!string.IsNullOrEmpty(licensePlate))
                        query += " AND LicensePlate LIKE @licensePlate";
                    
                    // Add an order by clause
                    query += " ORDER BY Model";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        // Add parameters with wildcards for flexible matching
                        if (!string.IsNullOrEmpty(carId))
                            cmd.Parameters.AddWithValue("@carId", $"%{carId}%");
                        
                        if (!string.IsNullOrEmpty(model))
                            cmd.Parameters.AddWithValue("@model", $"%{model}%");
                        
                        if (!string.IsNullOrEmpty(type))
                            cmd.Parameters.AddWithValue("@type", type);
                        
                        if (!string.IsNullOrEmpty(licensePlate))
                            cmd.Parameters.AddWithValue("@licensePlate", $"%{licensePlate}%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create car objects from the results
                                string id = reader["CarID"]?.ToString() ?? string.Empty;
                                string carModel = reader["Model"]?.ToString() ?? string.Empty;
                                string carType = reader["Type"]?.ToString() ?? string.Empty;
                                string plate = reader["LicensePlate"]?.ToString() ?? string.Empty;

                                Car car = new Car(id, carModel, carType, plate);
                                results.Add(car);
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

        private void DisplaySearchResults(List<Car> results)
        {
            resultsListView.Items.Clear();
            
            if (results.Count == 0)
            {
                lblResultsCount.Text = "No cars found matching your criteria.";
                lblResultsCount.ForeColor = Color.FromArgb(150, 30, 30); // Dark red
                return;
            }

            // Update results count
            lblResultsCount.Text = $"Found {results.Count} car(s) matching your criteria.";
            lblResultsCount.ForeColor = Color.FromArgb(0, 120, 0); // Dark green

            // Populate results
            foreach (Car car in results)
            {
                ListViewItem item = new ListViewItem(car.CarID);
                item.SubItems.Add(car.Model);
                item.SubItems.Add(car.Type);
                item.SubItems.Add(car.LicensePlate);
                
                // Store the full car object for later use
                item.Tag = car;
                
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
            // Show detailed car information when double-clicking a result
            if (resultsListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = resultsListView.SelectedItems[0];
                
                if (selectedItem.Tag is Car car)
                {
                    DisplayDetailedCarInfo(car);
                }
            }
        }

        private void DisplayDetailedCarInfo(Car car)
        {
            // Create a nicely formatted detailed view
            string detailMessage = 
                $"Car Details:\n\n" +
                $"Car ID: {car.CarID}\n" +
                $"Model: {car.Model}\n" +
                $"Type: {car.Type}\n" +
                $"License Plate: {car.LicensePlate}\n";

            MessageBox.Show(detailMessage, "Car Details", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
