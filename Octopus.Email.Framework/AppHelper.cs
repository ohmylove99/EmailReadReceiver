using System;
using System.Data;
using System.IO;

namespace Octopus.Email.Framework
{
    public class AppHelper
    {
        public static string GetTemporaryFolder()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}
