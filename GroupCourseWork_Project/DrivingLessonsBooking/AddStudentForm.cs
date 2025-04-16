using System;
using System.Windows.Forms;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class AddStudentForm : Form
    {
        private StudentLogic studentSystem;
        // Using null-forgiving operator to indicate these will be initialized in InitializeComponent
        private TextBox txtStudentID = null!;
        private TextBox txtName = null!;
        private TextBox txtEmail = null!;

        public AddStudentForm(StudentLogic student)
        {
            studentSystem = student;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Apply consistent styling to the form
            BaseFormHelper.SetupStandardForm(this, "Add Student", 450, 300);

            // Main container with padding
            Panel mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            // Create group box for input fields
            GroupBox inputGroupBox = BaseFormHelper.CreateStandardGroupBox("Student Details");
            inputGroupBox.Dock = DockStyle.Fill;
            
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 2,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 30),
                    new ColumnStyle(SizeType.Percent, 70)
                }
            };

            // Header with blue accent
            Panel headerPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(37, 57, 111)
            };

            Label headerLabel = new Label
            {
                Text = "Add New Student",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            headerPanel.Controls.Add(headerLabel);
            mainContainer.Controls.Add(headerPanel);

            // Add labels and controls with consistent styling
            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Student ID:"), 0, 0);
            txtStudentID = BaseFormHelper.CreateStandardTextBox();
            mainPanel.Controls.Add(txtStudentID, 1, 0);

            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Name:"), 0, 1);
            txtName = BaseFormHelper.CreateStandardTextBox();
            mainPanel.Controls.Add(txtName, 1, 1);

            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Email:"), 0, 2);
            txtEmail = BaseFormHelper.CreateStandardTextBox();
            mainPanel.Controls.Add(txtEmail, 1, 2);

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
                Width = 100,
                Height = 40,
                Margin = new Padding(5),
                Font = BaseFormHelper.RegularFont,
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
            
            // Add hover effects
            btnCancel.MouseEnter += (s, e) => {
                btnCancel.BackColor = Color.FromArgb(230, 230, 230);
            };
            btnCancel.MouseLeave += (s, e) => {
                btnCancel.BackColor = Color.FromArgb(240, 240, 240);
            };
            
            buttonPanel.Controls.Add(btnCancel);

            Button btnSave = new Button
            {
                Text = "Save Student",
                Width = 130,
                Height = 40,
                Margin = new Padding(5),
                Font = new Font(BaseFormHelper.RegularFont.FontFamily, 11, FontStyle.Bold),
                BackColor = Color.FromArgb(37, 57, 111),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            
            // Add hover effects
            btnSave.MouseEnter += (s, e) => {
                btnSave.BackColor = Color.FromArgb(57, 77, 131);
            };
            btnSave.MouseLeave += (s, e) => {
                btnSave.BackColor = Color.FromArgb(37, 57, 111);
            };
            
            buttonPanel.Controls.Add(btnSave);

            mainPanel.Controls.Add(buttonPanel, 1, 3);

            // Add panel to group box
            inputGroupBox.Controls.Add(mainPanel);
            
            // Create a container for the group box with proper padding
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 70, 10, 10) // Extra top padding to account for header
            };
            
            // Add group box to content panel
            contentPanel.Controls.Add(inputGroupBox);
            
            // Add content panel to container
            mainContainer.Controls.Add(contentPanel);
            
            // Add container to form
            this.Controls.Add(mainContainer);
            
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            // Validation logic remains the same
            if (string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                MessageBox.Show("Please enter a Student ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter an Email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Student student = new Student(
                txtStudentID.Text,
                txtName.Text,
                txtEmail.Text
            );

            studentSystem.AddStudent(student);
            DialogResult = DialogResult.OK;
        }
    }
}
