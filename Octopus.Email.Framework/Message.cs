using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Email.Framework
{
    public interface IMessage
    {
        object Data { get; set; }
        IDictionary<string, string> Header { get; }
    }
    public class Message : IMessage
    {
        private IDictionary<string, string> _header = new Dictionary<string, string>();
        public IDictionary<string, string> Header
        {
            get { return _header; }
        }
        public object Data { get; set; }
    }
}
