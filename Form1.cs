using System.Diagnostics;
using System.IO.Ports;
using System.Management;
using System.Text;
using static FlashEx.FileSizeFormat;

namespace FlashEx
{
    public partial class Form1 : Form
    {

        // TODO: Button for reset device

        public Form1()
        {
            InitializeComponent();
        }

        // Variables
        private Process? Process;
        private StringBuilder? OutputBuffer;
        SerialPort BuiltInSerialPort = new();
        string FirmwareFileWithPath = "";
        string SelectedCOMPort = "";


        // Folder Path Defs
        private static readonly string AppExecPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";
        private static readonly string Firmwares = AppExecPath + @"firmwares\";
        private static readonly string Config = AppExecPath + @"config\";

        // App Defs
        private static readonly string esptool = AppExecPath + @"esptool\esptool.exe";

        // Configs
        private static readonly string firmware_urls = Config + @"firmware_urls.txt";

        private void Form1_Load(object sender, EventArgs e)
        {
            // App and esptool version
            this.Text = "FlashEx v1.2 | esptool v4.7.0";

            // Get a list of serial port names
            GetAvailableSerialPorts();

            // Read tasmota firmwares from file
            foreach (string line in File.ReadLines(firmware_urls))
            {
                comboBoxTasmotaSource.Items.Add(line);
            }

            // Default selected Baudrate
            comboBoxBaudRate.SelectedItem = "115200";

            // Default selected Partition
            comboBoxPartitionTable.SelectedItem = "0x0";

        }

        // Select Firmware
        private void iconButtonOpenBinFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog OpenFirmware = new()
            {
                InitialDirectory = Firmwares,
                Title = "Browse firmware Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "bin",
                Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*",

                FilterIndex = 1,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (OpenFirmware.ShowDialog() == DialogResult.OK)
            {
                FirmwareFileWithPath = OpenFirmware.FileName;
                labelSelectedFWName.Text = Path.GetFileName(OpenFirmware.FileName);
            }
        }


        // Write Firmware
        private async void buttonInitFlashProcess_Click(object sender, EventArgs e)
        {
            // Check settings
            if (!CheckSettings())
            {
                return;
            }

            ActionStatus("Flashing...", "orange");

            DisableInputs();

            string WriteFirmware = $" --port {SelectedCOMPort} --baud {comboBoxBaudRate.Text} write_flash {comboBoxPartitionTable.Text} {FirmwareFileWithPath}";

            await ExecuteCommand(WriteFirmware);

            EnableInputs();

            ActionStatus("Flashing Done!", "green");
        }



