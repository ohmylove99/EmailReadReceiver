using MsgReader;
using Octopus.Email.Excel;
using System;
using System.IO;
using System.Linq;

namespace Octopus.Email.Common.MsgProcess
{
    public class MsgProcess
    {
        private AppArgs appArgs = new AppArgs();
        private ExcelProcess excelProcess = new ExcelProcess();
        public void Process()
        {
            string path = appArgs.Path;
            var msgReader = new Reader();
            if (File.Exists(path))
            {
                var file = new FileInfo(path);
                Console.WriteLine("Try process '" + file + "'");
                ProcessFile(msgReader, file);
            }
            else if (Directory.Exists(path))
            {
                var files = new DirectoryInfo(path).GetFiles("*.msg");
                Console.WriteLine("Found '" + files.Count() + "' files to process");
                foreach (var file in files)
                {
                    Console.WriteLine("Try process '" + file + "'");
                    ProcessFile(msgReader, file);
                }
            }
            else
            {
                Console.WriteLine("path doesn't exist.");
            }
        }

        private string[] ProcessFile(Reader msgReader, FileInfo file)
        {
            var extractFiles = msgReader.ExtractToFolder(file.FullName, AppHelper.GetTemporaryFolder(), true);
            excelProcess.Run(extractFiles[1],0);
            return extractFiles;
        }
    }
}
