using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FisherLeakClientServerPTMA
{
    class FLMSGCollectionClient
    {
       //Decleared and initialies a variable count to allow message to be sent to server sequentially
        public int cnt = 0;
        //Deleared message in array  to send to the server 
        public string[] MsgForServer = {"end", "?Status", "?VECode", "=Pressure", "=WaterDischarge", "=VECode"};
        //Initialises appendMsg to null
        public string appendMsg = null;

        public void MsgToSend()
        {
            //cnts stand for count, it will increament each time the MsgTosend is call from anothe class
            cnt+=1;
            if(cnt == 6)
            {
                cnt = 0;
            }
            appendMsg = MsgForServer[cnt].ToString();
        }
        public void MsgRestToZero()
        {
            cnt--;
        }
    }
}
