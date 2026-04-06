using Library.AI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Books
{
    public partial class frmAskAIBook : Form
    {
        string Info;
        public frmAskAIBook(string info)
        {
            InitializeComponent();
            Info = info;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();    
        }

        public async void AskAI(string Info)
        {
            AIService ai = new AIService();
            AnswerBox.Text = "Thinking...";
            string result = await ai.GetResponseAsync("how do you see this book , i need your opinion for " + Info);
            AnswerBox.Text = result;

        }

        private void frmAskAIBook_Load(object sender, EventArgs e)
        {
            AskAI(Info);
        }
    }
}
