using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FisherLeakClientServerPTMA
{
    public partial class Form1 : Form
    {
        FLMSGCollectionClient fLMSG = new FLMSGCollectionClient();
        public Form1()
        {
            InitializeComponent();
        }

        public void StartClientServer()
        {
            try
            {
                //EStablish a remote endpoint for the socket.This example uses port 2689 on the local PC.
                IPHostEntry ipHost = Dns.GetHostEntry("10.30.8.99");
                IPAddress localIPAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(localIPAddr, 2689);
                //Creation of TCP/IP Socket using Socket class Construtor
                Socket sender = new Socket(localIPAddr.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
               
                try
                {
                  
                    lblcnts.Text = fLMSG.cnt.ToString() + " Message";
                    //Connect Socket to the remote endpoint using method Connect()
                    sender.Connect(localEndPoint);
                    //print Endpoint information that is Connected
                    //Create a message that will be sending each time an event occures to server
                    byte[] messageSent = Encoding.ASCII.GetBytes(fLMSG.appendMsg.ToString());
                    int byteSent = sender.Send(messageSent);
                    //Create a Data Buffer of 1024 byte
                    byte[] messageReceived = new byte[1024];
                    //This Method returns number of byte received
                    int byteRecv = sender.Receive(messageReceived);
                    ritxtBoxMsgDispl.Text = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
                    //Close the socket using close() method
                    //sender.Close();
                }
                catch (ArgumentNullException anx)
                {
                    ritxtBoxMsgDispl.Text = "ArgumnentNullException: {0}"+ anx.ToString();
            }
                catch(SocketException se)
                {
                    ritxtBoxMsgDispl.Text = "SocketException : {0}" + se.ToString();
                }
            }
            catch(Exception e)
            {
                ritxtBoxMsgDispl.Text = e.ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fLMSG.MsgToSend();
            StartClientServer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }
}
