using BusinessLogicLayer;
using Library.Global_Classes;
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
    public partial class frmEditUserPersmission : Form
    {
        clsUser user;
        Dictionary<int, (string name, string Description)> Roles;
        public frmEditUserPersmission(int id)
        {
            InitializeComponent();
            user = clsUser.FindByUserID(id);
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        void FillRolesBpx()
        {
            Roles = clsRole.GetAllRolesAsDictionary();
            foreach (var item in Roles)
            {
                RolesBox.Items.Add(item.Value.name);
            }
        }

        private void frmEditUserPersmission_Load(object sender, EventArgs e)
        {
            if(user != null)
            {
                lbDate.Text = user.LastState.ToString();
                lbRole.Text = clsRole.Find(user.CurrentRoleID).RoleName.ToString();

                 FillRolesBpx();
          
               
            }
            else
            {
                frmMessage frm = new frmMessage("No User Founded ", frmMessage.enMode.Error);
                frm.ShowDialog();
                btnClose.PerformClick();
            }

        }

        private void btnPersonInfo_Click(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage("Are You Sure ", frmMessage.enMode.Question);
            frm.ShowDialog();

            if (frm.Result == frmMessage.enResult.Ok)
            {
                var role = Roles.FirstOrDefault(x => x.Value.name == RolesBox.Text);

                user.LastRole = user.CurrentRoleID;
                user.CurrentRoleID = role.Key;
                user.LastState = DateTime.Now;
                if (user.Save())
                {

                    frmMessage frm1 = new frmMessage("Data Saved Succefully ", frmMessage.enMode.Question);
                    frm1.ShowDialog();
                    btnClose.PerformClick();
                   
                }
                else
                {
                    frmMessage frm1 = new frmMessage("Change Role Failed", frmMessage.enMode.Error);
                    frm1.ShowDialog();
                    btnClose.PerformClick();
                }
            }
        }
    }
}
