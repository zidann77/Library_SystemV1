using BusinessLogicLayer;
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

namespace Library.Users.Controls
{
    public partial class ctrlUserInfo : UserControl
    {
        clsUser user ;
        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {

        }
        public void LoadData(int ID)
        {
            user = clsUser.FindByUserID(ID);
            lbuserID.Text = user.UserID.ToString();
            lbUserName.Text = user.UserName.ToString();
            lbRole.Text=clsRole.Find(user.CurrentRoleID).RoleName;

        }

        private void btnPersonInfo_Click(object sender, EventArgs e)
        {
            frmPersonInfo frm = new frmPersonInfo(user.PersonID);
            frm.ShowDialog();
        }
    }
}
