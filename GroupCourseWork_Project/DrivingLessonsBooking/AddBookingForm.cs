using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class AddBookingForm : Form
    {
        private BookingLogic bookingSystem;
        private StudentLogic studentSystem;
        private InstructorLogic instructorSystem;
        private CarLogic carSystem;

        private TextBox txtBookingID = null!;
        private ComboBox cmbStudent = null!;
        private ComboBox cmbInstructor = null!;
        private DateTimePicker dtpLessonDate = null!;
        private ComboBox cmbCar = null!;

        public AddBookingForm(BookingLogic booking, StudentLogic student, InstructorLogic instructor, CarLogic car)
        {
            bookingSystem = booking;
            studentSystem = student;
            instructorSystem = instructor;
            carSystem = car;
            InitializeComponent();
            PopulateStudents();
            PopulateInstructors();
            PopulateCars();
        }

        private void InitializeComponent()
        {
            // Apply consistent styling to the form
            BaseFormHelper.SetupStandardForm(this, "Add Booking", 450, 350);

            // Main container with padding
            Panel mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            // Create group box for input fields
            GroupBox inputGroupBox = BaseFormHelper.CreateStandardGroupBox("Booking Details");
            
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 6,
                ColumnCount = 2,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 30),
                    new ColumnStyle(SizeType.Percent, 70)
                }
            };

            // Add labels and controls with consistent styling
            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Booking ID:"), 0, 0);
            txtBookingID = BaseFormHelper.CreateStandardTextBox();
            mainPanel.Controls.Add(txtBookingID, 1, 0);

            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Student:"), 0, 1);
            cmbStudent = BaseFormHelper.CreateStandardComboBox();
            mainPanel.Controls.Add(cmbStudent, 1, 1);

            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Instructor:"), 0, 2);
            cmbInstructor = BaseFormHelper.CreateStandardComboBox();
            mainPanel.Controls.Add(cmbInstructor, 1, 2);

            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Lesson Date:"), 0, 3);
            dtpLessonDate = BaseFormHelper.CreateStandardDateTimePicker();
            mainPanel.Controls.Add(dtpLessonDate, 1, 3);

            mainPanel.Controls.Add(BaseFormHelper.CreateStandardLabel("Car:"), 0, 4);
            cmbCar = BaseFormHelper.CreateStandardComboBox();
            mainPanel.Controls.Add(cmbCar, 1, 4);

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

            mainPanel.Controls.Add(buttonPanel, 1, 5);

            // Add panel to group box
            inputGroupBox.Controls.Add(mainPanel);
            
            // Add group box to container
            mainContainer.Controls.Add(inputGroupBox);
            
            // Add container to form
            this.Controls.Add(mainContainer);
            
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void PopulateStudents()
        {
            // Existing code for populating students
            cmbStudent.Items.Clear();
            cmbStudent.Items.Add("Select a student...");
            cmbStudent.SelectedIndex = 0;
            
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT * FROM Students ORDER BY Name", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string? id = reader["StudentID"]?.ToString();
                            string? name = reader["Name"]?.ToString();
                            
                            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name))
                            {
                                // Store both name and ID for display and retrieval
                                cmbStudent.Items.Add($"{name} ({id})");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateInstructors()
        {
            // Existing code for populating instructors
            cmbInstructor.Items.Clear();
            cmbInstructor.Items.Add("Select an instructor...");
            cmbInstructor.SelectedIndex = 0;
            
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT * FROM Instructors ORDER BY Name", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string? id = reader["InstructorID"]?.ToString();
                            string? name = reader["Name"]?.ToString();
                            
                            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name))
                            {
                                cmbInstructor.Items.Add($"{name} ({id})");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading instructors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateCars()
        {
            // Existing code for populating cars
            cmbCar.Items.Clear();
            cmbCar.Items.Add("Select a car...");
            cmbCar.SelectedIndex = 0;
            
            try
            {
                using (var conn = new SQLiteConnection(bookingSystem.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT * FROM Cars ORDER BY Model", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string? id = reader["CarID"]?.ToString();
                            string? model = reader["Model"]?.ToString();
                            string? licensePlate = reader["LicensePlate"]?.ToString();
                            
                            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(model))
                            {
                                cmbCar.Items.Add($"{model} ({licensePlate}) - {id}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cars: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            // Existing validation and save logic
            if (cmbStudent.SelectedIndex <= 0 || cmbInstructor.SelectedIndex <= 0 || cmbCar.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a student, instructor, and car.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Extract IDs from selected items
            string? studentText = cmbStudent.SelectedItem?.ToString();
            string? instructorText = cmbInstructor.SelectedItem?.ToString();
            string? carText = cmbCar.SelectedItem?.ToString();
            
            if (studentText == null || instructorText == null || carText == null)
            {
                MessageBox.Show("Please select valid options for student, instructor, and car.", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Extract ID from format "Name (ID)"
            string studentID = ExtractID(studentText);
            string instructorID = ExtractID(instructorText);
            string carID = ExtractID(carText);
            
            // Get the selected date
            DateTime lessonDate = dtpLessonDate.Value;
            
            string bookingID = Guid.NewGuid().ToString().Substring(0, 8);
            
            // Create and save the booking
            if (bookingSystem.CreateBooking(bookingID, studentID, instructorID, lessonDate, carID))
            {
                MessageBox.Show("Booking added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Failed to add booking. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to extract ID from string format "Name (ID)"
        private string ExtractID(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
                
            int start = text.LastIndexOf('(') + 1;
            int end = text.LastIndexOf(')');
            if (start > 0 && end > start)
            {
                return text.Substring(start, end - start);
            }
            return string.Empty;
        }
    }
}
