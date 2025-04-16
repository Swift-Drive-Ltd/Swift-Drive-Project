using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrivingLessonsBooking
{
    public enum MessageType
    {
        Success,
        Warning,
        Error,
        Information
    }

    // A custom message dialog with improved styling
    public class CustomMessageDialog : Form
    {
        public CustomMessageDialog(string title, string message, MessageType messageType)
        {
            InitializeComponent(title, message, messageType);
        }

        private void InitializeComponent(string title, string message, MessageType messageType)
        {
            // Form setup
            this.Text = title;
            this.Size = new Size(420, 220);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // Define colors based on message type
            Color headerColor = messageType switch
            {
                MessageType.Success => Color.FromArgb(223, 240, 216),      // Light green
                MessageType.Warning => Color.FromArgb(252, 248, 227),      // Light yellow
                MessageType.Error => Color.FromArgb(242, 222, 222),        // Light red
                MessageType.Information => Color.FromArgb(217, 237, 247),  // Light blue
                _ => Color.FromArgb(217, 237, 247)
            };

            Color iconColor = messageType switch
            {
                MessageType.Success => Color.FromArgb(60, 118, 61),      // Dark green
                MessageType.Warning => Color.FromArgb(138, 109, 59),     // Dark yellow
                MessageType.Error => Color.FromArgb(169, 68, 66),        // Dark red
                MessageType.Information => Color.FromArgb(49, 112, 143), // Dark blue
                _ => Color.FromArgb(49, 112, 143)
            };

            string icon = messageType switch
            {
                MessageType.Success => "✓",    // Checkmark
                MessageType.Warning => "⚠",    // Warning
                MessageType.Error => "✕",      // X mark
                MessageType.Information => "ℹ", // Info
                _ => "ℹ"
            };

            // Header panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = headerColor
            };

            // Icon label
            Label iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 24, FontStyle.Regular),
                ForeColor = iconColor,
                Size = new Size(50, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(15, 5)
            };
            headerPanel.Controls.Add(iconLabel);

            // Title label
            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = iconColor,
                AutoSize = true,
                Location = new Point(70, 18)
            };
            headerPanel.Controls.Add(titleLabel);

            // Message panel
            Panel messagePanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Message text
            TextBox txtMessage = new TextBox
            {
                Text = message,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(60, 60, 60),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };
            messagePanel.Controls.Add(txtMessage);

            // Button panel
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(245, 245, 250)
            };

            // OK button
            Button btnOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Width = 100,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(37, 57, 111),
                ForeColor = Color.White,
                Location = new Point((420 - 100) / 2, 12) // Center horizontally
            };
            btnOk.FlatAppearance.BorderSize = 0;
            
            // Add hover effects
            btnOk.MouseEnter += (s, e) => {
                btnOk.BackColor = Color.FromArgb(57, 77, 131); // Lighter blue
            };
            btnOk.MouseLeave += (s, e) => {
                btnOk.BackColor = Color.FromArgb(37, 57, 111); // Back to original blue
            };
            
            buttonPanel.Controls.Add(btnOk);

            this.Controls.Add(messagePanel);
            this.Controls.Add(headerPanel);
            this.Controls.Add(buttonPanel);

            this.AcceptButton = btnOk;
        }
    }
}
