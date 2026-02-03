using Library.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Message
{
    public partial class frmMessage : Form
    {
        string Message = "";
        public frmMessage(string message, enMode mode = enMode.Error)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // نلغي البوردر الافتراضي
            this.Padding = new Padding(3); // سماكة البوردر
            Message = message;
            Mode = mode;
        }

        //public frmMessage()
        //{
        //    InitializeComponent();
        //    this.FormBorderStyle = FormBorderStyle.None; // نلغي البوردر الافتراضي
        //    this.Padding = new Padding(3); // سماكة البوردر
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Color borderColor = Color.DarkGreen; // اللون المناسب مع الأخضر + البيج
            int borderWidth = 4;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }


        public enum enMode { Error = 1, Info = 2, Question = 3 , PaymentDone = 4 , PaymentFailed = 5 }

        public enum enResult { Ok = 0, Cancel = 1, none = 2 }


        public enResult _Result = enResult.none;
        public enMode _Mode = enMode.Error;

        public enResult Result
        {
            get { return _Result; }
            set { _Result = value; }
        }



        public enMode Mode
        {
            set
            {
                _Mode = value;

                switch (_Mode)
                {
                    case enMode.Question:
                        lbtitle.Text = "Messsage";
                        pbImage.BackgroundImage = Resources.question_mark;
                        break;

                    case enMode.Error:
                        lbtitle.Text = "Error";
                        pbImage.BackgroundImage = Resources.error;
                        break;

                    case enMode.Info:
                        lbtitle.Text = "Info";
                        pbImage.BackgroundImage = Resources.warning;
                        break;

                    case enMode.PaymentFailed:
                        lbtitle.Text = "Payment Failed";
                        pbImage.BackgroundImage = Resources.error__1_;
                        break;

                    case enMode.PaymentDone:
                        lbtitle.Text = "Payment successful";
                        pbImage.BackgroundImage = Resources.pay__1_;
                        break;


                }
            }
            get { return _Mode; }
        }

        public bool EnableCloseButtonVisibale
        {
            get { return btnClose.Visible; }
            set { btnClose.Visible = value; }
        }

        public bool EnableCancelButtonVisibale
        {
            get { return btnCancel.Visible; }
            set { btnCancel.Visible = value; }
        }






        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbtitle_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Result = enResult.Cancel;
            btnClose.PerformClick();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Result = enResult.Ok;
            btnClose.PerformClick();
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            label1.Text = Message;

            System.Media.SystemSounds.Exclamation.Play();

        }
    }
}
