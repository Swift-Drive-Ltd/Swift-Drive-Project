using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class CarManagementForm : Form
    {
        private CarLogic carSystem;
        // Using null-forgiving operator to indicate this will be initialized in InitializeComponent
        private ListView carsListView = null!;
        private ComboBox cmbSortOrder = null!;
        private TextBox txtSearch = null!;
        private Button btnApplyFilter = null!;

        public CarManagementForm(CarLogic car)
        {
            carSystem = car;
            InitializeComponent();
            LoadCarsList();
        }

        private void InitializeComponent()
        {
            // Set up form with modern styling
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Car Management - Swift Drive";
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

            // Content panel with cars list and action buttons
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
                Text = "Car Management",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 57, 111),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            headerPanel.Controls.Add(titleLabel);

            // Cars list panel
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
                PlaceholderText = "Search by ID or model..."
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
            cmbSortOrder.Items.Add("Car ID (A-Z)");
            cmbSortOrder.Items.Add("Model (A-Z)");
            cmbSortOrder.Items.Add("Type (A-Z)");
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

            // Create ListView with columns for car details
            carsListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                MultiSelect = false
            };
            carsListView.Columns.Add("Car ID", 100);
            carsListView.Columns.Add("Model", 200);
            carsListView.Columns.Add("Type", 150);
            carsListView.Columns.Add("License Plate", 150);
            carsListView.ItemSelectionChanged += (s, e) => UpdateSelectionStatus();

            listPanel.Controls.Add(filterPanel);
            listPanel.Controls.Add(carsListView);
            filterPanel.Dock = DockStyle.Top;
            carsListView.Dock = DockStyle.Fill;

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
                Text = "Car Actions",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20),
                ForeColor = Color.FromArgb(50, 50, 50)
            };
            actionsCard.Controls.Add(actionsTitle);

            // Create action buttons with consistent styling
            Button btnAdd = CreateActionButton("Add New Car", 20, 70);
            btnAdd.Click += btnAdd_Click;
            actionsCard.Controls.Add(btnAdd);

            Button btnSearch = CreateActionButton("Search Car", 20, 130);
            btnSearch.Click += btnSearch_Click;
            actionsCard.Controls.Add(btnSearch);

            Button btnDelete = CreateActionButton("Delete Selected Car", 20, 190);
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
                Text = "Selected Car Details",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 15),
                ForeColor = Color.FromArgb(50, 50, 50)
            };
            selectionPanel.Controls.Add(selectionTitle);

            // Create a panel with auto-scroll capability to contain the car detail "table"
            Panel carDetailContainer = new Panel
            {
                AutoScroll = true,
                BorderStyle = BorderStyle.None,
                Location = new Point(20, 45),
                Size = new Size(300, 90),
                Name = "pnlCarDetailContainer"
            };
            selectionPanel.Controls.Add(carDetailContainer);

            // Create table-like layout for car details
            TableLayoutPanel carDetailsTable = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 5, // Accommodate additional properties if needed in the future
                Size = new Size(280, 100),
                Dock = DockStyle.Top,
                AutoSize = true,
                Name = "tblCarDetails"
            };

            // Add column styles
            carDetailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            carDetailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));

            // Add row styles
            for (int i = 0; i < 5; i++)
            {
                carDetailsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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
            carDetailsTable.Controls.Add(headerProperty, 0, 0);

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
            carDetailsTable.Controls.Add(headerValue, 1, 0);

            // Create property rows with alternating colors
            string[] propertyNames = { "Car ID", "Model", "Type", "License Plate" };
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
                carDetailsTable.Controls.Add(propertyLabel, 0, i + 1);

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
                carDetailsTable.Controls.Add(valueLabel, 1, i + 1);
            }

            carDetailContainer.Controls.Add(carDetailsTable);
            
            // Add "No selection" message panel to show when no car is selected
            Panel noSelectionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = true,
                Name = "pnlNoSelection"
            };
            
            Label noSelectionLabel = new Label
            {
                Text = "No car selected. Select a car from the list to see details.",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            noSelectionPanel.Controls.Add(noSelectionLabel);
            carDetailContainer.Controls.Add(noSelectionPanel);
            
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

        private void LoadCarsList(string searchTerm = "", string sortBy = "CarID")
        {
            carsListView.Items.Clear();
            
            try
            {
                List<Car> cars = GetAllCarsFromDatabase(searchTerm, sortBy);
                
                if (cars.Count > 0)
                {
                    foreach (Car car in cars)
                    {
                        ListViewItem item = new ListViewItem(car.CarID);
                        item.SubItems.Add(car.Model);
                        item.SubItems.Add(car.Type);
                        item.SubItems.Add(car.LicensePlate);
                        item.Tag = car; // Store the car object for later use
                        carsListView.Items.Add(item);
                    }
                }
                else
                {
                    // No cars found, add a placeholder item
                    ListViewItem placeholder = new ListViewItem("No cars found");
                    placeholder.ForeColor = Color.Gray;
                    carsListView.Items.Add(placeholder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cars: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Car> GetAllCarsFromDatabase(string searchTerm = "", string sortBy = "CarID")
        {
            List<Car> carsList = new List<Car>();
            
            try
            {
                using (var conn = new SQLiteConnection(carSystem.GetConnectionString()))
                {
                    conn.Open();
                    
                    string query = "SELECT * FROM Cars";
                    
                    // Add search condition if provided
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " WHERE CarID LIKE @search OR Model LIKE @search OR Type LIKE @search OR LicensePlate LIKE @search";
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
                                string carId = reader["CarID"].ToString() ?? string.Empty;
                                string model = reader["Model"].ToString() ?? string.Empty;
                                string type = reader["Type"].ToString() ?? string.Empty;
                                string licensePlate = reader["LicensePlate"].ToString() ?? string.Empty;
                                
                                Car car = new Car(carId, model, type, licensePlate);
                                carsList.Add(car);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return carsList;
        }

        private void UpdateSelectionStatus()
        {
            // Find the panels by name
            Panel? carDetailContainer = Controls.Find("pnlCarDetailContainer", true).FirstOrDefault() as Panel;
            Panel? noSelectionPanel = Controls.Find("pnlNoSelection", true).FirstOrDefault() as Panel;
            
            if (carDetailContainer == null || noSelectionPanel == null)
                return;

            if (carsListView.SelectedItems.Count > 0 && carsListView.SelectedItems[0].Tag is Car selectedCar)
            {
                // Show the table and hide "no selection" message
                noSelectionPanel.Visible = false;

                // Update each value label with the car properties
                UpdateValueLabel(carDetailContainer, "lblValue0", selectedCar.CarID);
                UpdateValueLabel(carDetailContainer, "lblValue1", selectedCar.Model);
                UpdateValueLabel(carDetailContainer, "lblValue2", selectedCar.Type);
                UpdateValueLabel(carDetailContainer, "lblValue3", selectedCar.LicensePlate);
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

        private void ApplyFilters()
        {
            string searchTerm = txtSearch.Text;
            string sortField = "CarID"; // Default sort field
            
            // Determine sort field based on selection
            switch (cmbSortOrder.SelectedIndex)
            {
                case 0: sortField = "CarID"; break;
                case 1: sortField = "Model"; break;
                case 2: sortField = "Type"; break;
            }
            
            LoadCarsList(searchTerm, sortField);
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            using (var form = new AddCarForm(carSystem))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCarsList(); // Refresh the list
                    MessageBox.Show("Car successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            // Open the new professional search form instead of the simple input box
            using (var searchForm = new SearchCarForm(carSystem))
            {
                searchForm.ShowDialog();
                // Refresh the list in case any changes were made
                LoadCarsList();
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (carsListView.SelectedItems.Count == 0 || !(carsListView.SelectedItems[0].Tag is Car selectedCar))
            {
                // Enhanced message when no car is selected
                using (var noSelectionDialog = new CustomMessageDialog(
                    "No Car Selected",
                    "Please select a car from the list to delete.",
                    MessageType.Warning))
                {
                    noSelectionDialog.ShowDialog();
                }
                return;
            }

            string id = selectedCar.CarID;
            
            // Create a professional confirmation dialog for deletion
            using (var confirmDialog = new ConfirmDeleteCarDialog(
                selectedCar.Model,
                $"Car ID: {selectedCar.CarID}\nType: {selectedCar.Type}\nLicense Plate: {selectedCar.LicensePlate}"))
            {
                if (confirmDialog.ShowDialog() == DialogResult.Yes)
                {
                    if (carSystem.DeleteCar(id))
                    {
                        LoadCarsList(); // Refresh the list
                        
                        // Enhanced success message
                        using (var successDialog = new CustomMessageDialog(
                            "Deletion Successful",
                            $"The car '{selectedCar.Model}' has been successfully deleted.",
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
                            "The car could not be deleted. It may be in use by existing bookings.",
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
            LoadCarsList();
        }

        private void btnDisplaySorted_Click(object? sender, EventArgs e)
        {
            // Instead of opening a new form, just sort the current list
            cmbSortOrder.SelectedIndex = 1; // Sort by Model
            ApplyFilters();
        }
    }
}
