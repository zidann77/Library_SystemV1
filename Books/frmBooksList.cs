using BusinessLogicLayer;
using Library.BookCopies;
using Library.Books.controls;
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
using static Guna.UI2.Native.WinApi;

namespace Library.Books
{
    public partial class frmBooksList : Form
    {
        DataTable dt;
        string CurrentFilter;

        public frmBooksList()
        {
            InitializeComponent();
        }

        void Reloaddata()
        {
            dt = clsBook.GetAllBooks();
            guna2DataGridView1.DataSource = dt;
        }

        void setupDataGridView()
        {
            guna2DataGridView1.DataSource = dt;

            // rename headers
            guna2DataGridView1.Columns["BookID"].HeaderText = "ID";
            guna2DataGridView1.Columns["Title"].HeaderText = "Title";
            guna2DataGridView1.Columns["ISBN"].HeaderText = "ISBN";
            guna2DataGridView1.Columns["PublicationDate"].HeaderText = "Publication Date";
            guna2DataGridView1.Columns["Rate"].HeaderText = "Rating";
          

            // adjust widths
            guna2DataGridView1.Columns["BookID"].Width = 50;
            guna2DataGridView1.Columns["Title"].Width = 350;
            guna2DataGridView1.Columns["ISBN"].Width = 150;
            guna2DataGridView1.Columns["PublicationDate"].Width = 200;
            guna2DataGridView1.Columns["Rate"].Width = 60;
          

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

            // Format publication date
            guna2DataGridView1.Columns["PublicationDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            // Format rating column
            guna2DataGridView1.Columns["Rate"].DefaultCellStyle.Format = "0.0";
            guna2DataGridView1.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        void SetupComboBox()
        {
            // إضافة خيارات الفلترة فقط لـ ISBN و Title
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("ISBN");
            guna2ComboBox1.Items.Add("Title");

            // تعيين القيمة الافتراضية
            guna2ComboBox1.SelectedIndex = 0;
        }

        private void frmBooksList_Load(object sender, EventArgs e)
        {
            SetupComboBox(); // تهيئة ComboBox
            Reloaddata();
            setupDataGridView();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentFilter = guna2ComboBox1.Text.ToString();

            // تحديث نص placeholder بناءً على الفلتر المختار
            UpdateSearchPlaceholder();
        }

        private void UpdateSearchPlaceholder()
        {
            switch (CurrentFilter)
            {
                case "ISBN":
                    guna2TextBox1.PlaceholderText = "Search by ISBN...";
                    break;
                case "Title":
                    guna2TextBox1.PlaceholderText = "Search by Title...";
                    break;
                default:
                    guna2TextBox1.PlaceholderText = "Search...";
                    break;
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            //string allowed = "0123456789-";
            //string text = guna2TextBox1.Text;

            //// تنظيف أي حرف غير مسموح
            //string cleaned = new string(text.Where(c => allowed.Contains(c)).ToArray());

            //if (cleaned != text)
            //{
            //  guna2TextBox1.Text = cleaned;
            // guna2TextBox1 .SelectionStart = cleaned.Length; // يرجّع الكيرسر لنهاية النص
            //}

            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                dt.DefaultView.RowFilter = null;
                return;
            }

            string searchText = guna2TextBox1.Text.Trim();

            switch (CurrentFilter)
            {
                case "ISBN":
                   
                    dt.DefaultView.RowFilter = $"[ISBN] LIKE '%{searchText}%'";
                    break;

                case "Title":
                    
                    dt.DefaultView.RowFilter = $"[Title] LIKE '%{searchText}%'";
                    break;

                default:
                    dt.DefaultView.RowFilter = null;
                    break;
            }
        }

       

        private void _DisableFormWhileRefresh()
        {
            this.Enabled = false;
            Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;
        }

        private void _EnableFormAfterRefresh()
        {
            this.Enabled = true;
            Application.DoEvents();
            Cursor.Current = Cursors.Default;
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DisableFormWhileRefresh();
            Reloaddata();
            _EnableFormAfterRefresh();
        }

        private void showBookInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                int bookID = (int)guna2DataGridView1.CurrentRow.Cells["BookID"].Value;
                // frmBookInfo frm = new frmBookInfo(bookID);
                // frm.ShowDialog();
                MessageBox.Show($"Show info for book ID: {bookID}", "Book Info");
            }
        }

        private void editBookInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                int bookID = (int)guna2DataGridView1.CurrentRow.Cells["BookID"].Value;
                // frmAddUpdateBook frm = new frmAddUpdateBook(bookID);
                // frm.ShowDialog();
                MessageBox.Show($"Edit book ID: {bookID}", "Edit Book");
                Reloaddata();
            }
        }

        private void btnAddNewBook_Click(object sender, EventArgs e)
        {
            // frmAddUpdateBook frm = new frmAddUpdateBook();
            // frm.ShowDialog();
            MessageBox.Show("Open Add New Book form", "Add Book");
            Reloaddata();
        }

        private void deleteBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                int bookID = (int)guna2DataGridView1.CurrentRow.Cells["BookID"].Value;
                string bookTitle = guna2DataGridView1.CurrentRow.Cells["Title"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete '{bookTitle}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (clsBook.Delete(bookID))
                    {
                        MessageBox.Show("Book deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reloaddata();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete book!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void viewImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                int bookID = (int)guna2DataGridView1.CurrentRow.Cells["BookID"].Value;
                // frmBookImages frm = new frmBookImages(bookID);
                // frm.ShowDialog();
                MessageBox.Show($"Show images for book ID: {bookID}", "Book Images");
            }
        }

        private void btnAddnewBook_Click_1(object sender, EventArgs e)
        {
            AddUpdateBook addUpdateBook = new AddUpdateBook();
            addUpdateBook.ShowDialog();
            Reloaddata();
        }

        private void reloadToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Reloaddata();
        }

        private void showBookInfoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int BookID = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;

            frmBookInfo frm = new frmBookInfo(BookID);
            frm.ShowDialog();   

        }

        private void guna2TextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (CurrentFilter == "Title")
                return;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void updateInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Bookid = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;
            AddUpdateBook updateBook = new AddUpdateBook(Bookid);
            updateBook.ShowDialog();
            Refresh();
        }

        private void addCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int Bookid = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;
            //frmManageBookCopies frm = new frmManageBookCopies(Bookid);
            //frm.ShowDialog();
        }

        private void copiesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Bookid = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;
            frmManageBookCopies frm = new frmManageBookCopies(Bookid);
            frm.ShowDialog();
        }

        private void addCopyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Bookid = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;
            frmAddCopy frm = new frmAddCopy(Bookid);
            frm.ShowDialog();
        }


        private void borrowBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Bookid = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;
            frmBorrowBook frm = new frmBorrowBook(Bookid);
            frm.ShowDialog();
        }

        private void reserveBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Bookid = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;
            frmReserveBook frm = new frmReserveBook(Bookid);
            frm.ShowDialog();
        }
    }
}
