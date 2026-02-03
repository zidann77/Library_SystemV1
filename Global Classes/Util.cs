using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Library.Global_Classes
{
    internal class Util
    {

        public static string GenerateGUID()
        {

            // Generate a new GUID
            Guid newGuid = Guid.NewGuid();

            // convert the GUID to a string
            return newGuid.ToString();

        }


        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;

        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            // Full file name. Change your file name   
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string extn = fi.Extension;
            return GenerateGUID() + extn;

        }

        //private static bool SaveFile( string SourceFile, string destinationFile)
        //{
        //    try
        //    {
        //        File.Copy(SourceFile, destinationFile, true);

        //    }
        //    catch (IOException iox)
        //    {
        //        MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    return true;
        //}

        public enum enFileDestination { People, book }

        static public enFileDestination FileDestination = enFileDestination.People;

        public static bool CopyPeopleImageToProjectImagesFolder(enFileDestination FileDestination, ref string sourceFile)
        {
            // this funciton will copy the image to the
            // project images foldr after renaming it
            // with GUID with the same extention, then it will update the sourceFileName with the new name.

            string DestinationFolder = "";

            if(FileDestination == enFileDestination.People) 
             DestinationFolder = @"C:\Library-People-Images\People";
            else
                DestinationFolder = @"C:\Library-People-Images\Books";

            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, destinationFile, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile = destinationFile;
            return true;

        }

        private static readonly string KeyFolder = @"C:\LibraryApp\";
        private static readonly string KeyFilePath = @"C:\LibraryApp\app.key";

        // ===================== KEY MANAGEMENT =====================

        public static string GetOrCreateAESKey()
        {

            if (!Directory.Exists(KeyFolder))
                Directory.CreateDirectory(KeyFolder);

            if (File.Exists(KeyFilePath))
                return File.ReadAllText(KeyFilePath);

            string newKey = GenerateAESKey();
            File.WriteAllText(KeyFilePath, newKey);
            return newKey;
        }

        private static string GenerateAESKey()
        {
            byte[] keyBytes = new byte[32]; // 256-bit
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }

        // ===================== AES ENCRYPTION =====================

        public static string AESEncrypt(string plainText)
        {
            string key = GetOrCreateAESKey();
            byte[] keyBytes = Convert.FromBase64String(key);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.GenerateIV();

                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string AESDecrypt(string cipherText)
        {
            string key = GetOrCreateAESKey();
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;

                byte[] iv = new byte[16];
                Array.Copy(fullCipher, iv, iv.Length);
                aes.IV = iv;

                using (var ms = new MemoryStream(fullCipher, 16, fullCipher.Length - 16))
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        // Delete 

        public static bool DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false; // الملف ما موجود
        }

        //public static bool DeletePeopleImage(string fileName)
        //{
        //    string fullPath = Path.Combine(@"C:\Library-People-Images\People", fileName);
        //    return DeleteFile(fullPath);
        //}

        //public static bool DeleteBookImage(string fileName)
        //{
        //    string fullPath = Path.Combine(@"C:\Library-People-Images\Books", fileName);
        //    return DeleteFile(fullPath);
        //}


        public static bool ExportDataTableToExcel(
      DataTable dt,
      string sheetName = "Sheet1",
      string fileName = "Export.xlsx")
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("No data to export", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Add(Type.Missing);
                worksheet = workbook.ActiveSheet;
                worksheet.Name = sheetName;

                // 🔹 Headers
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                    worksheet.Cells[1, i + 1].Font.Bold = true;
                }

                // 🔹 Rows
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        worksheet.Cells[r + 2, c + 1] = dt.Rows[r][c]?.ToString();
                    }
                }

                worksheet.Columns.AutoFit();

                // 🔹 اسم تلقائي فريد
                string finalFileName =
                    $"{System.IO.Path.GetFileNameWithoutExtension(fileName)}_" +
                    $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = finalFileName
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(sfd.FileName);
                    MessageBox.Show("Exported Successfully ✅",
                        "Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                workbook?.Close(false);
                excelApp?.Quit();

                // 🔹 تحرير الـ COM Objects (مهم جدًا)
                if (worksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                worksheet = null;
                workbook = null;
                excelApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return true;
        }


    }
}
