using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMTPClientServer
{
    class POP3:TcpClient
        
    {
         private SslStream sslStream;
        public void ConnectPOP(string serverName, string userName,string password)
        {
            try
            {
                string message;
                string result = "";
                Connect(serverName, 995);
                sslStream = new SslStream(this.GetStream());
                sslStream.AuthenticateAsClient(serverName);
                result = Response();
                if (result.Substring(0, 3) != "+OK")
                {
                    throw new PopMailException(result);
                }

                message = "USER " + userName + "\r\n";
                Write(message);
                result = Response();
                if (result.Substring(0, 3) != "+OK")
                {
                    throw new PopMailException(result);
                }

                message = "PASS " + password + "\r\n";
                Write(message);
                result = Response();
                if (result.Substring(0, 3) != "+OK")
                {
                    throw new PopMailException(result);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DisconnectPOP()
        {
            string message;
            string result;
            message = "QUIT\r\n";
            Write(message);
            result = Response();
            if (result.Substring(0, 3) != "+OK")
            {
                throw new PopMailException(result);
            }
        }
        public void Write(string message)
        {
            ASCIIEncoding coding = new ASCIIEncoding();
            byte[] WriteBuffer = new byte[1024];
            WriteBuffer = coding.GetBytes(message);
            NetworkStream networkStream = GetStream();
            networkStream.Write(WriteBuffer, 0, WriteBuffer.Length);
        }
        private string Response()
        {
            ASCIIEncoding Encoder = new ASCIIEncoding();
            byte[] serverBuffer = new byte[2048];
            int count = 0;
            while(true)
            {
                byte[] buff = new byte[2];
                int bytes = sslStream.Read(buff, 0, 1);
                if(bytes==1)
                {
                    serverBuffer[count] = buff[0];
                    count++;
                    if(buff[0]=='\n')
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            string ReturnValue = Encoder.GetString(serverBuffer,0,count);
            return ReturnValue;
        }
        public ArrayList ListMessage()
        {
            string message;
            string result;
            ArrayList returnValue = new ArrayList();
            message = "LIST\r\n";
            Write(message);
            result = Response();
            if (result.Substring(0, 3) != "+OK")
            {
                throw new PopMailException(result);
            }
            while(true)
            {
                Pop3EmailMessage emailMessage = new Pop3EmailMessage();
                char[] sep = { ' ' };
                string[] values = result.Split(sep);
                emailMessage.msgNumber = int.Parse(values[0]);
                emailMessage.msgSize = int.Parse(values[1]);
                emailMessage.msgReceived = false;
                returnValue.Add(emailMessage);
                continue;
            }
        }

        public Pop3EmailMessage RetrieveMessage(Pop3EmailMessage msdRETR)
        {
            string message;
            string result;
            Pop3EmailMessage mailMessage = new Pop3EmailMessage();
            mailMessage.msgSize = msdRETR.msgSize;
            mailMessage.msgNumber = msdRETR.msgNumber;
            message = "RETR " + msdRETR.msgNumber + "\r\n";
            Write(message);
            result = Response();
            if (result.Substring(0, 3) != "+OK")
            {
                throw new PopMailException(result);
            }
            mailMessage.msgReceived = true;
            while(true)
            {
                result = Response();
                if (result == "\r\n")
                    break;
                else
                    mailMessage.msgContent = result;
            }
            return mailMessage;
        }

        public void DeleteMessage(Pop3EmailMessage emailMessage)
        {
            string message;
            string result;
            message = "DELE " + emailMessage.msgNumber + "\r\n";
            Write(message);
            result = Response();
            if (result.Substring(0, 3) != "+OK")
            {
                throw new PopMailException(result);
            }
        }
    }
}
