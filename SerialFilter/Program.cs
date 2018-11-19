using System;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;

class PortDataReceived
{
    public static void Main()
    {
        string ExitCondition = "";
        SerialPort InputPort = new SerialPort("COM1");
        SerialPort OutputPort = new SerialPort("COM20");

        InputPort.BaudRate = 115200;
        InputPort.Parity = Parity.None;
        InputPort.StopBits = StopBits.One;
        InputPort.DataBits = 8;
        InputPort.Handshake = Handshake.None;
        InputPort.RtsEnable = true;

        OutputPort.BaudRate = 9600;
        OutputPort.Parity = Parity.None;
        OutputPort.StopBits = StopBits.One;
        OutputPort.DataBits = 8;
        OutputPort.Handshake = Handshake.None;
        OutputPort.RtsEnable = true;


        InputPort.Open();
        OutputPort.Open();

        decimal validator = 1;
        while (ExitCondition == "")
        {
            Thread.Sleep(1000);
            Console.Clear();
            string indata = InputPort.ReadLine();
            string Weight = Regex.Match(indata, @"(\d+\.\d+)").Value;
            
            try
                { decimal.TryParse(Weight, out validator); }
            catch
               { validator = -1;  }
            Console.WriteLine(validator);
            if (validator >= 0)
            {
                try
                {
                    OutputPort.Write("__" + validator.ToString() + "  LB");
                    OutputPort.Write(new byte[] { 13, 10 }, 0, 2);
                    
                }
                catch
                { }
            }

        }
        InputPort.Close();
        OutputPort.Close();
    }

}