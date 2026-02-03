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

namespace Library.Dashboard.Fines
{
    public partial class frmFineStatistics : Form
    {
        private Guna2Panel pnlRoot;
        private Guna2Panel pnlCards;
        private Guna2Button btnExport;

        private DataTable finesTable;

        // تعريف الكروت (العنوان + اسم العمود من الـ DataTable)
        private readonly (string Title, string ColumnName)[] FineCards =
        {
            ("Total Fines", "TotalFines"),
            ("Total Fine Amount", "TotalFineAmount"),
            ("Unpaid Fines", "LateBorrowings"),
            ("Cash Payments", "CashFineAmount"),
            ("Visa Payments", "VisaFineAmount"),
            ("Cash Ops Count", "CashFinesCount"),
            ("Visa Ops Count", "VisaFinesCount"),
            ("Total Late Days", "TotalLateDays")
        };

        public frmFineStatistics()
        {
            InitializeComponent();
            this.Load += frmFineStatistics_Load;
        }

        private void frmFineStatistics_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(245, 247, 250);

            // جلب البيانات مباشرة من BLL
            finesTable = clsDashboard.GetFinesStatistics();

            if (finesTable == null || finesTable.Rows.Count == 0)
            {
                MessageBox.Show("No data found", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            BuildDashboard();
        }

        private void BuildDashboard()
        {
            if (pnlRoot != null)
            {
                this.Controls.Remove(pnlRoot);
                pnlRoot.Dispose();
            }

            pnlRoot = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(25),
                FillColor = Color.Transparent
            };
            this.Controls.Add(pnlRoot);

            pnlCards = new Guna2Panel
            {
                Dock = DockStyle.Top,
                Padding = new Padding(20, 60, 20, 20),
                FillColor = Color.Transparent
            };
            pnlRoot.Controls.Add(pnlCards);

            int cardWidth = 260;
            int cardHeight = 150;
            int spacingX = 25;
            int spacingY = 25;

            int availableWidth = this.ClientSize.Width - 50;
            int cardsPerRow = Math.Max(1, availableWidth / (cardWidth + spacingX));

            Color[] colorsStart =
            {
                Color.FromArgb(99, 102, 241),
                Color.FromArgb(16, 185, 129),
                Color.FromArgb(245, 158, 11),
                Color.FromArgb(236, 72, 153),
                Color.FromArgb(239, 68, 68),
                Color.FromArgb(52, 211, 153),
                Color.FromArgb(96, 165, 250),
                Color.FromArgb(234, 179, 8)
            };

            Color[] colorsEnd =
            {
                Color.FromArgb(79, 70, 229),
                Color.FromArgb(5, 150, 105),
                Color.FromArgb(217, 119, 6),
                Color.FromArgb(219, 39, 119),
                Color.FromArgb(220, 38, 38),
                Color.FromArgb(16, 185, 129),
                Color.FromArgb(59, 130, 246),
                Color.FromArgb(202, 138, 4)
            };

            DataRow row = finesTable.Rows[0];
            int index = 0;

            foreach (var card in FineCards)
            {
                int x = pnlCards.Padding.Left + (index % cardsPerRow) * (cardWidth + spacingX);
                int y = pnlCards.Padding.Top + (index / cardsPerRow) * (cardHeight + spacingY);

                pnlCards.Controls.Add(CreateCard(
                    card.Title,
                    row[card.ColumnName].ToString(),
                    colorsStart[index % colorsStart.Length],
                    colorsEnd[index % colorsEnd.Length],
                    x, y));

                index++;
            }

            int rows = (index + cardsPerRow - 1) / cardsPerRow;
            pnlCards.Height = pnlCards.Padding.Top + pnlCards.Padding.Bottom +
                              rows * (cardHeight + spacingY);
        }

        private Guna2ShadowPanel CreateCard(
            string title,
            string value,
            Color colorStart,
            Color colorEnd,
            int x,
            int y)
        {
            Guna2ShadowPanel shadow = new Guna2ShadowPanel
            {
                Size = new Size(260, 150),
                Radius = 25,
                ShadowColor = Color.Gray,
                ShadowShift = 5,
                Location = new Point(x, y)
            };

            Guna2GradientPanel card = new Guna2GradientPanel
            {
                Dock = DockStyle.Fill,
                BorderRadius = 25,
                FillColor = colorStart,
                FillColor2 = colorEnd
            };

            Guna2HtmlLabel lblTitle = new Guna2HtmlLabel
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.WhiteSmoke,
                Location = new Point(20, 20)
            };

            Guna2HtmlLabel lblValue = new Guna2HtmlLabel
            {
                Text = value,
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 60)
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            shadow.Controls.Add(card);

            // Hover Effect
            shadow.MouseEnter += (s, e) =>
            {
                shadow.ShadowShift = 15;
                shadow.Size = new Size(270, 160);
                card.FillColor = ControlPaint.Light(colorStart);
                card.FillColor2 = ControlPaint.Light(colorEnd);
            };

            shadow.MouseLeave += (s, e) =>
            {
                shadow.ShadowShift = 5;
                shadow.Size = new Size(260, 150);
                card.FillColor = colorStart;
                card.FillColor2 = colorEnd;
            };

            return shadow;
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

                        // GradientPanel inside
                        foreach (Control c in panel.Controls[0].Controls)
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

                // لو صار نفس الاسم بنفس الثانية (نادر) نخليه يزيد رقم
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
                // Close Excel properly
                if (xlWorkBook != null) xlWorkBook.Close(false);
                if (xlApp != null) xlApp.Quit();

                // Release COM objects (important)
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


        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            // export to excel
            ExportDashboardToExcel();
        }

        private void frmFineStatistics_Load_1(object sender, EventArgs e)
        {

        }
    }
}
