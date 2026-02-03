using BusinessLogicLayer;
using Library.Global_Classes;
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
using System.Web.Security;
using System.Windows.Forms;

namespace Library.Users.Forms
{
    public partial class frmUserSettings : Form
    {
        clsUser user;
        clsPeople person;

        public enum enMode { add, update }
        public enMode mode;

        public frmUserSettings()
        {
            InitializeComponent();
            user = new clsUser();
            person = new clsPeople();
            mode = enMode.add;
        }
        public frmUserSettings(int id)
        {
            InitializeComponent();
            user = clsUser.FindByUserID(id);
            person = clsPeople.Find(user.PersonID);
            mode = enMode.update;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage("Are You Sure ", frmMessage.enMode.Question);
            frm.ShowDialog();
            if (frm.Result == frmMessage.enResult.Ok)
            {
                if (this.ValidateChildren())
                {
                    if (mode == enMode.add && clsUser.IsUserExist(txtUserName.Text))
                    {
                        frmMessage frmm = new frmMessage("User Name Used For Another User", frmMessage.enMode.Info);
                        frmm.ShowDialog();
                        btnClose.PerformClick();
                    }

                    user.PersonID = person.Id;
                    user.PersonInfo = person;
                    user.Password = txtPassword.Text;
                    user.UserName = txtUserName.Text;
                    user.IsActive = cbActive.Checked;
                    user.CurrentRoleID = 2;
                    user.LastState = DateTime.Now;
                    user.LastRole = 2;
                 
                    if (user.Save())
                    {
                        frmMessage frmm = new frmMessage("Data saved Successfully", frmMessage.enMode.Info);
                        frmm.ShowDialog();
                        btnClose.PerformClick();
                    }
                    else
                    {
                        frmMessage frmm = new frmMessage("Failed to Save", frmMessage.enMode.Error);
                        frmm.ShowDialog();
                        btnClose.PerformClick();
                    }

                }
                else
                {

                    frmMessage frmm = new frmMessage("There is No Valid Data", frmMessage.enMode.Error);
                    frmm.ShowDialog();
                    btnClose.PerformClick();

                }
            }
        }

        private void frmUserSettings_Load(object sender, EventArgs e)
        {
            if (mode == enMode.update)
            {
                if (!clsPermissionsRecords.DoesRoleHasPermission(clsGlobal.CurrentUser.CurrentRoleID, (int)clsPermissionsRecords.enPermissionNumber.Update))
                {
                    new frmMessage(" You don't have permission to Update Data.",
                frmMessage.enMode.Error).ShowDialog();
                    btnClose.PerformClick();
                    return;
                }

                if (!clsPermissionsRecords.DoesRoleHasPermission(clsGlobal.CurrentUser.CurrentRoleID, (int)clsPermissionsRecords.enPermissionNumber.ChangePassword))
                {
                    new frmMessage(" You don't have permission to Change Passowrd.",
                frmMessage.enMode.Error).ShowDialog();
                    txtPassword.ReadOnly = true;
                }


                lbtitle.Text = "Edit User";
                btnPerson.Visible = false;
                //  lbperson.Visible = false;   
                lbEditPErson.Visible = true;

              

                if (user != null)
                {
                    txtUserName.Text = user.UserName;
                    txtPassword.Text = user.Password;
                    cbActive.Checked = user.IsActive;
                    //txtPassword.PasswordChar = '*';
                    txtUserName.ReadOnly = true;
                }
                else
                {
                    frmMessage frm = new frmMessage("No User Founded ", frmMessage.enMode.Error);
                    frm.ShowDialog();
                    btnClose.PerformClick();

                }
            }
            else
            {
                lbtitle.Text = "Add User";
                btnPerson.Visible = true;
                //  lbperson.Visible = false;   
                lbEditPErson.Visible = false;
            }


        }

        void GetPersonID(object sender, int ID)
        {
            person = clsPeople.Find(ID);
            if (person == null)
                return;

        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
           frmSearchPerson frm = new frmSearchPerson();
            frm.DataBack += GetPersonID;
            frm.ShowDialog();
        }

        private void lbEditPErson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(user.PersonID);
            frm.ShowDialog();
        }
    }
}
