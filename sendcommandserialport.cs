using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace serialportok
{
    public partial class MainForm : Form
    {
        private SerialPort serialPort = null;
        private bool relay1State = false;
        private Button relay1Button;
        private Label responseLabel;
        private Button closeButton;
        
        public MainForm()
        {
            InitializeComponent();
            SetupCustomControls();
            SetupSerialPort();
        }
        
        private void SetupCustomControls()
        {
            // Create controls
            relay1Button = new Button();
            responseLabel = new Label();
            closeButton = new Button();
            
            // Configure controls
            relay1Button.Text = "Relay 1";
            relay1Button.Location = new System.Drawing.Point(100, 30);
            relay1Button.Size = new System.Drawing.Size(100, 30);
            
            responseLabel.Text = "Serial Response:";
            responseLabel.Location = new System.Drawing.Point(20, 80);
            responseLabel.Size = new System.Drawing.Size(260, 40);
            responseLabel.AutoSize = false;
            
            closeButton.Text = "Close";
            closeButton.Location = new System.Drawing.Point(100, 120);
            closeButton.Size = new System.Drawing.Size(100, 30);

            // Add controls to form
            this.Controls.Add(relay1Button);
            this.Controls.Add(responseLabel);
            this.Controls.Add(closeButton);

            // Configure form
            this.Text = "Relay Control Panel";
            this.Size = new System.Drawing.Size(300, 200);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Add event handlers
            closeButton.Click += new EventHandler(CloseButton_Click);
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
        }
        
        private void SetupSerialPort()
        {
            try
            {
                string[] lines = File.ReadAllLines("serial.txt");
                int comPort = -1;
                
                foreach (string line in lines)
                {
                    if (line.StartsWith("com:"))
                    {
                        comPort = int.Parse(line.Split(':')[1]);
                        break;
                    }
                }
                
                if (comPort != -1)
                {
                    serialPort = new SerialPort(string.Format("COM{0}", comPort), 9600);
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                    serialPort.Open();

                    relay1Button.Click += new EventHandler(Relay1Button_Click);
                }
                else
                {
                    relay1Button.Enabled = false;
                    responseLabel.Text = "Invalid COM port configuration.\nPlease check serial.txt";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error reading COM port: {0}", ex.Message));
                relay1Button.Enabled = false;
                responseLabel.Text = "Error reading COM port configuration.\nPlease check serial.txt";
            }
        }
        
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string response = serialPort.ReadExisting();
            this.Invoke((MethodInvoker)delegate
            {
                responseLabel.Text = string.Format("Serial Response: {0}", response);
            });
        }
        
        private void Relay1Button_Click(object sender, EventArgs e)
        {
            relay1State = !relay1State;
            if (relay1State)
            {
                Console.WriteLine("Relay 1 activated");
                relay1Button.BackColor = Color.Red;
                serialPort.Write("so001");
                System.Threading.Thread.Sleep(300);
            }
            else
            {
                Console.WriteLine("Relay 1 deactivated");
                relay1Button.BackColor = SystemColors.Control;
                serialPort.Write("ro001");
                System.Threading.Thread.Sleep(300);
            }
        }
        
        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
            this.Close();
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }
}