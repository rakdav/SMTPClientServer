using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMTPClientServer
{
    public partial class Form1 : Form
    {
        private string recieveData;
        private string sendString;
        private byte[] dataToSend;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient smtpServer = new TcpClient(txtSmtpServer.Text,25);
            lstLog.Items.Add("Connection establish");
            NetworkStream writeStream = smtpServer.GetStream();
            StreamReader readStream = new StreamReader(smtpServer.GetStream());
            recieveData = readStream.ReadLine();
            lstLog.Items.Add(recieveData);
            sendString = "MAIL FROM: " + "<" + txtFrom.Text + ">";
            dataToSend = Encoding.ASCII.GetBytes(sendString);
            writeStream.Write(dataToSend,0,dataToSend.Length);
            recieveData = readStream.ReadLine();
            lstLog.Items.Add(recieveData);
            sendString = "DATA " + "\r\n";
            dataToSend= Encoding.ASCII.GetBytes(sendString);
            writeStream.Write(dataToSend, 0, dataToSend.Length);
        }

    }
}
