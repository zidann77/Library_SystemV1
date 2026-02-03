using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicLayer;
using Library.BookCopies;
using Library.Books;
using Library.Message;
using Library.People;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Library.Reservations
{
    public partial class frmReservationsList : Form
    {
        DataTable Table;
        string CurrentFilter = "";

        public frmReservationsList()
        {
            InitializeComponent();
        }

        // ================= TABLE STYLE =================
        void PaintTable()
        {


            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            guna2DataGridView1.ReadOnly = true;
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;


            // color theme
            guna2DataGridView1.EnableHeadersVisualStyles = false;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.MediumSeaGreen;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            guna2DataGridView1.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
            guna2DataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            //*****************

            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.MediumSeaGreen;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Rows background / text
            guna2DataGridView1.DefaultCellStyle.BackColor = Color.White;
            guna2DataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Honeydew; // soft green tint

            // Selection highlight (to match the theme)
            guna2DataGridView1.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            guna2DataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // Grid & borders
            guna2DataGridView1.GridColor = Color.MediumSeaGreen;
            guna2DataGridView1.BorderStyle = BorderStyle.None;
            guna2DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Row headers off (optional for clean look)
            guna2DataGridView1.RowHeadersVisible = false;

            // Rounded edges & smooth scroll (Guna2 bonus)
            guna2DataGridView1.ColumnHeadersHeight = 35;
            guna2DataGridView1.RowTemplate.Height = 30;
            guna2DataGridView1.BackgroundColor = Color.White;

        }

        void RefreshData()
        {
            Table = clsReservation.GetAllReservations();
            guna2DataGridView1.DataSource = Table;
        }

        // ================= FILTER CHOICE =================
        private void FilterChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentFilter = FilterChoices.Text;

            if (CurrentFilter == "Status")
            {
                guna2TextBox1.Visible = false;
                StatusBox.Visible = true;
                StatusBox.SelectedIndex = 0;
            }
            else
            {
                guna2TextBox1.Visible = true;
                StatusBox.Visible = false;
                guna2TextBox1.Text = "";
                guna2TextBox1.Focus();
            }
        }

        // ================= STATUS FILTER =================
        private void StatusBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Table == null) return;

            DataView dv = Table.DefaultView;

            if (StatusBox.SelectedIndex == 0)      // All
                dv.RowFilter = "";
            else if (StatusBox.SelectedIndex == 1) // New
                dv.RowFilter = "ReservationStatus = 'New'";
            else if (StatusBox.SelectedIndex == 2) // Cancelled
                dv.RowFilter = "ReservationStatus ='Cancelled'";
            else if (StatusBox.SelectedIndex == 3) // Complete
                dv.RowFilter = "ReservationStatus = 'Complete'";
        }

        // ================= TEXT FILTER =================
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Table == null) return;

            string value = guna2TextBox1.Text.Trim();
            DataView dv = Table.DefaultView;

            if (string.IsNullOrEmpty(value))
            {
                dv.RowFilter = "";
                return;
            }

            dv.RowFilter = $"{CurrentFilter} = {value}";
        }

        // ================= ONLY NUMBERS =================
        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }


        // ================= UI ACTIONS =================
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddnewReservation_Click(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage(
                "Choose the Book You Want Reserve",
                frmMessage.enMode.Info);

            frm.ShowDialog();

            frmBooksList booksList = new frmBooksList();
            booksList.ShowDialog();

            RefreshData();
        }

        // ================= CONTEXT MENU =================
        private void borrowerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID =
                Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[1].Value);

            frmPersonInfo frm = new frmPersonInfo(personID);
            frm.ShowDialog();
        }

        private void bToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int reservationID =
                Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);

            frmReservedCopy frm = new frmReservedCopy(reservationID);
            frm.ShowDialog();
        }

        private void endBorrowingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id =
                Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);

            frmMessage confirm =
                new frmMessage($"Cancel reservation ID = {id} ?",
                frmMessage.enMode.Error);

            confirm.ShowDialog();

            if (confirm.Result != frmMessage.enResult.Ok)
                return;

            clsReservation reservation = clsReservation.Find(id);

            reservation.ReservationStatus =
                clsReservation.enReservationStatus.Cancelled;

            reservation.CopyInfo.Reserved = false;

            reservation.CopyInfo.Save();
            reservation.Save();

            frmMessage done =
                new frmMessage("Reservation cancelled successfully",
                frmMessage.enMode.Info);

            done.ShowDialog();

            RefreshData();
        }

        private void frmReservationsList_Load_1(object sender, EventArgs e)
        {
            FilterChoices.SelectedIndex = 0;
            StatusBox.Visible = false;

            RefreshData();
            PaintTable();
        }

     
    }
}
