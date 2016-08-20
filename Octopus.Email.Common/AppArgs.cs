using System;

namespace Octopus.Email.Common
{
    public class AppArgs
    {
        private String[] arguments = Environment.GetCommandLineArgs();
        private const string KEY_PATH = "Path";
        private string[] Parse()
        {
            var dest = new String[arguments.Length -1];
            Array.Copy(arguments, 1, dest, 0, arguments.Length - 1);
            return dest;
        }
        public string Path{
            get{return CommandLineDictionary.FromArguments(Parse(), '-','=')[KEY_PATH]; }
        }
    }
}
