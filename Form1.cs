using MailKit.Net.Pop3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenPop;
using System.Net.Mail;

namespace SMTPClientServer
{
    public partial class Form1 : Form
    {
        private string recieveData;
        private string sendString;
        private byte[] dataToSend;
        private Socket _Socket = null;
        private string Host = String.Empty;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //TcpClient smtpServer = new TcpClient(txtSmtpServer.Text, 25);
                //lstLog.Items.Add("Connection establish");
                //NetworkStream writeStream = smtpServer.GetStream();
                //StreamReader readStream = new StreamReader(smtpServer.GetStream());
                //recieveData = readStream.ReadLine();
                //lstLog.Items.Add(recieveData);

                //var user = Encoding.UTF8.GetBytes(txtUserName.Text);
                //var pass = Encoding.UTF8.GetBytes(txtPassword.Text);
                //string userCode = Convert.ToBase64String(user);
                //string passCode = Convert.ToBase64String(pass);
                //sendString = "AUTH LOGIN: " + "\r\n" + userCode + "\r\n" + passCode;
                //dataToSend = Encoding.ASCII.GetBytes(sendString);
                //writeStream.Write(dataToSend, 0, dataToSend.Length);
                //recieveData = readStream.ReadLine();
                //lstLog.Items.Add(recieveData);

                //sendString = "MAIL FROM: " + "<" + txtFrom.Text + ">";
                //dataToSend = Encoding.ASCII.GetBytes(sendString);
                //writeStream.Write(dataToSend, 0, dataToSend.Length);
                //recieveData = readStream.ReadLine();
                //lstLog.Items.Add(recieveData);

                //sendString = "RCPT TO: " + " <" + txtTo.Text + ">";
                //dataToSend = Encoding.ASCII.GetBytes(sendString);
                //writeStream.Write(dataToSend, 0, dataToSend.Length);
                //recieveData = readStream.ReadLine();
                //lstLog.Items.Add(recieveData);

                //sendString = "DATA " + "\r\n";
                //dataToSend = Encoding.ASCII.GetBytes(sendString);
                //writeStream.Write(dataToSend, 0, dataToSend.Length);
                //recieveData = readStream.ReadLine();
                //lstLog.Items.Add(recieveData);

                //sendString = "SUBJECT: " + Subject.Text + "\r\n" +
                //              txtMessage.Text + "." + "\r\n";
                //dataToSend = Encoding.ASCII.GetBytes(sendString);
                //writeStream.Write(dataToSend, 0, dataToSend.Length);
                //recieveData = readStream.ReadLine();
                //lstLog.Items.Add(recieveData);

                //sendString = "QUIT: " + "\r\n";
                //dataToSend = Encoding.ASCII.GetBytes(sendString);
                //writeStream.Write(dataToSend, 0, dataToSend.Length);
                //recieveData = readStream.ReadLine();
                //lstLog.Items.Add(recieveData);

                //writeStream.Close();
                //readStream.Close();
                //smtpServer.Close();
                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress(txtFrom.Text, "Tom");
                // кому отправляем
                MailAddress to = new MailAddress(txtTo.Text);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Тест";
                // текст письма
                m.Body = "<h2>"+txtMessage.Text+"</h2>";
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                // логин и пароль
                smtp.Credentials = new NetworkCredential(txtUserName.Text, txtPassword.Text);
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pop3Client client = new Pop3Client();
            client.Connect(txtPopServer.Text, 110, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(txtUserName.Text, txtPassword.Text);
            int count = client.GetMessageCount();
            int counter = 0;
            for (int i = count; i >= 1; i--)
            {
                MimeKit.MimeMessage message = client.GetMessage(i);
                listBox1.Items.Add(message.Headers);
                counter++;
                if (counter > 5)
                {
                    break;
                }
            }
        }
    }
}
