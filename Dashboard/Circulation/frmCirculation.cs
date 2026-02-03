using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using BusinessLogicLayer;
using Guna.UI2.WinForms;
using Library.Message;
using Excel = Microsoft.Office.Interop.Excel;

namespace Library.Dashboard.Circulation
{
    public partial class frmCirculation : Form
    {
        DataTable DashboardTable;
        Guna2Panel pnlRoot;
        Guna2Panel pnlCards;

        public frmCirculation()
        {
            InitializeComponent();
        }

        private void frmCirculation_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(245, 247, 250);
            DashboardTable = clsDashboard.GetCirculationStatistics();
            BuildDashboard();
        }

        private void BuildDashboard()
        {
            if (pnlRoot != null)
            {
                this.Controls.Remove(pnlRoot);
                pnlRoot.Dispose();
            }

            pnlRoot = new Guna2Panel();
            pnlRoot.Dock = DockStyle.Fill;
            pnlRoot.Padding = new Padding(25);
            pnlRoot.FillColor = Color.Transparent;
            this.Controls.Add(pnlRoot);

            pnlCards = new Guna2Panel();
            pnlCards.Dock = DockStyle.Top;
            pnlCards.Padding = new Padding(20, 60, 20, 20);
            pnlCards.FillColor = Color.Transparent;
            pnlRoot.Controls.Add(pnlCards);

            if (DashboardTable == null || DashboardTable.Rows.Count == 0)
                return;

            DataRow row = DashboardTable.Rows[0];

            int cardWidth = 260;
            int cardHeight = 150;
            int spacing = 25;

            int availableWidth = pnlCards.Width - pnlCards.Padding.Left - pnlCards.Padding.Right;
            int cardsPerRow = Math.Max(1, availableWidth / (cardWidth + spacing));

            Color[] colorsStart = {
                Color.FromArgb(99, 102, 241),
                Color.FromArgb(16, 185, 129),
                Color.FromArgb(245, 158, 11),
                Color.FromArgb(236, 72, 153),
                Color.FromArgb(239, 68, 68)
            };

            Color[] colorsEnd = {
                Color.FromArgb(79, 70, 229),
                Color.FromArgb(5, 150, 105),
                Color.FromArgb(217, 119, 6),
                Color.FromArgb(219, 39, 119),
                Color.FromArgb(220, 38, 38)
            };

            int index = 0;
            foreach (DataColumn col in DashboardTable.Columns)
            {
                int x = pnlCards.Padding.Left + (index % cardsPerRow) * (cardWidth + spacing);
                int y = pnlCards.Padding.Top + (index / cardsPerRow) * (cardHeight + spacing);

                pnlCards.Controls.Add(CreateCard(
                    col.ColumnName,
                    row[col].ToString(),
                    colorsStart[index % colorsStart.Length],
                    colorsEnd[index % colorsEnd.Length],
                    x, y));

                index++;
            }

            int rows = (index + cardsPerRow - 1) / cardsPerRow;
            pnlCards.Height = pnlCards.Padding.Top + pnlCards.Padding.Bottom + rows * (cardHeight + spacing);
        }

        private Guna2ShadowPanel CreateCard(string title, string value, Color colorStart, Color colorEnd, int x, int y)
        {
            // Shadow Panel
            Guna2ShadowPanel shadow = new Guna2ShadowPanel();
            shadow.Size = new Size(260, 150);
            shadow.Radius = 20;
            shadow.ShadowColor = Color.Gray;
            shadow.ShadowShift = 5;
            shadow.Location = new Point(x, y);

            // Gradient Panel inside Shadow
            Guna2GradientPanel card = new Guna2GradientPanel();
            card.Dock = DockStyle.Fill;
            card.BorderRadius = 20;
            card.FillColor = colorStart;
            card.FillColor2 = colorEnd;

            // Title
            Guna2HtmlLabel lblTitle = new Guna2HtmlLabel();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTitle.ForeColor = Color.WhiteSmoke;
            lblTitle.Location = new Point(20, 20);

            // Value
            Guna2HtmlLabel lblValue = new Guna2HtmlLabel();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI", 32, FontStyle.Bold);
            lblValue.ForeColor = Color.White;
            lblValue.Location = new Point(20, 60);

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);

            shadow.Controls.Add(card);

            // Hover effect
            shadow.MouseEnter += (s, e) => { shadow.ShadowShift = 15; shadow.Size = new Size(270, 160); };
            shadow.MouseLeave += (s, e) => { shadow.ShadowShift = 5; shadow.Size = new Size(260, 150); };

            return shadow;
        }

        // ===== EXPORT BUTTON =====
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ExportDashboardToExcel();
        }

        private void ExportDashboardToExcel()
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkBook = null;
            Excel.Worksheet xlWorkSheet = null;

            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add();
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[1];

                xlWorkSheet.Cells[1, 1] = "Title";
                xlWorkSheet.Cells[1, 2] = "Value";

                Excel.Range headerRange = xlWorkSheet.Range["A1", "B1"];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = ColorTranslator.ToOle(Color.LightGray);

                int row = 2;
                foreach (Control shadow in pnlCards.Controls)
                {
                    if (shadow is Guna2ShadowPanel panel)
                    {
                        string title = "";
                        string value = "";

                        foreach (Control c in panel.Controls[0].Controls) // GradientPanel inside
                        {
                            if (c is Guna2HtmlLabel lbl)
                            {
                                if (lbl.Font.Size < 20)
                                    title = lbl.Text;
                                else
                                    value = lbl.Text;
                            }
                        }

                        xlWorkSheet.Cells[row, 1] = title;
                        xlWorkSheet.Cells[row, 2] = value;
                        row++;
                    }
                }

                xlWorkSheet.Columns.AutoFit();

                // ===== File Name Based On Date & Time =====
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string baseName = "LibraryDashboard";
                string ext = ".xlsx";

                string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string filePath = Path.Combine(desktopPath, $"{baseName}_{dateTime}{ext}");

                // لو صار بنفس الثانية وطلع نفس الاسم (نادر) نخليه يزيد رقم
                int counter = 1;
                while (File.Exists(filePath))
                {
                    filePath = Path.Combine(desktopPath, $"{baseName}_{dateTime}_{counter}{ext}");
                    counter++;
                }
                // =========================================

                xlWorkBook.SaveAs(filePath);

                frmMessage frm = new frmMessage($"✅ Exported successfully\n{Path.GetFileName(filePath)}", frmMessage.enMode.Info);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                frmMessage frm = new frmMessage("❌ Export Failed: " + ex.Message, frmMessage.enMode.Error);
                frm.ShowDialog();
            }
            finally
            {
                if (xlWorkBook != null) xlWorkBook.Close(false);
                if (xlApp != null) xlApp.Quit();

                if (xlWorkSheet != null) Marshal.ReleaseComObject(xlWorkSheet);
                if (xlWorkBook != null) Marshal.ReleaseComObject(xlWorkBook);
                if (xlApp != null) Marshal.ReleaseComObject(xlApp);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        // ===== CLOSE BUTTON =====
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
