using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.Threading;

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
				try {
					Log("Trying to Stop Process!", "ALWAYS");
					foreach (Process proc in Process.GetProcessesByName("logonassistant")) {
						proc.Kill();
					}
				} 
				catch (Exception) {
					Log("Failed to Stop Process!", "ERROR");
					throw;
				}
				Log("Trying to Delete File", "DELETE");
				try{
					File.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\logonassistant.exe");
				}
				catch{
					Log("Failed to Delete File!", "ERROR");
					System.Windows.Forms.MessageBox.Show("Warning could not Delete the File you must Delete it Manualy!!", "Warning!");
					System.Diagnostics.Process Explorer = new System.Diagnostics.Process();
                    Explorer.StartInfo.FileName = "explorer.exe";
                    Explorer.StartInfo.Arguments = System.Environment.GetFolderPath(Environment.SpecialFolder.System);
                    Explorer.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    Explorer.Start();
                    System.Windows.Forms.MessageBox.Show("Search and Delete only the File logonassistant.exe!!!!", "Warning!");
				}
				RegistryKey k = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
				k.DeleteValue("logonassist");
                k.Close();
                Log("Removed the Startup Key!", "DELETE");
                RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
                objRegistryKey.DeleteValue("DisableTaskMgr");
                objRegistryKey.DeleteValue("DisableRegistryTools");
                objRegistryKey.Close();
                Log("Removed the Disabled Task Manager Key!", "DELETE");
                Log("Removed everything!", "DELETE SUCCESFUL");
                Console.WriteLine("Removed everything from the System!");
                Console.WriteLine("Show at your Desktop the Log File to know what was deleted!");
                Console.WriteLine("Press any Key to continue!");
                Console.ReadKey(true);
			}
			catch (Exception) {
				Log("Cant Delete The The RAT!", "DELETE FAILED!");
				Console.WriteLine("Cant Delete the RAT!");
				Console.WriteLine("Press any Key to continue!");
				Console.ReadKey(true);
			}
			
		}
	}
}