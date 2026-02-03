using BusinessLogicLayer;
using Library.Books;
using Library.Borrowings.Forms;
using Library.Dashboard.Circulation;
using Library.Dashboard.Fines;
using Library.Fines;
using Library.Global_Classes;
using Library.LogIn;
using Library.Message;
using Library.People;
using Library.Properties;
using Library.Reservations;
using Library.Users.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Library
{
    public partial class frmMain : Form
    {
        frmLoginForm LoginForm = new frmLoginForm();
        public frmMain(frmLoginForm frm)
        {
            InitializeComponent();
            LoginForm = frm;
            SetTabColors();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetTabColors()
        {
            // Idle (الحالة العادية)
            guna2TabControl1.TabButtonIdleState.FillColor = Color.SeaGreen;
            guna2TabControl1.TabButtonIdleState.ForeColor = Color.White;

            // Hover (أخف من SeaGreen)
            guna2TabControl1.TabButtonHoverState.FillColor = Color.MediumSeaGreen;
            guna2TabControl1.TabButtonHoverState.ForeColor = Color.White;

            // Selected (أفتح قليلاً لتوضيح الاختيار)
            guna2TabControl1.TabButtonSelectedState.FillColor = Color.LightSeaGreen;
            guna2TabControl1.TabButtonSelectedState.ForeColor = Color.White;

            // إزالة الحدود
            guna2TabControl1.TabButtonIdleState.BorderColor = Color.Transparent;
            guna2TabControl1.TabButtonHoverState.BorderColor = Color.Transparent;
            guna2TabControl1.TabButtonSelectedState.BorderColor = Color.Transparent;
        }





        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTimer.Text = DateTime.Now.ToString("hh:mm tt");
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2TabControl1_Click(object sender, EventArgs e)
        {
          
        }

        private void tbSignOut_Click(object sender, EventArgs e)
        {
          //  this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            clsGlobal.CurrentUser= null;
            LoginForm.Show();
        }

        private void tpMain_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            lbUserName.Text = clsGlobal.CurrentUser.UserName;

            if (!string.IsNullOrEmpty(clsGlobal.CurrentUser.PersonInfo.ImageURL))
            {
                pbuserImg.ImageLocation = clsGlobal.CurrentUser.PersonInfo.ImageURL;
            }
            else
            {
                pbuserImg.Image = clsGlobal.CurrentUser.PersonInfo.IsMale
                    ? Resources.man
                    : Resources.woman;
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmBooksList frm = new frmBooksList();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddUpdateBook frm = new AddUpdateBook();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmUserSettings frm = new frmUserSettings(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmBorrowingsList frm = new frmBorrowingsList();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage("Choose the Book You Want Borrow ", frmMessage.enMode.Info);
            frm.ShowDialog();
            frmBooksList frmBooksList = new frmBooksList();
            frmBooksList.ShowDialog();
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            frmReservationsList frm = new frmReservationsList();
            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage("Choose the Book You Want Reserve ", frmMessage.enMode.Info);
            frm.ShowDialog();
            frmBooksList frmBooksList = new frmBooksList();
            frmBooksList.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frmCirculation frmCirculation = new frmCirculation();   
            frmCirculation.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frmFineStatistics frm = new frmFineStatistics();
            frm.ShowDialog();
        }

        private void tpUsers_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            frmFinesList frm = new frmFinesList();
            frm.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {

            frmMessage frmm = new frmMessage("End Your Borrowing in Borrowing List"
                , frmMessage.enMode.Info);
            frmm.ShowDialog();
            frmBorrowingsList frm = new frmBorrowingsList();
            frm.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmUsersList frm = new frmUsersList();
            frm.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frmUserSettings frm = new frmUserSettings();
            frm.ShowDialog();
        }

        private void pbuserImg_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }
    }
}
