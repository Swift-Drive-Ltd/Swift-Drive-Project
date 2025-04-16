using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;
using DrivingLessonsBooking.UI;

namespace DrivingLessonsBooking
{
    public partial class MainForm : Form
    {
        // Using null-forgiving operator to indicate these will be initialized in InitializeSystems
        private BookingLogic bookingSystem = null!;
        private StudentLogic studentSystem = null!;
        private InstructorLogic instructorSystem = null!;
        private CarLogic carSystem = null!;

        // Dashboard panels
        private Panel dashboardPanel = null!;
        private Panel statisticsPanel = null!;

        // Database connection string
        private string connectionString = "Data Source=C:\\sqlite\\gui\\driving-lessons.db;Version=3;";

        public MainForm()
        {
            InitializeComponent();
            InitializeSystems();
            LoadDashboardStatistics();
        }

        private void InitializeSystems()
        {
            bookingSystem = new BookingLogic(10);
            studentSystem = new StudentLogic(10);
            instructorSystem = new InstructorLogic(10);
            carSystem = new CarLogic(10);
        }

        private void btnBookings_Click(object? sender, EventArgs e)
        {
            using (var form = new BookingManagementForm(bookingSystem, studentSystem, instructorSystem, carSystem))
            {
                form.ShowDialog();
                // Refresh dashboard stats after management form closes
                LoadDashboardStatistics();
            }
        }

        private void btnStudents_Click(object? sender, EventArgs e)
        {
            using (var form = new StudentManagementForm(studentSystem))
            {
                form.ShowDialog();
                // Refresh dashboard stats after management form closes
                LoadDashboardStatistics();
            }
        }

        private void btnInstructors_Click(object? sender, EventArgs e)
        {
            using (var form = new InstructorManagementForm(instructorSystem))
            {
                form.ShowDialog();
                // Refresh dashboard stats after management form closes
                LoadDashboardStatistics();
            }
        }

        private void btnCars_Click(object? sender, EventArgs e)
        {
            using (var form = new CarManagementForm(carSystem))
            {
                form.ShowDialog();
                // Refresh dashboard stats after management form closes
                LoadDashboardStatistics();
            }
        }

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void LoadDashboardStatistics()
        {
            try
            {
                // Get counts from database for statistics
                int bookingsCount = GetBookingsCount();
                int studentsCount = GetStudentsCount();
                int instructorsCount = GetInstructorsCount();
                int carsCount = GetCarsCount();

                // Update statistics UI
                UpdateStatisticsUI(bookingsCount, studentsCount, instructorsCount, carsCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard statistics: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetBookingsCount()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    // Check if the table exists first
                    using (var checkCmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Bookings'", conn))
                    {
                        if (checkCmd.ExecuteScalar() == null)
                            return 0; // Table doesn't exist
                    }

                    // Get count from Bookings table
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Bookings", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting bookings count: {ex.Message}");
            }
            return 0;
        }

        private int GetStudentsCount()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    // Check if the table exists first
                    using (var checkCmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Students'", conn))
                    {
                        if (checkCmd.ExecuteScalar() == null)
                            return 0; // Table doesn't exist
                    }

                    // Get count from Students table
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Students", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting students count: {ex.Message}");
            }
            return 0;
        }

