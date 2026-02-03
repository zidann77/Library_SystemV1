using BusinessLogicLayer;
using Library.Global_Classes;
using Library.People;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Library.Users.Forms
{
    public partial class frmUsersList : Form
    {
        DataTable UsersTable;
        string CurrentFilter = "";

        public frmUsersList()
        {
            InitializeComponent();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ===================== Styling =====================
        void PaintTable()
        {


            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            guna2DataGridView1.ReadOnly = true;
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Theme
            guna2DataGridView1.EnableHeadersVisualStyles = false;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.MediumSeaGreen;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            guna2DataGridView1.DefaultCellStyle.BackColor = Color.White;
            guna2DataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            guna2DataGridView1.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            guna2DataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Honeydew;

            guna2DataGridView1.GridColor = Color.MediumSeaGreen;
            guna2DataGridView1.BorderStyle = BorderStyle.None;
            guna2DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            guna2DataGridView1.RowHeadersVisible = false;

            guna2DataGridView1.ColumnHeadersHeight = 35;
            guna2DataGridView1.RowTemplate.Height = 30;
            guna2DataGridView1.BackgroundColor = Color.White;
        }

        // ===================== Data =====================
        void RefreshData()
        {
            UsersTable = clsUser.GetAllUsers();
            guna2DataGridView1.DataSource = UsersTable;
            FilterChoicesBox.SelectedIndex = 0;
            guna2TextBox1.Clear();
            guna2TextBox1.Focus();
        }

        // ===================== Load =====================
        private void frmUsersList_Load(object sender, EventArgs e)
        {
            FilterChoicesBox.SelectedIndex = 0;
            ActiveBox.Visible = false;

            RefreshData();
            PaintTable();
        }

        // ===================== Filters =====================
        private void FilterChoicesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentFilter = FilterChoicesBox.Text;

            guna2TextBox1.Text = "";
            guna2TextBox1.Visible = true;
            ActiveBox.Visible = false;

            if (CurrentFilter == "IsActive")
            {
                guna2TextBox1.Visible = false;
                ActiveBox.Visible = true;
                ActiveBox.SelectedIndex = 0;
            }
        }

        private void ActiveBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UsersTable == null) return;

            DataView dv = UsersTable.DefaultView;

            if (ActiveBox.SelectedIndex == 0)       // All
                dv.RowFilter = "";
            else if (ActiveBox.SelectedIndex == 1)  // Active
                dv.RowFilter = "IsActive = 1";
            else if (ActiveBox.SelectedIndex == 2)  // Inactive
                dv.RowFilter = "IsActive = 0";
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (UsersTable == null) return;

            string value = guna2TextBox1.Text.Trim();
            DataView dv = UsersTable.DefaultView;

            if (string.IsNullOrEmpty(value))
            {
                dv.RowFilter = "";
                return;
            }

           
            if (CurrentFilter == "UserName" || CurrentFilter == "RoleName")
                dv.RowFilter = $"{CurrentFilter} LIKE '%{value}%'";
            else
                dv.RowFilter = $"{CurrentFilter} = '{value}'";
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

         
            if (CurrentFilter == "UserID" ||
                CurrentFilter == "personID" ||
                CurrentFilter == "CurrentRoleID")
            {
                if (!char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
        }

    

        private void showPersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value);
            frmPersonInfo frm = new frmPersonInfo(ID);
            frm.ShowDialog();
        }

        private void updateRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);
            frmEditUserPersmission frm = new frmEditUserPersmission(ID);
            frm.ShowDialog();
            RefreshData();
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int ID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);
            frmUserSettings frm = new frmUserSettings(ID);
            frm.ShowDialog();
            RefreshData();
        }

        private void btnAddnewBook_Click(object sender, EventArgs e)
        {
            frmUserSettings frm = new frmUserSettings();
            frm.ShowDialog();
            RefreshData();  
        }
    }
}
