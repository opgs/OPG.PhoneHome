using OPG.Signage.Info;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace OPG.PhoneHome
{
    class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private NativeMethods() { }
    }

    class Caller
    { 
        public static void SetConsoleWindowVisibility(bool visible, string title)
        {
            // below is Brandon's code                    
            IntPtr hWnd = NativeMethods.FindWindow(null, title);

            if (hWnd != IntPtr.Zero)
            {
                if (!visible)
                    //Hide the window                   
                    NativeMethods.ShowWindow(hWnd, 0); // 0 = SW_HIDE               
                else
                    //Show window again                   
                    NativeMethods.ShowWindow(hWnd, 1); //1 = SW_SHOWNORMA          
            }
        }

        [STAThread()]
        public static void Main(string[] args)
        {
            Console.Title = "OPG.PhoneHome";

            SetConsoleWindowVisibility(false, Console.Title);

            string regUrl = "https://dev.opgs.org/signage/index.php?page=userdevice";

            Console.WriteLine("OPG.PhoneHome");

            if (args.Length > 0)
            {
                regUrl = args[0] + "&ip=" + Network.Ip + "&hostname=" + Network.HostName + "&user=" + Environment.UserName;
            }
            else
            {
                regUrl += "&ip=" + Network.Ip + "&hostname=" + Network.HostName + "&user=" + Environment.UserName;
            }

            Task call = Task.Run(async () =>
            {
                using (HttpClient curl = new HttpClient())
                {
                    Console.WriteLine(regUrl);
                    using (HttpResponseMessage httpres = await curl.GetAsync(new Uri(regUrl)))
                    {
                        Console.WriteLine(await httpres.Content.ReadAsStringAsync());
                    }
                    Console.WriteLine("Done");
                }
            });
            call.Wait();
        }
    }
}
