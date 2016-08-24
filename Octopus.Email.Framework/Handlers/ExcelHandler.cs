using MsgReader;
using System;
using System.Collections.Generic;
using System.IO;

namespace Octopus.Email.Framework.Handlers
{
    public class ExcelHandler : BaseHandler
    {
        public override IMessage Filter(IMessage message)
        {
            IDictionary<string, string[]> dict = message.Data as IDictionary<string, string[]>;
            if (dict == null)
            {
                Console.WriteLine("Error, input not  IDictionary<string, string[]>");
                message.Data = null;
            }
            return message;
        }

        public override IMessage HandleRequest(IMessage message)
        {
            IMessage filteredMsg = Filter(message);
            if (filteredMsg.Data == null)
            {
                return null;
            }
            IDictionary<string, string[]> dict = filteredMsg.Data as IDictionary<string, string[]>;

            foreach (var file in dict.Keys)
            {
                Console.WriteLine("Try to process '" + file + "'");
                string[] val = ProcessFile(file);
                dict.Add(file.Name, val);
            }
            IMessage filteredMsg = new Message();
            filteredMsg.Data = dict;
            //
            if (successor != null)
            {
                return successor.HandleRequest(filteredMsg);
            }
            return filteredMsg;
        }
        public string[] ProcessFile(FileInfo file)
        {
            var msgReader = new Reader();
            var extractFiles = msgReader.ExtractToFolder(file.FullName, AppHelper.GetTemporaryFolder(), true);
            return extractFiles;
        }
    }
}
