using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailReadReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            new Sender().Send();
            new Receiver().Receive();
        }
    }

    class Sender
    {
        public void Send()
        {
            var message = new MailMessage("jason@octopus.org", "dashboard@octopus.org");
            message.Subject = "What Up, Dog?";
            message.Body = "Why you gotta be all up in my grill?";
            SmtpClient mailer = new SmtpClient("localhost", 25);
            mailer.Credentials = new NetworkCredential("jason", "jason");
            //mailer.EnableSsl = true;
            mailer.Send(message);
        }
    }

    class Receiver
    {
        public void Receive()
        {
            using (Pop3Client client = new Pop3Client())
            {
                client.Connect("localhost", 110, false);
                client.Authenticate("dashboard", "dashboard", AuthenticationMethod.UsernameAndPassword);
                if (client.Connected)
                {
                    int messageCount = client.GetMessageCount();
                    List<Message> allMessages = new List<Message>(messageCount);
                    for (int i = messageCount; i > 0; i--)
                    {
                        allMessages.Add(client.GetMessage(i));
                    }
                    foreach (Message msg in allMessages)
                    {
                        var att = msg.FindAllAttachments();
                        for (int attachment = 0; attachment < att.Count; attachment++)
                        {
                            FileInfo file = new FileInfo("Some File Name");
                            // do Save
                        }
                    }
                }
            }
        }
    }
}
