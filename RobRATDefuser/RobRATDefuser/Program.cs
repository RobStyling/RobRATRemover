using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace RobRATDefuser
{
	class Program
	{
		
        public static string GetTempPath()
        {
        	string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        	if (!path.EndsWith("\\")) path += "\\";
        	return path;
        }
        
        public static void Log(string msg, string art)
        {
        	System.IO.StreamWriter sw = System.IO.File.AppendText(
        		GetTempPath() + "RATRemoveLog.txt");
        	try
        	{
        		string logLine = System.String.Format(
        			"{0:G}: {1}: {2} ", System.DateTime.Now, art, msg);
        		sw.WriteLine(logLine);
        	}
        	finally 
        	{
        		sw.Close();
        	}
        }
		public static void Main(string[] args)
		{
			Log("Deleting old Log!" , "ALWAYS");
		try {
        
          	File.Delete(GetTempPath() + "RATRemoveLog.txt");
		}
		catch {
            	
		}
			Log("Remover opened! Waiting for User Input!", "ALWAYS");
			Console.Write("Press any key to continue delting RobRAT...");
			Console.ReadKey(true);
			try{
				RegistryKey k = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
				k.DeleteValue("logonassist");
                k.Close();
                Log("Removed the Startup Key!", "DELETE");
                RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
                objRegistryKey.DeleteValue("DisableTaskMgr");
                objRegistryKey.Close();
                Log("Removed the Disabled Task Manager Key!", "DELETE");
                Log("Removed everything!", "DELETE SUCCESFUL");
                Console.WriteLine("Removed everything from the System!");
                Console.WriteLine("Show at your Desktop the Log File to know what was deleted!");
                Console.WriteLine("Press any Key to continue!");
                Console.ReadKey(true);
			}
			catch {
				Log("Cant Delete The The RAT!", "DELETE FAILED!");
				Console.WriteLine("Cant Delete the RAT!");
				Console.WriteLine("Press any Key to continue!");
				Console.ReadKey(true);
			}
			
		}
	}
}