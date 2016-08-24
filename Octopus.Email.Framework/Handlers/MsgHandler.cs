using MsgReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Email.Framework.Handlers
{
    public class MsgHandler : BaseHandler
    {
        public override IMessage Filter(IMessage message) {
            FileInfo[] files = message.Data as FileInfo[];
            if (files == null)
            {
                Console.WriteLine("Error, input not  FileInfo[]");
                message.Data = null;
            }
            return message;
        }
        public override IMessage HandleRequest(IMessage message)
        {
            FileInfo[] files = message.Data as FileInfo[];
            IMessage filteredMsg = Filter(message);
            if(filteredMsg == null)
                Console.WriteLine("No Message to process, skip and return");
            if (successor != null)
            {
                return successor.HandleRequest(filteredMsg);
            }
            return message;
        }
    }
}
