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

namespace Library.AI.form
{
    public partial class frmAISuggestionsFrom : Form
    {
        string NotePrompt = "Always be polite and informative in your responses , make sure your anser bo short as much as possible .";
        public frmAISuggestionsFrom()
        {
            InitializeComponent();
        }

        private void frmAISuggestionsFrom_Load(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            // send data from user text box to AI and get response then show it in the response textbox
      
            AIService ai = new AIService();

            string userMessage = UserTextBox.Text + NotePrompt;

            string result = await ai.GetResponseAsync(userMessage);

            AnswerTextBox.Text = result;
        }
           
        

        private void AnswerTextBox_TextChanged(object sender, EventArgs e)
        {
            // show responce from AI in the AnswerTextBox
        }
    }
}
