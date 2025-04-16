using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrivingLessonsBooking.UI
{
    public static class BaseFormHelper
    {
        // Common font definitions
        public static Font TitleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        public static Font SubtitleFont = new Font("Segoe UI", 14, FontStyle.Regular);
        public static Font RegularFont = new Font("Segoe UI", 10);
        public static Font ButtonFont = new Font("Segoe UI", 11);
        public static Font GroupBoxFont = new Font("Segoe UI", 11);
        
        // Common colors
        public static Color PrimaryColor = Color.FromArgb(37, 57, 111); // Deep blue
        public static Color SecondaryColor = Color.FromArgb(57, 77, 131); // Lighter blue
        public static Color AccentColor = Color.FromArgb(0, 149, 255); // Bright blue
        public static Color TextColor = Color.FromArgb(51, 51, 51); // Dark gray for text
        public static Color BackgroundColor = Color.FromArgb(245, 245, 245); // Light gray background
        public static Color CardBackgroundColor = Color.White;
        
        // Common sizes
        public static Size ButtonSize = new Size(200, 40);
        public static Size MainButtonSize = new Size(250, 50);
        public static Size ActionButtonSize = new Size(180, 40);
        public static Size TextBoxSize = new Size(200, 30);
        
        // Common padding
        public static Padding StandardPadding = new Padding(15);
        public static Padding ControlMargin = new Padding(5);
        
        // Setup standard form properties
        public static void SetupStandardForm(Form form, string title, int width = 800, int height = 600)
        {
            form.Text = title;
            form.Size = new Size(width, height);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.Font = RegularFont;
            form.BackColor = BackgroundColor;
        }
        
        // Setup fullscreen form properties
        public static void SetupFullscreenForm(Form form, string title)
        {
            form.Text = title;
            form.WindowState = FormWindowState.Maximized;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.Font = RegularFont;
            form.BackColor = BackgroundColor;
        }
        
        // Create standard button
        public static Button CreateStandardButton(string text, EventHandler? clickHandler)
        {
            Button button = new Button
            {
                Text = text,
                Size = ButtonSize,
                Font = ButtonFont,
                Margin = ControlMargin,
                Dock = DockStyle.Fill,
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            
            button.FlatAppearance.BorderSize = 0;
            
            // Add hover effect
            button.MouseEnter += (s, e) => {
                button.BackColor = SecondaryColor;
            };
            
            button.MouseLeave += (s, e) => {
                button.BackColor = PrimaryColor;
            };
            
            if (clickHandler != null)
                button.Click += clickHandler;
                
            return button;
        }
        
        // Create main menu button
        public static Button CreateMainMenuButton(string text, EventHandler? clickHandler)
        {
            Button button = new Button
            {
                Text = text,
                Size = MainButtonSize,
                Font = ButtonFont,
                Margin = new Padding(150, 10, 150, 10),
                Dock = DockStyle.Fill,
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            
            button.FlatAppearance.BorderSize = 0;
            
            // Add hover effect
            button.MouseEnter += (s, e) => {
                button.BackColor = SecondaryColor;
            };
            
            button.MouseLeave += (s, e) => {
                button.BackColor = PrimaryColor;
            };
            
            if (clickHandler != null)
                button.Click += clickHandler;
                
            return button;
        }
        
        // Create action button (smaller, for specific actions)
        public static Button CreateActionButton(string text, EventHandler? clickHandler)
        {
            Button button = new Button
            {
                Text = text,
                Size = ActionButtonSize,
                Font = ButtonFont,
                Margin = new Padding(5),
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            
            button.FlatAppearance.BorderSize = 0;
            
            // Add hover effect
            button.MouseEnter += (s, e) => {
                button.BackColor = SecondaryColor;
            };
            
            button.MouseLeave += (s, e) => {
                button.BackColor = PrimaryColor;
            };
            
            if (clickHandler != null)
                button.Click += clickHandler;
                
            return button;
        }
        
        // Create standard label
        public static Label CreateStandardLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = RegularFont,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                ForeColor = TextColor
            };
        }
        
        // Create title label
        public static Label CreateTitleLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = TitleFont,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = PrimaryColor
            };
        }
        
        // Create subtitle label
        public static Label CreateSubtitleLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = SubtitleFont,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                ForeColor = TextColor
            };
        }
        
        // Create standard text box
        public static TextBox CreateStandardTextBox()
        {
            return new TextBox
            {
                Font = RegularFont,
                Dock = DockStyle.Fill,
                Margin = ControlMargin,
                BorderStyle = BorderStyle.FixedSingle
            };
        }
        
        // Create standard combo box
        public static ComboBox CreateStandardComboBox(ComboBoxStyle style = ComboBoxStyle.DropDownList)
        {
            return new ComboBox
            {
                Font = RegularFont,
                Dock = DockStyle.Fill,
                DropDownStyle = style,
                Margin = ControlMargin
            };
        }
        
        // Create standard list view
        public static ListView CreateStandardListView()
        {
            return new ListView
            {
                Font = RegularFont,
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BorderStyle = BorderStyle.FixedSingle,
                MultiSelect = false
            };
        }
        
        // Create standard group box
        public static GroupBox CreateStandardGroupBox(string title)
        {
            return new GroupBox
            {
                Text = title,
                Font = GroupBoxFont,
                Dock = DockStyle.Fill,
                Padding = StandardPadding,
                ForeColor = TextColor
            };
        }
        
        // Create standard date time picker
        public static DateTimePicker CreateStandardDateTimePicker()
        {
            return new DateTimePicker
            {
                Font = RegularFont,
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill,
                Margin = ControlMargin
            };
        }
        
        // Create a card panel with shadow effect
        public static Panel CreateCardPanel(int width, int height)
        {
            Panel panel = new Panel
            {
                Width = width,
                Height = height,
                BackColor = CardBackgroundColor
            };
            
            // Add shadow effect
            panel.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid);
            };
            
            return panel;
        }
        
        // Create a stats card panel
        public static Panel CreateStatsCardPanel(string title, string value, Color accentColor, int width = 200, int height = 120)
        {
            Panel card = CreateCardPanel(width, height);
            
            // Add a border with accent color at the top
            Panel accent = new Panel
            {
                Height = 5,
                Width = width,
                BackColor = accentColor,
                Location = new Point(0, 0)
            };
            card.Controls.Add(accent);
            
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
    }
}
