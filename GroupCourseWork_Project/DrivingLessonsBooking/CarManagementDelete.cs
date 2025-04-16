using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrivingLessonsBooking
{
    // A custom confirmation dialog for car delete operations
    public class ConfirmDeleteCarDialog : Form
    {
        public ConfirmDeleteCarDialog(string carName, string carDetails)
        {
            InitializeComponent(carName, carDetails);
        }

        private void InitializeComponent(string carName, string carDetails)
        {
            // Form setup
            this.Text = "Confirm Car Deletion";
            this.Size = new Size(450, 280);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // Panel for the warning icon and text
            Panel warningPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(255, 240, 240)
            };

            // Warning icon (using a label with a warning symbol)
            Label warningIcon = new Label
            {
                Text = "⚠",
                Font = new Font("Segoe UI", 24, FontStyle.Regular),
                ForeColor = Color.FromArgb(192, 0, 0),
                Size = new Size(50, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 15)
            };
            warningPanel.Controls.Add(warningIcon);

            // Warning title
            Label warningTitle = new Label
            {
                Text = "Are you sure you want to delete this car?",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(192, 0, 0),
                AutoSize = true,
                Location = new Point(80, 15)
            };
            warningPanel.Controls.Add(warningTitle);

            // Warning subtitle
            Label warningSubtitle = new Label
            {
                Text = "This action cannot be undone.",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(80, 45)
            };
            warningPanel.Controls.Add(warningSubtitle);

            // Details panel
            Panel detailsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Car name label
            Label lblCarName = new Label
            {
                Text = carName,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 57, 111),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            detailsPanel.Controls.Add(lblCarName);

            // Car details in a text box for better formatting
            TextBox txtDetails = new TextBox
            {
                Text = carDetails,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(60, 60, 60),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Location = new Point(20, 50),
                Size = new Size(380, 60)
            };
            detailsPanel.Controls.Add(txtDetails);

            // Button panel
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(245, 245, 250)
            };

            // Cancel button
            Button btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.No,
                Width = 100,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                Location = new Point(230, 10)
            };
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            buttonPanel.Controls.Add(btnCancel);

            // Delete button
            Button btnDelete = new Button
            {
                Text = "Delete",
                DialogResult = DialogResult.Yes,
                Width = 100,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(192, 0, 0),
                ForeColor = Color.White,
                Location = new Point(340, 10)
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            buttonPanel.Controls.Add(btnDelete);

            this.Controls.Add(detailsPanel);
            this.Controls.Add(warningPanel);
            this.Controls.Add(buttonPanel);

            this.AcceptButton = btnDelete;
            this.CancelButton = btnCancel;
        }
    }
}
