using BusinessLogicLayer;
using Library.Global_Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Library.Fines
{
    public partial class frmFinesList : Form
    {
        DataTable dt;
        string CurrentFilter = "";

        public frmFinesList()
        {
            InitializeComponent();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        void refreshData()
        {
            dt = clsFine.GetAllFines();
            guna2DataGridView1.DataSource = dt;
        }

        private void frmFinesList_Load(object sender, EventArgs e)
        {
            FilterChoicesBox.SelectedIndex = 0;
            PaymentsWayBox.Visible = false;

            refreshData();
            PaintTable();
        }

        private void FilterChoicesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentFilter = FilterChoicesBox.Text;

            guna2TextBox1.Text = "";
            PaymentsWayBox.Visible = false;
            guna2TextBox1.Visible = true;

            if (CurrentFilter == "PaymentWay")
            {
                guna2TextBox1.Visible = false;
                PaymentsWayBox.Visible = true;
                PaymentsWayBox.SelectedIndex = 0;
            }
        }

        private void PaymentsWayBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dt == null) return;

            DataView dv = dt.DefaultView;

            if (PaymentsWayBox.SelectedIndex == 0)       // All
                dv.RowFilter = "";
            else if (PaymentsWayBox.SelectedIndex == 1)  // Cash
                dv.RowFilter = "PaymentWay = 1";
            else if (PaymentsWayBox.SelectedIndex == 2)  // Visa
                dv.RowFilter = "PaymentWay = 0";
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (dt == null) return;

            string value = guna2TextBox1.Text.Trim();
            DataView dv = dt.DefaultView;

            if (string.IsNullOrEmpty(value))
            {
                dv.RowFilter = "";
                return;
            }

            dv.RowFilter = $"{CurrentFilter} = '{value}'";
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Util.ExportDataTableToExcel(dt);
        }
    }
}
