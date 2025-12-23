using System.Configuration;
using System.Diagnostics;
using System.IO.Ports;
using System.Management;
using System.Text;
using static FlashEx.FileSizeFormat;

namespace FlashEx
{
    public partial class Form1 : Form
    {

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

        // Custom firmwares list
        string firmwareUrl = "";

        // Folder Path Defs
        private static readonly string AppExecPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";
        private static readonly string Firmwares = AppExecPath + @"firmwares\";
        private static readonly string Config = AppExecPath + @"config\";

        // App Defs
        private static readonly string esptool = AppExecPath + @"esptool\esptool.exe";

        // Configs
        private static readonly string firmware_urls = Config + @"firmware_urls.txt";

        // XML config file
        private readonly ExeConfigurationFileMap appConfigMap = new ExeConfigurationFileMap
        {
            ExeConfigFilename = Config + "appSettings.xml"
        };

        // Class for custom firmwares list
        public class DownloadItem
        {
            public string Name { get; set; } = "";
            public string Url { get; set; } = "";

            public override string ToString()
            {
                return Name;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // App and esptool version
            this.Text = "FlashEx v1.5 | esptool v5.1.0";

            // Get a list of serial port names
            GetAvailableSerialPorts();

            // Read tasmota firmwares from file
            foreach (var line in File.ReadAllLines(firmware_urls))
            {
                comboBoxTasmotaSource.Items.Add(new DownloadItem
                {
                    Url = line,
                    Name = Path.GetFileName(line) // e.g.: tasmota32.bin
                });
            }

            // Config file
            Configuration customConfig = ConfigurationManager.OpenMappedExeConfiguration(appConfigMap, ConfigurationUserLevel.None);
            AppSettingsSection appSettings = (customConfig.GetSection("appSettings") as AppSettingsSection);


            // Load - Last used Baud Rate
            if (appSettings.Settings["Saved_BaudRate"] != null)
            {
                comboBoxBaudRate.Text = appSettings.Settings["Saved_BaudRate"].Value;
            }
            else
            {
                // Default selected Baudrate
                comboBoxBaudRate.SelectedItem = "115200";
            }

            // Load - Last used BIN file
            if (appSettings.Settings["Saved_BinFileAndPath"] != null)
            {
                FirmwareFileWithPath = appSettings.Settings["Saved_BinFileAndPath"].Value;
                labelSelectedFWName.Text = Path.GetFileName(FirmwareFileWithPath);
            }

            // Default selected Partition
            comboBoxPartitionTable.SelectedItem = "0x0";


            // Debug default selected Baudrate
            comboBoxDebugBaudrate.SelectedItem = "115200";

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

            string WriteFirmware = $" --port {SelectedCOMPort} --baud {comboBoxBaudRate.Text} write-flash {comboBoxPartitionTable.Text} {FirmwareFileWithPath}";

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

            string EraseFlash = $" --port {SelectedCOMPort} erase-flash";

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

            string BackupFlash = $" --port {SelectedCOMPort} --baud {GetBaudRate} read-flash {FirmwarePartitionFrom} {FirmwarePartitionTo} {FirmwareBackupName}";

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

                string GetFlashId = $" --port {SelectedCOMPort} flash-id";

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

                string GetImageInfo = $" image-info {FirmwareFileWithPath}";

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

        // Set MQTT credentials for Tasmota devices
        private void iconButtonTasmotaSetMqtt_Click(object sender, EventArgs e)
        {
            SerialPortCommunication("SetTasmotaMQTT");
        }

        // Open tasmota firmware releases site
        private void linkLabelOpenTasmotaFirmwares_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLinkBrowser("https://ota.tasmota.com/tasmota/release");
        }

        private void linkLabelWifiMqttConfigSite1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLinkBrowser("https://tasmota.github.io/docs/Commands/#over-serial-bridge");
        }

        private void linkLabelWifiMqttConfigSite2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLinkBrowser("https://tasmota.github.io/docs/Commands/#over-serial-bridge");
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

            // Get firmware url
            if (comboBoxTasmotaSource.SelectedItem is DownloadItem item)
            {
                firmwareUrl = item.Url;
            }

            // UI info
            labelDownloadTasmotaBin.ForeColor = Color.DarkOrange;
            labelDownloadTasmotaBin.Text = "init...";