        // Erase Flash
        private async void iconButtonEraseFlash_Click(object sender, EventArgs e)
        {
            if (SelectedCOMPort == "")
            {
                MessageBox.Show("Select Serial Port...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DisableInputs();
            ActionStatus("Erase Flash...", "orange");

            string EraseFlash = $" --port {SelectedCOMPort} erase_flash";

            await ExecuteCommand(EraseFlash);

            EnableInputs();
            ActionStatus("Erase Done!", "green");

        }

        // Reset device state (run code on device)
        private async void iconButtonResetDevice_Click(object sender, EventArgs e)
        {
            if (SelectedCOMPort == "")
            {
                MessageBox.Show("Select Serial Port...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DisableInputs();
            ActionStatus("Reset device state...", "orange");

            string ResetDeviceState = $" --port {SelectedCOMPort} run";

            await ExecuteCommand(ResetDeviceState);

            EnableInputs();
            ActionStatus("Reset device state done!", "green");
        }

        private async void iconButtonSaveFirmware_Click(object sender, EventArgs e)
        {

            // Currently used variables
            string FirmwarePartitionFrom = comboBoxBackupFirmFrom.Text;
            string FirmwarePartitionTo = comboBoxBackupFirmTo.Text;
            string FirmwareBackupName = "";
            string GetBaudRate = comboBoxBaudRate.Text;

            // Check Partition "from" is set
            if (FirmwarePartitionFrom == "")
            {
                MessageBox.Show("Select 'Firmware Partition From'...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check Partition "to" is set
            if (FirmwarePartitionTo == "")
            {
                MessageBox.Show("Select 'Firmware Partition To'...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check Port is set
            if (SelectedCOMPort == "")
            {
                MessageBox.Show("Select Serial Port...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check BaudRate is set
            if (GetBaudRate == "")
            {
                MessageBox.Show("Select Baud Rate...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save file dialog
            SaveFileDialog saveFirmwareFile = new SaveFileDialog();
            saveFirmwareFile.Filter = "bin file|*.bin";
            saveFirmwareFile.Title = "Save bin file";
            saveFirmwareFile.ShowDialog();

            // Save path can not be emty
            if (saveFirmwareFile.FileName == "")
            {
                MessageBox.Show("Select Baud Rate...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Firmware path & filename
            FirmwareBackupName = saveFirmwareFile.FileName;

            DisableInputs();
            ActionStatus("Backup Flash (long process! 4MB ~6min)...", "orange");

            string BackupFlash = $" --port {SelectedCOMPort} --baud {GetBaudRate} read_flash {FirmwarePartitionFrom} {FirmwarePartitionTo} {FirmwareBackupName}";

            await ExecuteCommand(BackupFlash);

            EnableInputs();
            ActionStatus("Backup Done!", "green");

        }


        //
        //
        // TOOLS GROUP
        //
        //


        // Read ESP info
        private async void iconButtonReadESPInfo_Click(object sender, EventArgs e)
        {
            if (SelectedCOMPort != "")
            {
                DisableInputs();

                string GetFlashId = $" --port {SelectedCOMPort} flash_id";

                await ExecuteCommand(GetFlashId);

                EnableInputs();
            }
            else
            {
                MessageBox.Show("Select Serial Port...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Firmware file info
        private async void iconButtonImageInfo_Click(object sender, EventArgs e)
        {

            if (FirmwareFileWithPath != "")
            {
                DisableInputs();

                string GetImageInfo = $" image_info {FirmwareFileWithPath}";

                await ExecuteCommand(GetImageInfo);

                EnableInputs();
            }
            else
            {
                MessageBox.Show("Select Firmware File...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        //
        //
        // TASMOTA RELATED
        //
        //

        // Set WIFI credentials for Tasmota devices
        private void iconButtonTasmotaSetWifi_Click(object sender, EventArgs e)
        {
            SerialPortCommunication("SetTasmotaWifi");
        }

        // Open tasmota firmware releases site
        private void linkLabelOpenTasmotaFirmwares_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLinBrowser("https://ota.tasmota.com/tasmota/release");
        }

        // Download Tasmote Firmwares
        private async void iconButtonDownloadTasmotaBin_Click(object sender, EventArgs e)
        {

            // Check Tasmota url is selected
            if (comboBoxTasmotaSource.Text == "")
            {
                MessageBox.Show("Select Tasmota firmware...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // UI info
            labelDownloadTasmotaBin.ForeColor = Color.DarkOrange;
            labelDownloadTasmotaBin.Text = "init...";

            // Slpit tasmota url by slash
            var TasmotaUrlArray = comboBoxTasmotaSource.Text.Split('/');

            // Count tasmota array
            var CntTasmotaArray = TasmotaUrlArray.Length;

            // Create final filename. -1 needed because 'Length' count from 1 not 0
            string savePath = Firmwares + (TasmotaUrlArray[CntTasmotaArray - 1]);

            // Track total downloaded bytes
            long totalBytesDownloaded = 0;

            using HttpClient httpClient = new();
            try
            {
                using (Stream contentStream = await httpClient.GetStreamAsync(comboBoxTasmotaSource.Text))
                {

                    // Create a FileStream to save the downloaded file
                    using FileStream fileStream = new(savePath, FileMode.Create, FileAccess.Write);

                    // Buffer to store downloaded data
                    byte[] buffer = new byte[4096];

                    // Variable to store the number of bytes read in each iteration
                    int bytesRead = 0;

                    // Loop to read data from the response stream and write it to the FileStream
                    while ((bytesRead = await contentStream.ReadAsync(buffer)) > 0)
                    {

                        // Write file
                        await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));

                        // Update the total downloaded bytes
                        totalBytesDownloaded += bytesRead;

                        // Show Downloaded size
                        labelDownloadTasmotaBin.Text = FormatSize(totalBytesDownloaded);
                    }
                }

                labelDownloadTasmotaBin.ForeColor = Color.Green;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Failed to download: {ex.Message}", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        //
        //
        // Serial Port Communication
        //
        //

        // Connect to Serial Port
        private void iconButtonConnectToSerial_Click(object sender, EventArgs e)
        {
            SerialPortCommunication("");
        }

        // Disconnect to Serial Port
        private void iconButtonDisconnectFromCOM_Click(object sender, EventArgs e)
        {
            if (BuiltInSerialPort.IsOpen)
            {
                BuiltInSerialPort.Close();
            }

            EnableInputs();
            iconButtonConnectToSerial.Enabled = true;

            ActionStatus($"{SelectedCOMPort} Disconnected!", "black");
        }




        private async void SerialPortCommunication(string task)
        {
            // Set variables
            string TaskCommand = string.Empty;

            // Clear UI console log
            richTextBoxConsoleLog.Clear();

            // Check COM port is set
            if (comboBoxComPorts.Text == "")
            {
                MessageBox.Show("Set COM Port...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check Baudrate is set
            if (comboBoxBaudRate.Text == "")
            {
                MessageBox.Show("Set Baudrate...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //
            // Task Switcher
            //

            // Set Tasmota Wifi credentials
            if (task == "SetTasmotaWifi")
            {
                // Check SSID is set
                if (textBoxTasmotaWifiSSID.Text == "")
                {
                    MessageBox.Show("Set SSID...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Set Wifi Password is set
                if (textBoxTasmotaWifiPassword.Text == "")
                {
                    MessageBox.Show("Set Wifi Password...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Set exec params
                TaskCommand = $"Backlog SSID1 {textBoxTasmotaWifiSSID.Text}; Password1 {textBoxTasmotaWifiPassword.Text};";
                ActionStatus("WIFI is almost set! Check the logs for the IP | FIY: Use 'Disconnect' from 'Main' tab to close Serial communication.", "green");
            }



            // Just connect to Serial Port
            else if (task == "")
            {
                iconButtonConnectToSerial.Enabled = false;
                ActionStatus($"{SelectedCOMPort} Connected!", "orange");
            }

            // Disable UI buttons
            DisableInputs();

            // Init Serial Communication
            BuiltInSerialPort = new SerialPort(SelectedCOMPort, Int32.Parse(comboBoxBaudRate.Text), Parity.None, 8, StopBits.One);

            // Open Serial Communication
            BuiltInSerialPort.Open();

            // Read Serial Port main task
            await Task.Run(() =>
            {
                // Monitoring Serial Port if 'task' empty
                if (task != "")
                {
                    BuiltInSerialPort.WriteLine($"{TaskCommand}\r");
                }

                while (true)
                {
                    try
                    {

                        // Read Serial Port communication
                        var readData = BuiltInSerialPort.ReadLine();

                        //f (readData.Equals("val"))
                        // {
                        //
                        // }

                        void act1() => richTextBoxConsoleLog.AppendText(readData);
                        richTextBoxConsoleLog.Invoke(act1);

                        void act2() => richTextBoxConsoleLog.SelectionStart = richTextBoxConsoleLog.Text.Length;
                        richTextBoxConsoleLog.Invoke(act2);

                        void act3() => richTextBoxConsoleLog.ScrollToCaret();
                        richTextBoxConsoleLog.Invoke(act3);

                    }
                    catch (OperationCanceledException)
                    {
                        // Break while if connection closed
                        break;
                    }
                }
            });
        }

        // Get a list of serial port names
        private void iconButtonComPortRefresh_Click(object sender, EventArgs e)
        {
            GetAvailableSerialPorts();
        }

        // Set Serial Port variable
        private void comboBoxComPorts_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedCOMPort = comboBoxComPorts.Text.Split("-")[0].Replace(" ", "");
        }

        // Make Baudrate input writeable
        private void checkBoxCustomBaudRateBool_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxCustomBaudRateBool.Checked)
            {
                comboBoxBaudRate.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                comboBoxBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        // Make Partition input writeable
        private void checkBoxCustomPartitionTableBool_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxCustomPartitionTableBool.Checked)
            {
                comboBoxPartitionTable.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                comboBoxPartitionTable.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        // Make Save Partition 'From' input writeable
        private void checkBoxCustomSavePartFrom_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxCustomSavePartFrom.Checked)
            {
                comboBoxBackupFirmFrom.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                comboBoxBackupFirmFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        // Make Save Partition 'To' input writeable
        private void checkBoxCustomSavePartTo_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxCustomSavePartTo.Checked)
            {
                comboBoxBackupFirmTo.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                comboBoxBackupFirmTo.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }


        private void iconButtonOpenEspDocsDatabase_Click(object sender, EventArgs e)
        {
            Database db = new();
            db.Show();
        }

        //
        //
        // FUNCTIONS
        //
        //

        // Status function for UI info/status
        private void ActionStatus(string text, string color)
        {
            if (color == "red")
            {
                toolStripStatusLabelActionStatus.ForeColor = Color.Red;
            }
            else if (color == "green")
            {
                toolStripStatusLabelActionStatus.ForeColor = Color.Green;
            }
            else if (color == "orange")
            {
                toolStripStatusLabelActionStatus.ForeColor = Color.DarkOrange;
            }
            else
            {
                toolStripStatusLabelActionStatus.ForeColor = Color.Black;
            }

            toolStripStatusLabelActionStatus.Text = text;
        }


        // Settings check before write frimware to flash
        private bool CheckSettings()
        {
            if (comboBoxComPorts.Text == "")
            {
                MessageBox.Show("Set COM Port...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (comboBoxBaudRate.Text == "")
            {
                MessageBox.Show("Set Baudrate...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (comboBoxPartitionTable.Text == "")
            {
                MessageBox.Show("Set Partition...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (FirmwareFileWithPath == "")
            {
                MessageBox.Show("Select Firmware...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }


        }

        // Disable UI elements
        private void DisableInputs()
        {
            iconButtonConnectToSerial.Enabled = false;
            iconButtonEraseFlash.Enabled = false;
            iconButtonTasmotaSetWifi.Enabled = false;
            buttonInitFlashProcess.Enabled = false;
            iconButtonComPortRefresh.Enabled = false;
            iconButtonOpenBinFile.Enabled = false;
            comboBoxComPorts.Enabled = false;
            comboBoxBaudRate.Enabled = false;
            comboBoxPartitionTable.Enabled = false;
            comboBoxBackupFirmFrom.Enabled = false;
            comboBoxBackupFirmTo.Enabled = false;
            iconButtonSaveFirmware.Enabled = false;
            iconButtonResetDevice.Enabled = false;
        }

        // Enable UI elements
        private void EnableInputs()
        {
            iconButtonConnectToSerial.Enabled = true;
            iconButtonEraseFlash.Enabled = true;
            iconButtonTasmotaSetWifi.Enabled = true;
            buttonInitFlashProcess.Enabled = true;
            iconButtonComPortRefresh.Enabled = true;
            iconButtonOpenBinFile.Enabled = true;
            comboBoxComPorts.Enabled = true;
            comboBoxBaudRate.Enabled = true;
            comboBoxPartitionTable.Enabled = true;
            comboBoxBackupFirmFrom.Enabled = true;
            comboBoxBackupFirmTo.Enabled = true;
            iconButtonSaveFirmware.Enabled = true;
            iconButtonResetDevice.Enabled = true;
        }


        // Collect all available Serial Ports
        private void GetAvailableSerialPorts()
        {
            // Clear combobox
            comboBoxComPorts.Items.Clear();

            // Get a list of serial port names
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                string description = GetDescriptionFromPortName(port);
                comboBoxComPorts.Items.Add($"{port} - {description}");
            }
        }

        // Get more info about Serial Ports
        private string GetDescriptionFromPortName(string portName)
        {
            string query = $"SELECT * FROM Win32_PnPEntity WHERE Caption like '%{portName}%'";
            ManagementObjectSearcher searcher = new(query);
            ManagementObjectCollection ports = searcher.Get();

            foreach (ManagementObject port in ports.Cast<ManagementObject>())
            {
                // Retrieve the description property
                string? description = port["Description"]?.ToString();
                if (!string.IsNullOrEmpty(description))
                {
                    return description;
                }
            }
            return "Unknown";
        }


        // Main command executer for esptool command line tool
        private async Task ExecuteCommand(string command)
        {
            // New StringBuilder
            OutputBuffer = new StringBuilder();

            // Clear UI console log
            OutputBuffer.Clear();

            // Create process info
            var proc_info = new ProcessStartInfo
            {
                // App name
                FileName = esptool,

                // Command
                Arguments = " " + command,

                // Redirect standard output and error
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8
            };

            // Initialize process
            Process = new Process
            {
                StartInfo = proc_info
            };

            // Event handler for receiving data from the process
            Process.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    // Append output to buffer
                    OutputBuffer.AppendLine(e.Data);
                    UpdateOutput();
                }
            };

            // Start process
            Process.Start();

            // Begin asynchronous read operation on standard output
            Process.BeginOutputReadLine();

            // Wait for process to exit
            await Task.Run(() => Process.WaitForExit());
        }

        // Update incoming data to UI console log window
        private void UpdateOutput()
        {
            // Display the output in txtOutput
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateOutput));
            }
            else
            {
                richTextBoxConsoleLog.Text = OutputBuffer.ToString();
                richTextBoxConsoleLog.SelectionStart = richTextBoxConsoleLog.Text.Length;
                richTextBoxConsoleLog.ScrollToCaret();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean process
            if (Process != null && !Process.HasExited)
            {
                Process.Kill();
                Process.Dispose();
            }

            // Close Serial Port if open
            if (BuiltInSerialPort.IsOpen)
            {
                BuiltInSerialPort.Close();
            }

        }


        // Open URL with browser
        private void OpenLinBrowser(string url)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")
            {
                CreateNoWindow = true
            });
        }


    }
}
