using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Email.Framework.Services
{
    public class MsgProducer
    {
        public const string Extension = "*.msg";
        public string Path { get; set; }
        public FileInfo[] Files { get; set; }
        public MsgProducer(string Path)
        {
            {
                this.Path = Path;
                if (File.Exists(Path))
                {
                    var file = new FileInfo(Path);
                    if (file.Extension == Extension)
                        Console.WriteLine("Try to process '" + file + "'");
                    Files[0] = file;
                }
                else if (Directory.Exists(Path))
                {
                    var files = new DirectoryInfo(Path).GetFiles("*.msg");
                    Console.WriteLine("Found '" + files.Count() + "' files to process");
                    Files = files;
                }
            }
        }
    }
}
