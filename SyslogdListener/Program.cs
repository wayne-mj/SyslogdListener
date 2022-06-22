/***********************************************************************************************************/
/***********************************************************************************************************/
/*****                               Copyright 2003-2022  Wayne M Jackson                              *****/
/*****                                        Syslogd Listener                                         *****/
/*****                               Code provided as is with no warranty                              *****/
/*****            You are free to use this code for any project that does not derive funds             *****/
/***********************************************************************************************************/
/***********************************************************************************************************/

using System;
using System.Net;
using System.Net.Sockets;

namespace syslogd_console;

public class SyslogData
{
    // Needed if using a document DB store
    public Guid id { get; set; } = Guid.NewGuid();

    public string time { get; set; }
    public string ipaddress { get; set; }
    public string syslogdata { get; set; }
}

class Syslogd
{
    static void Main(string[] args)
    {
        Syslog_listen();
    }

    /// <summary>
    /// Writes to a CSV file the data collected.
    /// </summary>
    /// <param name="date"></param>
    /// <param name="ipAddr"></param>
    /// <param name="strData"></param>
    static void WriteToFile(string date, string ipAddr, string strData)
    {
        SyslogData sd = new();
        sd.time = date;
        sd.ipaddress = ipAddr;
        sd.syslogdata = strData;

        string csv = date + "," + ipAddr + "," + strData + "\r\n";
        try
        {
            File.AppendAllText(@"syslog.csv", csv);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    /// <summary>
    /// Syslogd is mostly related to Unix environments.  
    /// It is the System Logging Daemon that can be configured to use either files, 
    /// or can transmit the information to an IP address running a logger.
    /// </summary>
    /// <remarks>This function opens UDP Port 514 (Syslogd) and listens for any traffic.</remarks>
    /// <param name="void">This function does not return anything, but instead writes to the Console device</param>
    static void Syslog_listen()
    {
        try
        {
            // Create the UDP Socket
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            // Define the IP End Point to accept any IP traffic on port 514 
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 514);
            EndPoint ep;

            // Not sure why this part works, but it does
            //ep = (EndPoint)iep;
            ep = iep;

            //Bind the port, afterall this should be the only application using 514 on
            //a Windows machine.
            s.Bind(ep);

            //Go into the infinite loop to capture the data sent
            while (true)
            {
                //Set the buffer size to 1024 bytes - usually more than enough.
                byte[] data = new byte[1024];
                //This is just a place holder, but can be used to determine the amount of data
                //being sent from the syslogd daemon
                int recv;

                // Let's sit an wait for the traffic to start
                recv = s.ReceiveFrom(data, ref ep);

                // Same with this.
                iep = (IPEndPoint)ep;
                // Convert the buffer into something more pallatable and useable by the end-user
                string strData = System.Text.Encoding.ASCII.GetString(data, 0, recv);
                // Display the Time, IP address and the syslogd message associated with it.
                string date = System.DateTime.Now.ToLongTimeString();
                string ipAddr = iep.Address.ToString();

                Console.WriteLine($"{date}:{ipAddr} : {strData}"); // System.DateTime.Now.ToLongTimeString() + ":" + iep.Address.ToString() + ":  " + strData);

                WriteToFile(date, ipAddr, strData);
            }
        }
        catch (Exception ex)
        {
            //If anything is broke - what caused it...
            Console.WriteLine(ex.Message);
        }
    }
}