            // Slpit tasmota url by slash
            var TasmotaUrlArray = firmwareUrl.Split('/');

            // Count tasmota array
            var CntTasmotaArray = TasmotaUrlArray.Length;

            // Create final filename. -1 needed because 'Length' count from 1 not 0
            string savePath = Firmwares + (TasmotaUrlArray[CntTasmotaArray - 1]);

            // Track total downloaded bytes
            long totalBytesDownloaded = 0;

            using HttpClient httpClient = new();
            try
            {
                using (Stream contentStream = await httpClient.GetStreamAsync(firmwareUrl))
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
            SerialPortCommunication("initConnectionOnly");
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
                ActionStatus("WIFI is almost set! Check the logs for the IP | HINT: Use 'Disconnect' from 'Main' tab to close Serial communication.", "green");
            }


            // Set Tasmota MQTT credentials
            if (task == "SetTasmotaMQTT")
            {
                // Check MQTT Host is set
                if (textBoxMqttHost.Text == "")
                {
                    MessageBox.Show("Set MQTT Host...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Set MQTT User is set
                if (textBoxMqttUser.Text == "")
                {
                    MessageBox.Show("Set MQTT User...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Set MQTT Password is set
                if (textBoxMqttPasswd.Text == "")
                {
                    MessageBox.Show("Set MQTT Password...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Set MQTT Topic is set
                if (textBoxMqttCustomTopic.Text == "")
                {
                    MessageBox.Show("Set MQTT Topic...", "FlashEx", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Set exec params
                TaskCommand = $"Backlog MqttHost {textBoxMqttHost.Text}; MqttUser {textBoxMqttUser.Text}; MqttPassword {textBoxMqttPasswd.Text}; Topic {textBoxMqttCustomTopic.Text}; SetOption53 1; PowerRetain on";
                ActionStatus("HINT: Use 'Disconnect' from 'Main' tab to close Serial communication.", "green");
            }



            // Just connect to Serial Port
            else if (task == "initConnectionOnly")
            {
                iconButtonConnectToSerial.Enabled = false;
                ActionStatus($"{SelectedCOMPort} Connected!", "orange");
            }

            // Disable UI buttons
            DisableInputs();


            // comboBoxDebugBaudrate.Text
            int SelectedBaudRate;
            if (checkBoxDebugEnable.Checked == true)
            {
                SelectedBaudRate = Int32.Parse(comboBoxDebugBaudrate.Text);
            }
            else
            {
                SelectedBaudRate = Int32.Parse(comboBoxBaudRate.Text);
            }

            // Init Serial Communication
            BuiltInSerialPort = new SerialPort(SelectedCOMPort, SelectedBaudRate, Parity.None, 8, StopBits.One);

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

        // Clear terminal log
        private void iconButtonClearTerminal_Click(object sender, EventArgs e)
        {
            richTextBoxConsoleLog.Text = "";
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


            // Save process //

            // Save - Baud Rate
            saveSettings("Saved_BaudRate", comboBoxBaudRate.Text);

            // Save - BIN file name with Path
            saveSettings("Saved_BinFileAndPath", FirmwareFileWithPath);

        }


        // Open URL with browser
        private void OpenLinkBrowser(string url)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")
            {
                CreateNoWindow = true
            });
        }

        // Save settings function
        private void saveSettings(string key, string val)
        {

            Configuration customConfig = ConfigurationManager.OpenMappedExeConfiguration(appConfigMap, ConfigurationUserLevel.None);
            AppSettingsSection appSettings = (customConfig.GetSection("appSettings") as AppSettingsSection);

            // Add/Update keys and values in config file
            if (appSettings.Settings[key] == null)
            {
                appSettings.Settings.Add(key, val);
            }
            else
            {
                appSettings.Settings[key].Value = val;
            }

            customConfig.Save();
        }

        // DEBUG Connection
        private void checkBoxDebugEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDebugEnable.Checked == true)
            {
                checkBoxDebugEnable.ForeColor = Color.Red;
            }
            else
            {
                checkBoxDebugEnable.ForeColor = Color.Black;
            }
        }

        private void textBoxDebugConsoleInput_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                // Supress sound
                e.SuppressKeyPress = true;

                try
                {
                    BuiltInSerialPort.WriteLine($"{textBoxDebugConsoleInput.Text}\r");
                    textBoxDebugConsoleInput.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
