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
using System.Net.Mail;
using System.Collections;

namespace SMTPClientServer
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var mess = new MailMessage())
                {
                    var client = new SmtpClient(txtSmtpServer.Text, 25);
                    client.Credentials = new NetworkCredential(txtUserName.Text, txtPassword.Text);
                    client.EnableSsl = true;
                    mess.From = new MailAddress(txtFrom.Text);
                    mess.To.Add(new MailAddress(txtTo.Text));
                    mess.Subject = Subject.Text;
                    mess.SubjectEncoding = Encoding.UTF8;
                    mess.Priority = MailPriority.Normal;
                    mess.IsBodyHtml = false;
                    mess.Body = txtMessage.Text;
                    try
                    {
                        client.Send(mess);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex); ;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); ;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                POP3 pop3 = new POP3();
                
                pop3.ConnectPOP(txtPopServer.Text, txtUserName.Text, txtPassword.Text);
                ArrayList messageList = pop3.ListMessage();
                foreach (Pop3EmailMessage popMes in messageList)
                {
                    Pop3EmailMessage mes = pop3.RetrieveMessage(popMes);
                    listBox1.Items.Add(mes.msgNumber+ " "+mes.msgSize);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
