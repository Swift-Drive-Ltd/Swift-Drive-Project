using System;
using System.Windows.Forms;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class AddCarForm : Form
    {
        private CarLogic carSystem;
        // Using the null-forgiving operator (!) to indicate these will be initialized in InitializeComponent
        private TextBox txtCarID = null!;
        private TextBox txtModel = null!;
        private ComboBox cmbType = null!;
        private TextBox txtLicensePlate = null!;

        public AddCarForm(CarLogic car)
        {
            carSystem = car;
            InitializeComponent();
            PopulateCarTypes();
        }

        private void InitializeComponent()
        {
            // Apply consistent styling to the form
            BaseFormHelper.SetupStandardForm(this, "Add Car", 450, 300);

            // Main container with padding
            Panel mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            // Create group box for input fields
            GroupBox inputGroupBox = BaseFormHelper.CreateStandardGroupBox("Car Details");
            
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 5,
                ColumnCount = 2,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 30),
                    new ColumnStyle(SizeType.Percent, 70)
                }
            };

            // Add labels and controls with consistent styling
            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Car ID:"), 0, 0);
            txtCarID = BaseFormHelper.CreateStandardTextBox();
            mainPanel.Controls.Add(txtCarID, 1, 0);

            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Model:"), 0, 1);
            txtModel = BaseFormHelper.CreateStandardTextBox();
            mainPanel.Controls.Add(txtModel, 1, 1);

            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Type:"), 0, 2);
            cmbType = BaseFormHelper.CreateStandardComboBox();
            mainPanel.Controls.Add(cmbType, 1, 2);

            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("License Plate:"), 0, 3);
            txtLicensePlate = BaseFormHelper.CreateStandardTextBox();
            mainPanel.Controls.Add(txtLicensePlate, 1, 3);

            // Button panel
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            // Create buttons with consistent styling
            Button btnCancel = new Button
            {
                Text = "Cancel",
                Width = 80,
                Height = 30,
                Margin = new Padding(5),
                Font = BaseFormHelper.RegularFont
            };
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
            buttonPanel.Controls.Add(btnCancel);

            Button btnSave = new Button
            {
                Text = "Save",
                Width = 80,
                Height = 30,
                Margin = new Padding(5),
                Font = BaseFormHelper.RegularFont
            };
            btnSave.Click += btnSave_Click;
            buttonPanel.Controls.Add(btnSave);

            mainPanel.Controls.Add(buttonPanel, 1, 4);

            // Add panel to group box
            inputGroupBox.Controls.Add(mainPanel);
            
            // Add group box to container
            mainContainer.Controls.Add(inputGroupBox);
            
            // Add container to form
            this.Controls.Add(mainContainer);
            
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void PopulateCarTypes()
        {
            cmbType.Items.Add("Manual");
            cmbType.Items.Add("Automatic");
            cmbType.SelectedIndex = 0;
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            // Validation logic remains the same
            if (string.IsNullOrWhiteSpace(txtCarID.Text))
            {
                MessageBox.Show("Please enter a Car ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Please enter a Model.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLicensePlate.Text))
            {
                MessageBox.Show("Please enter a License Plate.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if selected item is null before calling ToString()
            string carType = cmbType.SelectedItem?.ToString() ?? "Unknown";

            Car car = new Car(
                txtCarID.Text,
                txtModel.Text,
                carType,
                txtLicensePlate.Text
            );

            carSystem.AddCar(car);
            DialogResult = DialogResult.OK;
        }
    }
}
