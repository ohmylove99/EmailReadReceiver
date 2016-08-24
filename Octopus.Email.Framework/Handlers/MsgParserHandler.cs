using MsgReader;
using System;
using System.Collections.Generic;
using System.IO;

namespace Octopus.Email.Framework.Handlers
{
    public class MsgParserHandler : BaseHandler
    {
        public override IMessage Filter(IMessage message) { return message; }
        public override IMessage HandleRequest(IMessage message)
        {
            FileInfo[] files = message.Data as FileInfo[];
            IDictionary<string, string[]> dict = new Dictionary<string, string[]>();
            foreach (var file in files)
            {
                Console.WriteLine("Try process '" + file + "'");
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