        private int GetInstructorsCount()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    // Check if the table exists first
                    using (var checkCmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Instructors'", conn))
                    {
                        if (checkCmd.ExecuteScalar() == null)
                            return 0; // Table doesn't exist
                    }

                    // Get count from Instructors table
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Instructors", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting instructors count: {ex.Message}");
            }
            return 0;
        }

        private int GetCarsCount()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    // Check if the table exists first
                    using (var checkCmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Cars'", conn))
                    {
                        if (checkCmd.ExecuteScalar() == null)
                            return 0; // Table doesn't exist
                    }

                    // Get count from Cars table
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Cars", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting cars count: {ex.Message}");
            }
            return 0;
        }

        private void UpdateStatisticsUI(int bookings, int students, int instructors, int cars)
        {
            // This will update the statistics panel with counts
            if (statisticsPanel != null && statisticsPanel.Controls.Count >= 4)
            {
                // Update the value labels with current counts
                // Each card has a value label - we need to find that specific label
                ((Label)statisticsPanel.Controls[0].Controls[2]).Text = bookings.ToString(); // Get valueLabel (3rd control)
                ((Label)statisticsPanel.Controls[1].Controls[2]).Text = students.ToString();
                ((Label)statisticsPanel.Controls[2].Controls[2]).Text = instructors.ToString();
                ((Label)statisticsPanel.Controls[3].Controls[2]).Text = cars.ToString();
            }
        }

        private void InitializeComponent()
        {
            // Set up form to be maximized with modern styling
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Swift Drive - Driving Lessons Booking System";
            this.BackColor = Color.FromArgb(245, 245, 245); // Light grey background

            // Main container with gradient sidebar and content area
            TableLayoutPanel mainContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 20), // Sidebar takes 20%
                    new ColumnStyle(SizeType.Percent, 80)  // Content takes 80%
                }
            };

            // Create a gradient sidebar panel
            Panel sidebar = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(37, 57, 111), // Deep blue
            };

            // Add logo and app name to sidebar
            PictureBox logoBox = new PictureBox
            {
                Size = new Size(120, 120),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(50, 20),
                BackColor = Color.Transparent
            };
            // Add this when you have the logo: logoBox.Image = Properties.Resources.LogoImage;
            sidebar.Controls.Add(logoBox);

            Label appNameLabel = new Label
            {
                Text = "Swift Drive",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 150),
                BackColor = Color.Transparent
            };
            sidebar.Controls.Add(appNameLabel);

            // Create a container for sidebar buttons
            FlowLayoutPanel sidebarButtonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.None,
                FlowDirection = FlowDirection.TopDown,
                Width = 200,
                Height = 300,
                Location = new Point(10, 200),
                BackColor = Color.Transparent,
                AutoSize = true
            };

            // Add styled navigation buttons
            Button btnDashboard = CreateSidebarButton("Dashboard", null, true);
            btnDashboard.Click += (s, e) => ShowDashboard();
            sidebarButtonsPanel.Controls.Add(btnDashboard);

            Button btnBookings = CreateSidebarButton("Manage Bookings", btnBookings_Click);
            sidebarButtonsPanel.Controls.Add(btnBookings);

            Button btnStudents = CreateSidebarButton("Manage Students", btnStudents_Click);
            sidebarButtonsPanel.Controls.Add(btnStudents);

            Button btnInstructors = CreateSidebarButton("Manage Instructors", btnInstructors_Click);
            sidebarButtonsPanel.Controls.Add(btnInstructors);

            Button btnCars = CreateSidebarButton("Manage Cars", btnCars_Click);
            sidebarButtonsPanel.Controls.Add(btnCars);

            Button btnExit = CreateSidebarButton("Exit Application", btnExit_Click);
            sidebarButtonsPanel.Controls.Add(btnExit);

            sidebar.Controls.Add(sidebarButtonsPanel);
            mainContainer.Controls.Add(sidebar, 0, 0);

            // Create main content panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Create dashboard content panel
            dashboardPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // Add dashboard header
            Label dashboardHeader = new Label
            {
                Text = "Welcome to Swift Drive",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 57, 111),
                AutoSize = true,
                Location = new Point(30, 30)
            };
            dashboardPanel.Controls.Add(dashboardHeader);

            Label dashboardSubtitle = new Label
            {
                Text = "Driving Lessons Booking Management System",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(30, 70)
            };
            dashboardPanel.Controls.Add(dashboardSubtitle);

            // Add statistics panels
            statisticsPanel = CreateStatisticsPanel();
            dashboardPanel.Controls.Add(statisticsPanel);

            // Add a welcome message panel
            Panel welcomePanel = new Panel
            {
                Width = 800,
                Height = 200,
                Location = new Point(30, 300),
                BackColor = Color.FromArgb(245, 245, 250),
                BorderStyle = BorderStyle.None
            };
            
            Label welcomeTitle = new Label
            {
                Text = "Getting Started",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 57, 111),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            welcomePanel.Controls.Add(welcomeTitle);
            
            Label welcomeText = new Label
            {
                Text = "Use the sidebar navigation to manage your driving school operations. View and manage bookings, students, instructors, and cars from their respective sections.",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80),
                Width = 760,
                Height = 100,
                Location = new Point(20, 60),
                TextAlign = ContentAlignment.TopLeft
            };
            welcomePanel.Controls.Add(welcomeText);
            
            dashboardPanel.Controls.Add(welcomePanel);

            contentPanel.Controls.Add(dashboardPanel);
            mainContainer.Controls.Add(contentPanel, 1, 0);

            this.Controls.Add(mainContainer);
        }

        private Panel CreateStatisticsPanel()
        {
            Panel statsPanel = new Panel
            {
                Width = 800,
                Height = 150,
                Location = new Point(30, 120),
                BackColor = Color.White
            };

            // Bookings stats
            Panel bookingStats = CreateStatsCard("Total Bookings", "0", Color.FromArgb(41, 128, 185));
            bookingStats.Location = new Point(0, 0);
            statsPanel.Controls.Add(bookingStats);

            // Students stats
            Panel studentStats = CreateStatsCard("Total Students", "0", Color.FromArgb(39, 174, 96));
            studentStats.Location = new Point(205, 0);
            statsPanel.Controls.Add(studentStats);

            // Instructors stats
            Panel instructorStats = CreateStatsCard("Total Instructors", "0", Color.FromArgb(142, 68, 173));
            instructorStats.Location = new Point(410, 0);
            statsPanel.Controls.Add(instructorStats);

            // Cars stats
            Panel carsStats = CreateStatsCard("Total Cars", "0", Color.FromArgb(230, 126, 34));
            carsStats.Location = new Point(615, 0);
            statsPanel.Controls.Add(carsStats);

            return statsPanel;
        }

        private Panel CreateStatsCard(string title, string value, Color accentColor)
        {
            Panel card = new Panel
            {
                Width = 190,
                Height = 120,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Add a border with accent color at the top
            Panel accent = new Panel
            {
                Height = 5,
                Width = 190,
                BackColor = accentColor,
                Location = new Point(0, 0)
            };
            card.Controls.Add(accent);

            // Add shadow effect
            card.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid);
            };

            // Title label
            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(15, 20)
            };
            card.Controls.Add(titleLabel);

            // Value label
            Label valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = accentColor,
                AutoSize = true,
                Location = new Point(15, 50)
            };
            card.Controls.Add(valueLabel);

            return card;
        }

        private Button CreateSidebarButton(string text, EventHandler? clickHandler, bool isActive = false)
        {
            Button button = new Button
            {
                Text = text,
                Width = 180,
                Height = 45,
                Margin = new Padding(0, 5, 0, 5),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Padding = new Padding(10, 0, 0, 0)
            };

            if (isActive)
            {
                button.BackColor = Color.FromArgb(57, 77, 131);  // Slightly lighter than sidebar
                button.ForeColor = Color.White;
                button.FlatAppearance.BorderSize = 0;
            }
            else
            {
                button.BackColor = Color.Transparent;
                button.ForeColor = Color.FromArgb(200, 200, 200);  // Light gray
                button.FlatAppearance.BorderSize = 0;
                
                // Hover effect
                button.MouseEnter += (s, e) => {
                    button.BackColor = Color.FromArgb(47, 67, 121);
                    button.ForeColor = Color.White;
                };
                
                button.MouseLeave += (s, e) => {
                    button.BackColor = Color.Transparent;
                    button.ForeColor = Color.FromArgb(200, 200, 200);
                };
            }

            if (clickHandler != null)
            {
                button.Click += clickHandler;
            }

            return button;
        }

        private void ShowDashboard()
        {
            // Show dashboard panel and hide other panels
            if (dashboardPanel != null)
            {
                dashboardPanel.BringToFront();
                dashboardPanel.Visible = true;
                // Refresh statistics
                LoadDashboardStatistics();
            }
        }
    }
}
