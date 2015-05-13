using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWebCamServer
{
    /// <summary>
    /// Encapsulates the communication from and to
    /// an Arduino Board
    /// </summary>
    class ArduinoBoard
    {
        /// <summary>
        /// Construction class with argument as Arduino Port
        /// </summary>
        public ArduinoBoard(string aPort)
        {
            this.arduinoPort = aPort;
        }

        /// <summary>
        /// Holds the data received from an Arduino Board.
        /// </summary>
        string serialDataBuffer = "";

        /// <summary>
        /// Gets the data retrieved from an Arduino Board.
        /// </summary>
        internal string getSerialDataBuffer()
        {
            return serialDataBuffer;
        }

        /// <summary>
        /// Interface for the Serial Port at which an Arduino Board
        /// is connected.
        /// </summary>
        SerialPort arduinoBoard = new SerialPort();

        string arduinoPort = "COM32";
        /// <summary>
        /// Raised when new data are received
        /// </summary>
        public event EventHandler arduinoDataReceived;

        /// <summary>
        /// Closes the connection to an Arduino Board.
        /// </summary>
        public void CloseArduinoConnection()
        {
            if (arduinoBoard.IsOpen)
            {
                arduinoBoard.Close();
                arduinoBoard.DataReceived -= arduinoBoard_DataReceived;
            }
        }

        /// <summary>
        /// Opens the connection to an Arduino board
        /// </summary>
        public void OpenArduinoConnection()
        {
            if (!arduinoBoard.IsOpen)
            {
                arduinoBoard.DataReceived += arduinoBoard_DataReceived;
                arduinoBoard.PortName = arduinoPort;
                arduinoBoard.BaudRate = 9600;

                arduinoBoard.Parity = Parity.None;
                arduinoBoard.DataBits = 8;
                arduinoBoard.StopBits = StopBits.One;

                arduinoBoard.DtrEnable = true;
                try
                {
                    arduinoBoard.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("The Serial Port is already open!");
            }
        }

        /// <summary>
        /// Sends the command to the Arduino board which triggers the board
        /// to send the weather data it has internally stored.
        /// </summary>
        public void sendDataToArduinoBoard(string serialData)
        {
            if (arduinoBoard.IsOpen)
            {
                arduinoBoard.Write(serialData);
            }
            else
            {
                Console.WriteLine("Can't send data if the serial Port is closed!");
            }
        }

        /// <summary>
        /// Reads data from the arduinoBoard serial port
        /// </summary>
        void arduinoBoard_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string data = arduinoBoard.ReadLine();
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadExisting();
            serialDataBuffer = data;
            var t = Task.Factory.StartNew(() =>
            {
                if (arduinoDataReceived != null)//If there is someone waiting for this event to be fired
                {
                    arduinoDataReceived(serialDataBuffer, new EventArgs()); //Fire the event, indicating that new Data received.
                }
            });
            t.Wait();

        }

    }
}
