using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regedit
{
    class Program
    {
        static void Main(string[] args)
        {
            //Registry
            //RegistryKey
            //RegistryKey currentKey = Registry.CurrentUser;
            //RegistryKey myKey = currentKey.CreateSubKey("TestUser");
            //myKey.SetValue("login", "temp");
            //myKey.SetValue("password", "1");
            //myKey.Close();

            //RegistryKey myKey = currentKey.OpenSubKey("TestUser", true);
            //RegistryKey temp = myKey.CreateSubKey("Group");
            //temp.SetValue("groupName", "Admin");
            //temp.Close();
            RegistryKey currentKey = Registry.CurrentUser;
            RegistryKey run = currentKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            var items = run.GetValueNames();
            foreach (var item in items)
            {
                Console.WriteLine($"{item, -30}{run.GetValueKind(item), -15}{run.GetValue(item)}");
                if (item.Contains("Skype"))
                    run.DeleteValue(item);
            }
            run.Close();
        }
    }
}
