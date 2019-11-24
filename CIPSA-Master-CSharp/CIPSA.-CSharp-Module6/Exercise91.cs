using CIPSA._CSharp_Module6.Contracts;
using System;
using System.Diagnostics;

namespace CIPSA._CSharp_Module6
{
    class Exercise91 : IExercisesModule6
    {
        public void ExecuteExercise()
        {
            Console.WriteLine("Escriba el nombre del fichero para consultar si es un ejecutable (.exe)");
            var value = Console.ReadLine();
            var result = GetFullPath(value);

        }

        public static bool ExistsOnPath(string exeName)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "where";
                p.StartInfo.Arguments = exeName;
                p.Start(); p.WaitForExit();
                return p.ExitCode == 0;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                throw new Exception("'where' command is not on path");
            }
        }
        public static string GetFullPath(string exeName)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "where";
                p.StartInfo.Arguments = exeName;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                if (p.ExitCode != 0)
                    return null; // just return first match 
                return output.Substring(0, output.IndexOf(Environment.NewLine));
            }
            catch (System.ComponentModel.Win32Exception)
            {
                throw new Exception("'where' command is not on path");
            }
        }
    
    
    }        
}