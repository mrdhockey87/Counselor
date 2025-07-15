using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Drawing;

namespace CounselQuickPlatinum
{
    class Utilities
    {

        internal static string GetCQPUserDataDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Counselor";
        }

        public static int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }


        public static int GetProcessIdByWindowTitle(string AppId)
        {
            Process[] P_CESSES = Process.GetProcesses();
            for (int p_count = 0; p_count < P_CESSES.Length; p_count++)
            {
                if (P_CESSES[p_count].MainWindowTitle.Equals(AppId))
                {
                    return P_CESSES[p_count].Id;
                }
            }

            return Int32.MaxValue;
        }

        internal static string DataGridViewToHTML(DataGridView dg)
        {
            StringBuilder strB = new StringBuilder();
            
            //create html & table
            string headerInfo = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> \n"
                                + "<html xmlns=\"http://www.w3.org/1999/xhtml\">                                    \n"
                                + "<head>                                                                           \n"
                                + "    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />    \n"
                                + "    <title>Untitled Document</title>                                             \n"
                                + "    <style rel=\"stylesheet\" type=\"text/css\">                                 \n"
                                + "         body                                                                    \n"
                                + "         {                                                                       \n"
                                + "            font-family:\"Trebuchet MS\", Arial, Helvetica, sans-serif;          \n"
                                + "            font-size:75%;                                                       \n"
                                + "         }                                                                       \n"
                                + "         th[scope=col]                                                           \n"
                                + "         {                                                                       \n"
                                + "             background:#3A769A;                                                 \n"
                                + "             color:#FFF;                                                         \n"
                                + "             text-align:left;                                                    \n"
                                + "             border: 1px solid black;                                            \n"
                                + "             border-collapse: collapse;                                          \n"
                                + "         }                                                                       \n"
                                + "         table                                                                   \n"
                                + "         {                                                                       \n"
                                + "             margin:0 auto ;                                                     \n"
                                + "         }                                                                       \n"
                                + "         td                                                                      \n" 
                                + "         {                                                                       \n"
                                + "             border: 1px solid black;                                            \n"
                                + "             border-collapse: collapse;                                          \n"
                                + "         }                                                                       \n"
                                + "     </style>                                                                    \n"
                                + "</head>                                                                          \n";

            strB.AppendLine(headerInfo);
            strB.AppendLine(/*"<html><body><font face=\"Trebuchet MS\"><center>" +*/
                            "<body><table width=\"100%\" border='0' cellpadding='5px' cellspacing='0'>");
            strB.AppendLine("<tr>");

            //create table header
            for (int i = 0; i < dg.Columns.Count; i++)
            {
                if (dg.Columns[i].Visible == false)
                    continue;

                strB.AppendLine("<th scope=\"col\">" + dg.Columns[i].HeaderText + "</th>");
            }
            strB.AppendLine("</tr>");

            //create table body
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if(i % 2 == 0)
                    strB.AppendLine("<tr bgcolor=\"#CCCCCC\">");
                else
                    strB.AppendLine("<tr >");
                
                foreach (DataGridViewCell dgvc in dg.Rows[i].Cells)
                {
                    if (dgvc.OwningColumn.Visible == false)
                        continue;

                    string value;

                    if (dgvc.Value is DateTime)
                    {
                        string dtString = dgvc.Value.ToString();
                        DateTime dt = Convert.ToDateTime(dtString);
                        value = GetDateTimeFormattedValue(dt);
                    }
                    else
                        value = dgvc.Value.ToString();

                    strB.AppendLine("<td>" + value + "</td>");
                }
                strB.AppendLine("</tr>");

            }

            //table footer & end of html file
            strB.AppendLine("</table></body></html>");
            
            return strB.ToString();
        }


        private static string GetDateTimeFormattedValue(DateTime dt)
        {
            return dt.ToString("yyyy MM dd");
        }

        public static bool EqualSortedDict<K, V>(SortedDictionary<K, V> x, SortedDictionary<K, V> y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x is null || y is null)
                return false; //both being null already hit above.
            if (x.Count != y.Count)
                return false;
            if (!x.Comparer.Equals(y.Comparer))
                return false;//check if this is what you need. Probably is but might not be in some cases.
            foreach (KeyValuePair<K, V> kvp in x)
            {
                K key = kvp.Key;
                V value = kvp.Value;

                bool exists = y.TryGetValue(key, out V outValue);

                if (!exists)
                    return false;

                if(value.GetHashCode() != outValue.GetHashCode())
                    return false;

            }
            return true;
        }

        internal static string WordWrappedString(string stringToReduce, System.Drawing.Font f, 
                                                    System.Drawing.Graphics g, float desiredWidth)
        {
            char []splitters = {',', ' ', '\n', '.', ';', '!', '?', '-'};

            StringBuilder sb = new StringBuilder();
            int firstIndex = stringToReduce.IndexOfAny(splitters);
            string temp = stringToReduce.Substring(0, firstIndex+1);

            while (stringToReduce.Length > 0)
            {
                float widthOfParsedPlusChunk = g.MeasureString(sb.ToString() + temp, f).Width;

                if (widthOfParsedPlusChunk > desiredWidth)
                {
                    if (sb.ToString() != "")
                        sb.Append("\n");

                    // if it still doesn't fit, increase width, otherwise we'll
                    // never accept and constantly be adding unnecessary newlines
                    if (g.MeasureString(sb.ToString() + temp, f).Width > desiredWidth)
                        desiredWidth = g.MeasureString(sb.ToString() + temp, f).Width;
                }

                //sb.Insert(0, temp);
                sb.Append(temp);

                int length = stringToReduce.Length - temp.Length;

                if (length <= 0)
                    break;
                else
                    stringToReduce = stringToReduce.Substring(temp.Length);

                firstIndex = stringToReduce.IndexOfAny(splitters);
                if (firstIndex == -1)
                    temp = stringToReduce;
                else
                    temp = stringToReduce.Substring(0, firstIndex+1);
            }

            return sb.ToString();
        }


        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }
        //Make sure the forms are visible so we can fix it if it isn't mdail 8-15-19
        public static bool isWindowVisible(Rectangle rect)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.IntersectsWith(rect))
                    return true;
            }
            return false;
        }      
        //Centers Form sent into the method on the primary screen mdail 8-15-19
        public static void centerFormPrimary(Form _form)
        {            
            int height = _form.ClientSize.Height;//472;
            int width = _form.ClientSize.Width; //413;
            Size size = new Size(width, height);
            int x = (Screen.PrimaryScreen.WorkingArea.Width - width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Height - height) / 2;
            Point location = new Point(x, y);
            _form.DesktopBounds = new Rectangle(location, size);
            _form.WindowState = FormWindowState.Normal;
        }
        //Center the splash screen position based on it size and the offset from form1's x & y, the splash screen is passed in to the method mdail 12-20-19
        public static void centerSplashInForm1(Form _form)
        {
            //get the splash screen's x & y form1 width & height minus offset from its width & height divided by 2 mdail 12-20-19
            int Yoffset = (Properties.Settings.Default.Form1Height - _form.Size.Height) / 2;
            int Xoffset = (Properties.Settings.Default.Form1Width - _form.Size.Width) / 2;
            //calculate the splash screen X & Y by adding the offset to form1's X & y then making the new location's point from the  calculated X & Y mdail 12-20-19
            int splashX = Properties.Settings.Default.Form1Location.X + Xoffset;
            int splashY = Properties.Settings.Default.Form1Location.Y + Yoffset;
            Point location = new Point(splashX, splashY);
            //make the desktop bounds rectangle start at the new location for the splash screens size with a normal state mdail 12-20-19
            _form.DesktopBounds = new Rectangle(location, _form.Size);
            _form.WindowState = FormWindowState.Normal;
        }


    }
}
