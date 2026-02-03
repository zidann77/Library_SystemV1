using BusinessLogicLayer;
using Library.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Users.Forms
{
    public partial class frmUserInfo : Form
    {
        clsUser user;
        public frmUserInfo(int ID)
        {
            InitializeComponent();
            user = clsUser.FindByUserID(ID);
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            if (user != null)
            {

                ctrlUserInfo1.LoadData(user.UserID);
            }
            else
            {
                frmMessage frm = new frmMessage("No User Found", frmMessage.enMode.Error);
                frm.ShowDialog();
            }
        }

        private void ctrlUserInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
