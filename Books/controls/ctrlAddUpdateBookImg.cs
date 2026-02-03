using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Library.Books.controls
{
    public partial class ctrlAddUpdateBookImg : UserControl
    {
        public ctrlAddUpdateBookImg()
        {
            InitializeComponent();

            AllowDrop = true;
            DragEnter += ctrlAddUpdateBookImg_DragEnter;
            DragDrop += ctrlAddUpdateBookImg_DragDrop;
        }

        // =========================
        // Event Args
        // =========================
        public class ImageChangedEventArgs : EventArgs
        {
            public string ImagePath { get; }

            public ImageChangedEventArgs(string imagePath)
            {
                ImagePath = imagePath;
            }
        }

        // =========================
        // Events
        // =========================
        public event EventHandler<ImageChangedEventArgs> ImageLoaded;
        public event EventHandler<ImageChangedEventArgs> ImageDeleted;

        // =========================
        // Properties
        // =========================
        public string ImagePath
        {
            get => guna2PictureBox1.ImageLocation;
            set
            {
                if (guna2PictureBox1.ImageLocation == value)
                    return;

                guna2PictureBox1.ImageLocation = value;
                UpdateUI();
            }
        }

        // =========================
        // Public Methods
        // =========================
        public void ClearImage()
        {
            if (string.IsNullOrEmpty(ImagePath))
                return;

            string oldPath = ImagePath;
            guna2PictureBox1.ImageLocation = null;

            UpdateUI();
            ImageDeleted?.Invoke(this, new ImageChangedEventArgs(oldPath));
        }

        // =========================
        // UI Logic
        // =========================
        private void UpdateUI()
        {
            bool hasImage = !string.IsNullOrEmpty(ImagePath);
            btnAddImg.Visible = !hasImage;
            btnDeleteImg.Visible = hasImage;
        }

        // =========================
        // Buttons
        // =========================
        private void btnAddImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadImage(openFileDialog1.FileName);
            }
        }

        private void btnDeleteImg_Click(object sender, EventArgs e)
        {
            ClearImage();
        }

        // =========================
        // Drag & Drop
        // =========================
        private void ctrlAddUpdateBookImg_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ctrlAddUpdateBookImg_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null) return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0) return;

            string file = files[0];
            if (!IsImageFile(file)) return;

            LoadImage(file);
        }

        // =========================
        // Helpers
        // =========================
        private void LoadImage(string path)
        {
            ImagePath = path;
            ImageLoaded?.Invoke(this, new ImageChangedEventArgs(path));
        }

        private bool IsImageFile(string path)
        {
            string ext = Path.GetExtension(path).ToLower();
            return ext == ".jpg" || ext == ".jpeg" ||
                   ext == ".png" || ext == ".gif" ||
                   ext == ".bmp";
        }

        private void ctrlAddUpdateBookImg_Load(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
