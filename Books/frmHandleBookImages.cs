using Library.Books.controls;
using Library.Global_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Library.Books
{
    public partial class frmHandleBookImages : Form
    {
        // =========================
        // Events
        // =========================
        public delegate void DataBackEventHandler(object sender, List<string> images);
        public event DataBackEventHandler DataBack;

        // =========================
        // Data (Single Source of Truth)
        // =========================
        private List<string> Images = new List<string> { null, null, null };

        // =========================
        // Controls
        // =========================
        private List<ctrlAddUpdateBookImg> controls;

        // =========================
        // Constructor
        // =========================
        public frmHandleBookImages(List<string> bookImages)
        {
            InitializeComponent();

            for (int i = 0; i < bookImages.Count && i < 3; i++)
                Images[i] = bookImages[i];

            InitControls();
        }

        // =========================
        // Init
        // =========================
        private void InitControls()
        {
            controls = new List<ctrlAddUpdateBookImg>
            {
                ctrlAddUpdateBookImg1,
                ctrlAddUpdateBookImg2,
                ctrlAddUpdateBookImg3
            };

            for (int i = 0; i < controls.Count; i++)
            {
                int index = i;

                controls[i].ImageLoaded += (s, e) =>
                {
                    OnImageLoaded(index, e.ImagePath);
                };

                controls[i].ImageDeleted += (s, e) =>
                {
                    OnImageDeleted(index);
                };
            }

            RefreshUI();
        }

        // =========================
        // Image Logic
        // =========================
        private void OnImageLoaded(int index, string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            Util.CopyPeopleImageToProjectImagesFolder(
                Util.enFileDestination.book, ref path);

            Images[index] = path;
            RefreshUI();
        }

        private void OnImageDeleted(int index)
        {
            Images[index] = null;

            // Shift images up
            for (int i = index; i < Images.Count - 1; i++)
            {
                Images[i] = Images[i + 1];
                Images[i + 1] = null;
            }

            RefreshUI();
        }

        // =========================
        // UI Sync
        // =========================
        private void RefreshUI()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].ImagePath = Images[i];
                controls[i].Enabled = (i == 0 || Images[i - 1] != null);
            }

            UpdateButtons();
        }

        // =========================
        // Swap
        // =========================
        private void SwapImages(int a, int b)
        {
            if (Images[a] == null || Images[b] == null)
                return;

            string temp = Images[a];
            Images[a] = Images[b];
            Images[b] = temp;

            RefreshUI();
        }

        // =========================
        // Buttons State
        // =========================
        private void UpdateButtons()
        {
            btn1Right.Enabled = Images[0] != null && Images[1] != null;
            btn2Left.Enabled = btn1Right.Enabled;

            btn2Right.Enabled = Images[1] != null && Images[2] != null;
            btn3Left.Enabled = btn2Right.Enabled;
        }

        // =========================
        // Button Events
        // =========================
        private void btn1Right_Click(object sender, EventArgs e)
        {
            SwapImages(0, 1);
        }

        private void btn2Left_Click(object sender, EventArgs e)
        {
            SwapImages(0, 1);
        }

        private void btn2Right_Click(object sender, EventArgs e)
        {
            SwapImages(1, 2);
        }

        private void btn3Left_Click(object sender, EventArgs e)
        {
            SwapImages(1, 2);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> result = Images
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            DataBack?.Invoke(this, result);
            Close();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmHandleBookImages_Load(object sender, EventArgs e)
        {

        }
    }
}
