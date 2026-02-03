using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicLayer;
using Guna.UI2.WinForms;
using Library.BookCopies.CopiesStatus;
using Library.Borrowings.Forms;
using Library.Message;
using Library.Reservations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.BookCopies
{
    public partial class frmManageBookCopies : Form
    {
        int BookId = -1;

        DataTable CopiesTable;

        //public frmManageBookCopies()
        //{
        //    InitializeComponent();
        //}

        public frmManageBookCopies(int BookID)
        {
            InitializeComponent();
            BookId = BookID;

            //lbfilter.Visible = false;
            //guna2ComboBox1.Visible = false;
            //guna2TextBox1.Visible = false;

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void PaintTable()
        {
            if (CopiesTable.Rows.Count > 0)
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

                //guna2DataGridView1.Columns["BookID"].HeaderText = "Book ID";
                //guna2DataGridView1.Columns["BookID"].Width = 80;

                //guna2DataGridView1.Columns["CopyID"].HeaderText = "Copy ID";
                //guna2DataGridView1.Columns["CopyID"].Width = 80;

                //guna2DataGridView1.Columns["Title"].HeaderText = "Title";
                //guna2DataGridView1.Columns["Title"].Width = 220;

                //guna2DataGridView1.Columns["ISBN"].HeaderText = "ISBN";
                //guna2DataGridView1.Columns["ISBN"].Width = 160;

                //guna2DataGridView1.Columns["AvailabilityStatus"].HeaderText = "Status";
                //guna2DataGridView1.Columns["AvailabilityStatus"].Width = 120;

            }
            else
            {
                this.Close();
                frmMessage frm = new frmMessage("No Data", frmMessage.enMode.Error);
                frm.ShowDialog();
            }
        }
        void RefreshData()
        {
            if (BookId == -1)
            {
                CopiesTable = clsBookCopy.GetAllCopies();
            }
            else
                CopiesTable = clsBook.GetBookCopisByBookID(BookId);


            guna2DataGridView1.DataSource = CopiesTable;
            PaintTable();
            guna2ComboBox1.SelectedIndex = 0;
            lbCount.Text = CopiesTable.Rows.Count.ToString();
        }

        private void frmManageBookCopies_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2TextBox1.PlaceholderText = "Search by Copy ID...";
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (CopiesTable == null)
                return;

            if (int.TryParse(guna2TextBox1.Text, out int id))
            {
                CopiesTable.DefaultView.RowFilter = $"[CopyID] = {id}"; // int
            }
            else
            {
                CopiesTable.DefaultView.RowFilter = ""; 
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage("Are You Sure ?", frmMessage.enMode.Question);
            frm.ShowDialog();
            if (frm.Result == frmMessage.enResult.Ok)
            {
                clsBookCopy copy = clsBookCopy.Find((int)guna2DataGridView1.CurrentRow.Cells[1].Value);
                if (clsBookCopy.Delete(copy.CopyID))
                {
                    frmMessage frm1 = new frmMessage("Copy Deleted Successfully", frmMessage.enMode.Info);
                    frm1.ShowDialog();
                }
                else
                {
                    frmMessage frm1 = new frmMessage("Faild To Delete Copy", frmMessage.enMode.Error);
                    frm1.ShowDialog();
                }

                RefreshData();

            }
        }

        private void updateStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCopystatus frm = new frmCopystatus((int)guna2DataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

            RefreshData();
        }

        private void btnAddnewBook_Click(object sender, EventArgs e)
        {

        }

        private void borrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int copyid = (int)guna2DataGridView1.CurrentRow.Cells[1].Value;
            frmBorrowBook frm = new frmBorrowBook(BookId , copyid);
            frm.ShowDialog();
            RefreshData();
        }

        private void reserveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int copyid = (int)guna2DataGridView1.CurrentRow.Cells[1].Value;
            frmReserveBook frm = new frmReserveBook(BookId, copyid);
            frm.ShowDialog();
            RefreshData();
                
        }

        private void guna2ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if ((bool)guna2DataGridView1.CurrentRow.Cells[4].Value)
            {
                borrowToolStripMenuItem.Enabled = true;
                reserveToolStripMenuItem.Enabled = false;
            }
            else
            {
                borrowToolStripMenuItem.Enabled = false;
                reserveToolStripMenuItem.Enabled = true;
            }
        }
    }
}
