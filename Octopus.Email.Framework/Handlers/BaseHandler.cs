using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Email.Framework.Handlers
{
    public abstract class BaseHandler : Handler
    {
        public override IMessage HandleRequest(IMessage message)
        {
            IMessage filteredMsg = Filter(message);
            if (successor != null)
            {
                return successor.HandleRequest(filteredMsg);
            }
            return filteredMsg;
        }
    }
}
