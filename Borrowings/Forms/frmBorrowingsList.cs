using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicLayer;
using Library.Books;
using Library.Fines;
using Library.Message;
using Library.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Library.Fines.frmPerformFine;

namespace Library.Borrowings.Forms
{
    public partial class frmBorrowingsList : Form
    {
        DataTable BorrowingsTable;

        public frmBorrowingsList()
        {
            InitializeComponent();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        void PaintTable()
        {

            guna2DataGridView1.Columns["ID"].HeaderText = "Borrowing ID";
            guna2DataGridView1.Columns["ID"].Width = 60;

            guna2DataGridView1.Columns["BorrowingDate"].HeaderText = "Borrowing Date";
            guna2DataGridView1.Columns["BorrowingDate"].Width = 120;

            guna2DataGridView1.Columns["PersonID"].HeaderText = "Person ID";
            guna2DataGridView1.Columns["PersonID"].Width = 60;

            guna2DataGridView1.Columns["CopyID"].HeaderText = "Copy ID";
            guna2DataGridView1.Columns["CopyID"].Width = 60;

            guna2DataGridView1.Columns["FineID"].HeaderText = "Fine ID";
            guna2DataGridView1.Columns["FineID"].Width = 60;

            guna2DataGridView1.Columns["Duration"].HeaderText = "End Date";
            guna2DataGridView1.Columns["Duration"].Width = 120;

          
            guna2DataGridView1.Columns["ActualReturnDate"].HeaderText = "Return Date";
            guna2DataGridView1.Columns["ActualReturnDate"].Width = 120;

            guna2DataGridView1.Columns["ByUser"].HeaderText = "User";
            guna2DataGridView1.Columns["ByUser"].Width = 60;

            guna2DataGridView1.Columns["Detailes"].HeaderText = "Details";
            guna2DataGridView1.Columns["Detailes"].Width = 200;

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
            BorrowingsTable = clsBorrowing.GetAllBorrowings();
            guna2DataGridView1.DataSource = BorrowingsTable;
        }

        string CurrentFillter = "";

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentFillter = FilterChoicesBox.Text;

         
            if (CurrentFillter == "Active")
            {
                guna2TextBox1.Visible = false;
                ActiveBox.Visible = true;
                ActiveBox.SelectedIndex = 0;
            }
            else
            {
                guna2TextBox1.Visible = true;
                ActiveBox.Visible = false;
                guna2TextBox1.Text = "";
            }
        }


        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BorrowingsTable == null) return;

            DataView dv = BorrowingsTable.DefaultView;

            if (ActiveBox.SelectedIndex == 0) // All
                dv.RowFilter = "";

            else if (ActiveBox.SelectedIndex == 1) // Active
                dv.RowFilter = "ActualReturnDate IS NULL";

            else if (ActiveBox.SelectedIndex == 2) // Inactive
                dv.RowFilter = "ActualReturnDate IS NOT NULL";
        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (BorrowingsTable == null) return;

            string value = guna2TextBox1.Text.Trim();
            DataView dv = BorrowingsTable.DefaultView;

            if (string.IsNullOrEmpty(value))
            {
                dv.RowFilter = "";
                return;
            }
            else
            {
              
                dv.RowFilter = $"{CurrentFillter} = '{value}'";
            }
        }


        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;

        }


        private void btnAddnewBorrowing_Click(object sender, EventArgs e)
        {
           
            frmMessage frm = new frmMessage("Choose the Book You Want Borrow ", frmMessage.enMode.Info);
            frm.ShowDialog();

            frmBooksList frmBooksList = new frmBooksList();
            frmBooksList.ShowDialog();

            RefreshData();
        }


        private void frmBorrowingsList_Load(object sender, EventArgs e)
        {
            FilterChoicesBox.SelectedIndex = 0;
            ActiveBox.Visible = false;

            RefreshData();
            PaintTable();

        }

        private void bToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int BorrowingID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);

          frmBorrowedCopy frm = new frmBorrowedCopy(BorrowingID);
            frm.ShowDialog();
        }

        private void borrowerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[2].Value);

            frmPersonInfo frm = new frmPersonInfo(PersonID); 
            frm.ShowDialog();
        }

       
        private void FineForm_FineCompleted(object sender, PerformFineResultEventArgs e)
        {
            if (e.IsSuccess)
                Ispaid = true;
              
            
            else
              Ispaid=false;
            
        }

        bool Ispaid = false;
        private void endBorrowingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int BorrowingID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);
            clsBorrowing borrowing = clsBorrowing.Find(BorrowingID);

           
            if (borrowing != null)
            {
                if(borrowing.ActualReturnDate != null)
                return;

                  Ispaid = borrowing.FineID != null ? true : false;


                if (borrowing.EndDate < DateTime.Now )
                {
                    int Diff = (DateTime.Now - borrowing.EndDate).Days;
                    if (Diff > 0 && !Ispaid)
                    {
                        frmMessage frm2 = new frmMessage("There is A Late Fine", frmMessage.enMode.Info);
                        frm2.ShowDialog();

                        if (borrowing.FineID == null)
                        {

                            frmPerformFine frm1 = new frmPerformFine(borrowing.ID);
                            frm1.FineCompleted += FineForm_FineCompleted;
                            frm1.ShowDialog();

                            if (!Ispaid) return;
                        }
                    }
                }

                RefreshData();

                frmMessage frm = new frmMessage("Are You Sure", frmMessage.enMode.Info);
                frm.ShowDialog();

               borrowing.ActualReturnDate = DateTime.Now;
                borrowing.CopyInfo.AvailabilityStatus = true;
                 
                borrowing.CopyInfo.Save();
                borrowing.Save();

                if (borrowing.IsReturned && borrowing.CopyInfo.IsAvailable)
                {

                    frmMessage frmm = new frmMessage("Borrowing End Successfully", frmMessage.enMode.Info);
                    frmm.ShowDialog();

                }
                else
                {
                    frmMessage frmm = new frmMessage("Borrowing End Failed", frmMessage.enMode.Info);
                    frmm.ShowDialog();
                }

                RefreshData();

            }
        

          
        }

        private void guna2ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int BorrowingID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);
            clsBorrowing borrowing = clsBorrowing.Find(BorrowingID);

            if (borrowing.IsReturned)
                endBorrowingToolStripMenuItem.Enabled = false;
        }
    }
}
