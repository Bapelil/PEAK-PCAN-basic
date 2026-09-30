using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/// <summary>
/// Inclusion of PEAK PCAN-Basic namespace
/// </summary>
using Peak.Can.Basic;
using TPCANHandle = System.UInt16;
using TPCANBitrateFD = System.String;
using TPCANTimestampFD = System.UInt64;
using System.Linq;
using System.Diagnostics;


namespace ICDIBasic
{
    public partial class Form1 : Form
    {
        #region MinMax Control
        private Dictionary<TPCANBitrateFD, double> minCollection;
        private Dictionary<TPCANBitrateFD, double> maxCollection;
        private Dictionary<TPCANBitrateFD, double> lastTime;
        #endregion

        #region Fields
        private DateTime? firstTime;
        #endregion

        #region Structures
        /// <summary>
        /// Message Status structure used to show CAN Messages
        /// in a ListView
        /// </summary>
        private class MessageStatus
        {
            private TPCANMsgFD m_Msg;
            private TPCANTimestampFD m_TimeStamp;
            private TPCANTimestampFD m_oldTimeStamp;
            private int m_iIndex;
            private int m_Count;
            private bool m_bShowPeriod;
            private bool m_bWasChanged;
            private int count;
            internal string TimeStamp;

            public MessageStatus(TPCANMsgFD canMsg, TPCANTimestampFD canTimestamp, int listIndex)
            {
                m_Msg = canMsg;
                m_TimeStamp = canTimestamp;
                m_oldTimeStamp = canTimestamp;
                m_iIndex = listIndex;
                m_Count = 1;
                m_bShowPeriod = true;
                m_bWasChanged = false;
            }

            public MessageStatus(TPCANMsgFD canMsg, ulong canTimestamp, int listIndex, int count) : this(canMsg, canTimestamp, listIndex)
            {
                this.count = count;
            }

            public void Update(TPCANMsgFD canMsg, TPCANTimestampFD canTimestamp)
            {
                m_Msg = canMsg;
                m_oldTimeStamp = m_TimeStamp;
                m_TimeStamp = canTimestamp;
                //var a = TimeSpan.FromMilliseconds(Environment.TickCount);
                //var spanNow = DateTime.Now.TimeOfDay;
                //var lastTime = spanNow.Subtract(a).Add(TimeSpan.FromMilliseconds((canTimestamp / 1000d)));
                //Debug.WriteLine($"Last: {lastTime} Now: {spanNow}");
                m_bWasChanged = true;
                m_Count += 1;
            }

            public TPCANMsgFD CANMsg
            {
                get { return m_Msg; }
            }

            public TPCANTimestampFD Timestamp
            {
                get { return m_TimeStamp; }
            }

            public int Position
            {
                get { return m_iIndex; }
            }

            public string TypeString
            {
                get { return GetMsgTypeString(); }
            }

            public string IdString
            {
                get { return GetIdString(); }
            }

            public string DataString
            {
                get { return GetDataString(); }
            }

            public int Count
            {
                get { return m_Count; }
            }

            public bool ShowingPeriod
            {
                get { return m_bShowPeriod; }
                set
                {
                    if (m_bShowPeriod ^ value)
                    {
                        m_bShowPeriod = value;
                        m_bWasChanged = true;
                    }
                }
            }

            public bool MarkedAsUpdated
            {
                get { return m_bWasChanged; }
                set { m_bWasChanged = value; }
            }

            public string TimeString
            {
                get { return GetTimeString(); }
            }

            private string GetTimeString()
            {
                double fTime;

                fTime = (m_TimeStamp / 1000.0);
                if (m_bShowPeriod)
                    fTime -= (m_oldTimeStamp / 1000.0);
                return fTime.ToString("F1");
            }

            private string GetDataString()
            {
                string strTemp;

                strTemp = "";

                if ((m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_RTR) == TPCANMessageType.PCAN_MESSAGE_RTR)
                    return "Remote Request";
                else
                    for (int i = 0; i < Form1.GetLengthFromDLC(m_Msg.DLC, (m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_FD) == 0); i++)
                        strTemp += string.Format("{0:X2} ", m_Msg.DATA[i]);

                return strTemp;
            }

            private string GetIdString()
            {
                // We format the ID of the message and show it
                //
                if ((m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_EXTENDED) == TPCANMessageType.PCAN_MESSAGE_EXTENDED)
                    return string.Format("{0:X8}h", m_Msg.ID);
                else
                    return string.Format("{0:X3}h", m_Msg.ID);
            }

            private string GetMsgTypeString()
            {
                string strTemp;
                bool isEcho = (m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_ECHO) == TPCANMessageType.PCAN_MESSAGE_ECHO;

                if ((m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_STATUS) == TPCANMessageType.PCAN_MESSAGE_STATUS)
                    return "STATUS";

                if ((m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_ERRFRAME) == TPCANMessageType.PCAN_MESSAGE_ERRFRAME)
                    return "ERROR";

                if ((m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_EXTENDED) == TPCANMessageType.PCAN_MESSAGE_EXTENDED)
                    strTemp = "EXT";
                else
                    strTemp = "STD";

                if ((m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_RTR) == TPCANMessageType.PCAN_MESSAGE_RTR)
                    strTemp += isEcho ? "/RTR [ ECHO ]" : "/RTR";
                else
                {
                    if ((int)m_Msg.MSGTYPE > (int)TPCANMessageType.PCAN_MESSAGE_EXTENDED)
                    {
                        if (isEcho)
                            strTemp += " [ ECHO";
                        else
                            strTemp += " [ ";
                        if ((m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_FD) == TPCANMessageType.PCAN_MESSAGE_FD)
                            strTemp += " FD";
                        if ((m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_BRS) == TPCANMessageType.PCAN_MESSAGE_BRS)
                            strTemp += " BRS";
                        if ((m_Msg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_ESI) == TPCANMessageType.PCAN_MESSAGE_ESI)
                            strTemp += " ESI";
                        strTemp += " ]";
                    }
                }

                return strTemp;
            }

        }
        #endregion

        #region Delegates
        /// <summary>
        /// Read-Delegate Handler
        /// </summary>
        private delegate void ReadDelegateHandler();
        #endregion

        #region Members
        /// <summary>
        /// Saves the desired connection mode
        /// </summary>
        private bool m_IsFD;
        /// <summary>
        /// Saves the handle of a PCAN hardware
        /// </summary>
        private TPCANHandle m_PcanHandle;
        /// <summary>
        /// Saves the baudrate register for a conenction
        /// </summary>
        private TPCANBaudrate m_Baudrate;
        /// <summary>
        /// Saves the type of a non-plug-and-play hardware
        /// </summary>
        private TPCANType m_HwType;
        /// <summary>
        /// Stores the status of received messages for its display
        /// </summary>
        private System.Collections.ArrayList m_LastMsgsList;
        /// <summary>
        /// Read Delegate for calling the function "ReadMessages"
        /// </summary>
        private ReadDelegateHandler m_ReadDelegate;
        /// <summary>
        /// Receive-Event
        /// </summary>
        private System.Threading.AutoResetEvent m_ReceiveEvent;
        /// <summary>
        /// Thread for message reading (using events)
        /// </summary>
        private System.Threading.Thread m_ReadThread;
        /// <summary>
        /// Handles of non plug and play PCAN-Hardware
        /// </summary>
        private TPCANHandle[] m_NonPnPHandles;
        #endregion

        #region Methods
        #region Help functions
        /// <summary>
        /// Convert a CAN DLC value into the actual data length of the CAN/CAN-FD frame.
        /// </summary>
        /// <param name="dlc">A value between 0 and 15 (CAN and FD DLC range)</param>
        /// <param name="isSTD">A value indicating if the msg is a standard CAN (FD Flag not checked)</param>
        /// <returns>The length represented by the DLC</returns>
        public static int GetLengthFromDLC(int dlc, bool isSTD)
        {
            if (dlc <= 8)
                return dlc;

            if (isSTD)
                return 8;

            switch (dlc)
            {
                case 9: return 12;
                case 10: return 16;
                case 11: return 20;
                case 12: return 24;
                case 13: return 32;
                case 14: return 48;
                case 15: return 64;
                default: return dlc;
            }
        }

        /// <summary>
        /// Initialization of PCAN-Basic components
        /// </summary>
        private void InitializeBasicComponents()
        {
            // Creates the list for received messages
            //
            m_LastMsgsList = new System.Collections.ArrayList();
            // Creates the delegate used for message reading
            //
            m_ReadDelegate = new ReadDelegateHandler(ReadMessages);
            // Creates the event used for signalize incomming messages 
            //
            m_ReceiveEvent = new System.Threading.AutoResetEvent(false);
            // Creates an array with all possible non plug-and-play PCAN-Channels
            //
            m_NonPnPHandles = new TPCANHandle[]
            {
                PCANBasic.PCAN_ISABUS1,
                PCANBasic.PCAN_ISABUS2,
                PCANBasic.PCAN_ISABUS3,
                PCANBasic.PCAN_ISABUS4,
                PCANBasic.PCAN_ISABUS5,
                PCANBasic.PCAN_ISABUS6,
                PCANBasic.PCAN_ISABUS7,
                PCANBasic.PCAN_ISABUS8,
                PCANBasic.PCAN_DNGBUS1
            };

            // Fills and configures the Data of several comboBox components
            //
            FillComboBoxData();

            // Prepares the PCAN-Basic's debug-Log file
            //
            ConfigureLogFile();
        }

        /// <summary>
        /// Configures the Debug-Log file of PCAN-Basic
        /// </summary>
        private void ConfigureLogFile()
        {
            UInt32 iBuffer;

            // Sets the mask to catch all events
            //
            iBuffer = PCANBasic.LOG_FUNCTION_ALL;

            // Configures the log file. 
            // NOTE: The Log capability is to be used with the NONEBUS Handle. Other handle than this will 
            // cause the function fail.
            //
            PCANBasic.SetValue(PCANBasic.PCAN_NONEBUS, TPCANParameter.PCAN_LOG_CONFIGURE, ref iBuffer, sizeof(UInt32));
        }

        /// <summary>
        /// Configures the PCAN-Trace file for a PCAN-Basic Channel
        /// </summary>
        private void ConfigureTraceFile()
        {
            UInt32 iBuffer;
            TPCANStatus stsResult;

            // Configure the maximum size of a trace file to 5 megabytes
            //
            iBuffer = 5;
            stsResult = PCANBasic.SetValue(m_PcanHandle, TPCANParameter.PCAN_TRACE_SIZE, ref iBuffer, sizeof(UInt32));
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                IncludeTextMessage(GetFormatedError(stsResult));

            // Configure the way how trace files are created: 
            // * Standard name is used
            // * Existing file is ovewritten, 
            // * Only one file is created.
            // * Recording stopts when the file size reaches 5 megabytes.
            //
            iBuffer = PCANBasic.TRACE_FILE_SINGLE | PCANBasic.TRACE_FILE_OVERWRITE;
            stsResult = PCANBasic.SetValue(m_PcanHandle, TPCANParameter.PCAN_TRACE_CONFIGURE, ref iBuffer, sizeof(UInt32));
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                IncludeTextMessage(GetFormatedError(stsResult));
        }

        /// <summary>
        /// Help Function used to get an error as text
        /// </summary>
        /// <param name="error">Error code to be translated</param>
        /// <returns>A text with the translated error</returns>
        private string GetFormatedError(TPCANStatus error)
        {
            StringBuilder strTemp;

            // Creates a buffer big enough for a error-text
            //
            strTemp = new StringBuilder(256);
            // Gets the text using the GetErrorText API function
            // If the function success, the translated error is returned. If it fails,
            // a text describing the current error is returned.
            //
            if (PCANBasic.GetErrorText(error, 0, strTemp) != TPCANStatus.PCAN_ERROR_OK)
                return string.Format("An error occurred. Error-code's text (0x{0:X}) couldn't be retrieved", error);
            else
                return strTemp.ToString();
        }

        /// <summary>
        /// Includes a new line of text into the information Listview
        /// </summary>
        /// <param name="strMsg">Text to be included</param>
        private void IncludeTextMessage(string strMsg)
        {
            lbxInfo.Items.Add(strMsg);
            lbxInfo.SelectedIndex = lbxInfo.Items.Count - 1;
        }

        /// <summary>
        /// Gets the current status of the PCAN-Basic message filter
        /// </summary>
        /// <param name="status">Buffer to retrieve the filter status</param>
        /// <returns>If calling the function was successfull or not</returns>
        private bool GetFilterStatus(out uint status)
        {
            TPCANStatus stsResult;

            // Tries to get the sttaus of the filter for the current connected hardware
            //
            stsResult = PCANBasic.GetValue(m_PcanHandle, TPCANParameter.PCAN_MESSAGE_FILTER, out status, sizeof(UInt32));

            // If it fails, a error message is shown
            //
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
            {
                MessageBox.Show(GetFormatedError(stsResult));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Configures the data of all ComboBox components of the main-form
        /// </summary>
        private void FillComboBoxData()
        {
            // Channels will be check
            //
            btnHwRefresh_Click(this, new EventArgs());

            // FD Bitrate: 
            //      Arbitration: 1 Mbit/sec 
            //      Data: 2 Mbit/sec
            //
            txtBitrate.Text = "f_clock_mhz=20, nom_brp=5, nom_tseg1=2, nom_tseg2=1, nom_sjw=1, data_brp=2, data_tseg1=3, data_tseg2=1, data_sjw=1";

            // Baudrates 
            //
            cbbBaudrates.SelectedIndex = 3; // 250 K

            // Hardware Type for no plugAndplay hardware
            //
            cbbHwType.SelectedIndex = 0;

            // Interrupt for no plugAndplay hardware
            //
            cbbInterrupt.SelectedIndex = 0;

            // IO Port for no plugAndplay hardware
            //
            cbbIO.SelectedIndex = 0;

        }

        /// <summary>
        /// Activates/deaactivates the different controls of the main-form according
        /// with the current connection status
        /// </summary>
        /// <param name="bConnected">Current status. True if connected, false otherwise</param>
        private void SetConnectionStatus(bool bConnected)
        {
            // Buttons
            //
            btnInit.Enabled = !bConnected;
            btnRelease.Enabled = bConnected;
            btnGetVersions.Enabled = bConnected;
            btnHwRefresh.Enabled = !bConnected;
            btnStatus.Enabled = bConnected;
            btnReset.Enabled = bConnected;

            // ComboBoxs
            //
            cbbChannel.Enabled = !bConnected;
            cbbBaudrates.Enabled = !bConnected;
            cbbHwType.Enabled = !bConnected;
            cbbIO.Enabled = !bConnected;
            cbbInterrupt.Enabled = !bConnected;

            // Check-Buttons
            //
            chbCanFD.Enabled = !bConnected;

            // Hardware configuration and read mode
            //
            if (!bConnected)
                cbbChannel_SelectedIndexChanged(this, new EventArgs());
            else
                rdbTimer_CheckedChanged(this, new EventArgs());

            if (bConnected)
            {
                maxCollection.Clear();
                minCollection.Clear();
                lastTime.Clear();
            }

            // Display messages in grid
            //
            tmrDisplay.Enabled = bConnected;
            tmrCalculateDelay.Enabled = bConnected;
        }

        /// <summary>
        /// Gets the formated text for a PCAN-Basic channel handle
        /// </summary>
        /// <param name="handle">PCAN-Basic Handle to format</param>
        /// <param name="isFD">If the channel is FD capable</param>
        /// <returns>The formatted text for a channel</returns>
        private string FormatChannelName(TPCANHandle handle, bool isFD)
        {
            TPCANDevice devDevice;
            byte byChannel;

            // Gets the owner device and channel for a 
            // PCAN-Basic handle
            //
            if (handle < 0x100)
            {
                devDevice = (TPCANDevice)(handle >> 4);
                byChannel = (byte)(handle & 0xF);
            }
            else
            {
                devDevice = (TPCANDevice)(handle >> 8);
                byChannel = (byte)(handle & 0xFF);
            }

            // Constructs the PCAN-Basic Channel name and return it
            //
            if (isFD)
                return string.Format("{0}:FD {1} ({2:X2}h)", devDevice, byChannel, handle);
            else
                return string.Format("{0} {1} ({2:X2}h)", devDevice, byChannel, handle);
        }

        /// <summary>
        /// Gets the formated text for a PCAN-Basic channel handle
        /// </summary>
        /// <param name="handle">PCAN-Basic Handle to format</param>
        /// <returns>The formatted text for a channel</returns>
        private string FormatChannelName(TPCANHandle handle)
        {
            return FormatChannelName(handle, false);
        }
        #endregion

        #region Message-proccessing functions
        /// <summary>
        /// Display CAN messages in the Message-ListView
        /// </summary>
        private void DisplayMessages()
        {
            ListViewItem lviCurrentItem;
            lock (m_LastMsgsList.SyncRoot)
            {
                foreach (MessageStatus msgStatus in m_LastMsgsList)
                {
                    // Get the data to actualize
                    //
                    if (msgStatus.MarkedAsUpdated)
                    {
                        msgStatus.MarkedAsUpdated = false;


                        if (float.TryParse(msgStatus.TimeString.ToString(), out var time))
                        {
                            if (minCollection.ContainsKey(msgStatus.IdString))
                                minCollection[msgStatus.IdString] = Math.Min(minCollection[msgStatus.IdString], time);

                            if (maxCollection.ContainsKey(msgStatus.IdString))
                                maxCollection[msgStatus.IdString] = Math.Max(maxCollection[msgStatus.IdString], time);
                        }
                        //var difference = (lastTime[msgStatus.IdString] - msgStatus.Timestamp) / 1_000d;
                        //minCollection[msgStatus.IdString] = Math.Min(minCollection[msgStatus.IdString], difference);

                        if (!lastTime.ContainsKey(msgStatus.IdString))
                        {
                            lastTime.Add(msgStatus.IdString, msgStatus.Timestamp);
                        }
                        else
                        {
                            lastTime[msgStatus.IdString] = msgStatus.Timestamp;
                            //Debug.WriteLine("Display Updated");
                        }

                        int itemCount = listMinMax.Items.Count;
                        if (msgStatus.Position < listMinMax.Items.Count)
                        {
                            var minMaxItem = listMinMax.Items[msgStatus.Position];
                            minMaxItem.SubItems[2].Text = minCollection[msgStatus.IdString] == double.MaxValue
                            ? string.Empty
                            : minCollection[msgStatus.IdString].ToString("0.0");
                            //minMaxItem.SubItems[4].Text = maxCollection[msgStatus.IdString].ToString();
                            minMaxItem.SubItems[5].Text = msgStatus.DataString;
                            minMaxItem.SubItems[4].Text = msgStatus.Count.ToString();
                        }




                        var traceItems = listTrace.Items.Add(msgStatus.DataString);
                        var a = TimeSpan.FromMilliseconds(Environment.TickCount);
                        var timeStamp = DateTime.Now.Subtract(a).AddMilliseconds(msgStatus.Timestamp / 1000d).TimeOfDay;

                        if (firstTime.HasValue)
                        {
                            var firstTime = ((DateTime)this.firstTime).TimeOfDay;
                            timeStamp -= firstTime; traceItems.SubItems.Add($"{timeStamp.TotalSeconds}");
                            listTrace.EnsureVisible(listTrace.Items.Count - 1);
                            listTrace.Items.Add(msgStatus.IdString);


                        }

                        //UpdateTraceList(theMsg, itsTimeStamp);
                    }
                }
            }
        }


        /// <summary>
        /// Inserts a new entry for a new message in the Message-ListView
        /// </summary>
        /// <param name="newMsg">The messasge to be inserted</param>
        /// <param name="timeStamp">The Timesamp of the new message</param>
        private void InsertMsgEntry(TPCANMsgFD newMsg, TPCANTimestampFD timeStamp)
        {
            MessageStatus msgStsCurrentMsg;
            ListViewItem lviCurrentItem;

            lock (m_LastMsgsList.SyncRoot)
            {
                // We add this status in the last message list
                //
                msgStsCurrentMsg = new MessageStatus(newMsg, timeStamp, listMinMax.Items.Count);

                m_LastMsgsList.Add(msgStsCurrentMsg);

                // Add the new ListView Item with the Type of the message
                //	



                if (!minCollection.ContainsKey(msgStsCurrentMsg.IdString))
                    minCollection.Add(msgStsCurrentMsg.IdString, double.MaxValue);


                if (!maxCollection.ContainsKey(msgStsCurrentMsg.IdString)
                    && double.TryParse(msgStsCurrentMsg.TimeString.ToString(), out var maxTime))
                    maxCollection.Add(msgStsCurrentMsg.IdString, maxTime);

                var minMaxItem = listMinMax.Items.Add(msgStsCurrentMsg.TypeString);
                minMaxItem.SubItems.Add(msgStsCurrentMsg.IdString);
                minMaxItem.SubItems.Add(msgStsCurrentMsg.Count.ToString());
                minMaxItem.SubItems.Add(minCollection[msgStsCurrentMsg.IdString] == double.MaxValue ? string.Empty : minCollection[msgStsCurrentMsg.IdString].ToString());
                minMaxItem.SubItems.Add(maxCollection[msgStsCurrentMsg.IdString].ToString());
                minMaxItem.SubItems.Add(msgStsCurrentMsg.DataString);

            }
        }
        /// <summary>
        /// Processes a received message, in order to show it in the Message-ListView
        /// </summary>
        /// <param name="theMsg">The received PCAN-Basic message</param>
        /// <returns>True if the message must be created, false if it must be modified</returns>
        private void ProcessMessage(TPCANMsgFD theMsg, TPCANTimestampFD itsTimeStamp)
        {
            // We search if a message (Same ID and Type) is 
            // already received or if this is a new message
            //
            lock (m_LastMsgsList.SyncRoot)
            {
                foreach (MessageStatus msg in m_LastMsgsList)
                {
                    if ((msg.CANMsg.ID == theMsg.ID) && (msg.CANMsg.MSGTYPE == theMsg.MSGTYPE))
                    {
                        // Modify the message and exit
                        //
                        msg.Update(theMsg, itsTimeStamp);
                        return;
                    }
                }
                // Message not found. It will created
                //
                InsertMsgEntry(theMsg, itsTimeStamp);
            }
        }

        /// <summary>
        /// Processes a received message, in order to show it in the Message-ListView
        /// </summary>
        /// <param name="theMsg">The received PCAN-Basic message</param>
        /// <returns>True if the message must be created, false if it must be modified</returns>
        private void ProcessMessage(TPCANMsg theMsg, TPCANTimestamp itsTimeStamp)
        {
            TPCANMsgFD newMsg;
            TPCANTimestampFD newTimestamp;

            newMsg = new TPCANMsgFD();
            newMsg.DATA = new byte[64];
            newMsg.ID = theMsg.ID;
            newMsg.DLC = theMsg.LEN;
            for (int i = 0; i < ((theMsg.LEN > 8) ? 8 : theMsg.LEN); i++)
                newMsg.DATA[i] = theMsg.DATA[i];
            newMsg.MSGTYPE = theMsg.MSGTYPE;

            newTimestamp = itsTimeStamp.micros + (1000UL * itsTimeStamp.millis) + (0x100_000_000UL * 1000UL * itsTimeStamp.millis_overflow);
            if (firstTime == null) firstTime = DateTime.Now;
            ProcessMessage(newMsg, newTimestamp);
        }


        /// <summary>
        /// Function for reading messages on FD devices
        /// </summary>
        /// <returns>A TPCANStatus error code</returns>
        private TPCANStatus ReadMessageFD()
        {
            TPCANMsgFD CANMsg;
            TPCANTimestampFD CANTimeStamp;
            TPCANStatus stsResult;

            // We execute the "Read" function of the PCANBasic                
            //
            stsResult = PCANBasic.ReadFD(m_PcanHandle, out CANMsg, out CANTimeStamp);
            if (stsResult != TPCANStatus.PCAN_ERROR_QRCVEMPTY)
                // We process the received message
                //
                ProcessMessage(CANMsg, CANTimeStamp);

            return stsResult;
        }

        /// <summary>
        /// Function for reading CAN messages on normal CAN devices
        /// </summary>
        /// <returns>A TPCANStatus error code</returns>
        private TPCANStatus ReadMessage()
        {
            TPCANMsg CANMsg;
            TPCANTimestamp CANTimeStamp;
            TPCANStatus stsResult;

            // We execute the "Read" function of the PCANBasic                
            //
            stsResult = PCANBasic.Read(m_PcanHandle, out CANMsg, out CANTimeStamp);
            if (stsResult != TPCANStatus.PCAN_ERROR_QRCVEMPTY)
                // We process the received message
                //
                ProcessMessage(CANMsg, CANTimeStamp);

            return stsResult;
        }

        /// <summary>
        /// Function for reading PCAN-Basic messages
        /// </summary>
        private void ReadMessages()
        {
            TPCANStatus stsResult;

            // We read at least one time the queue looking for messages.
            // If a message is found, we look again trying to find more.
            // If the queue is empty or an error occurr, we get out from
            // the dowhile statement.
            //			
            do
            {
                stsResult = m_IsFD ? ReadMessageFD() : ReadMessage();
                if (stsResult == TPCANStatus.PCAN_ERROR_ILLOPERATION)
                    break;
            } while (btnRelease.Enabled && (!Convert.ToBoolean(stsResult & TPCANStatus.PCAN_ERROR_QRCVEMPTY)));
        }
        #endregion

        #region Event Handlers
        #region Form event-handlers
        /// <summary>
        /// Consturctor
        /// </summary>
        public Form1()
        {
            minCollection = new Dictionary<TPCANBitrateFD, double>();
            maxCollection = new Dictionary<TPCANBitrateFD, double>();
            lastTime = new Dictionary<TPCANBitrateFD, double>();
            // Initializes Form's component
            //
            InitializeComponent();
            // Initializes specific components
            //
            InitializeBasicComponents();
        }

        /// <summary>
        /// Form-Closing Function / Finish function
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Releases the used PCAN-Basic channel
            //
            if (btnRelease.Enabled)
                btnRelease_Click(this, new EventArgs());
        }
        #endregion

        #region ComboBox event-handlers
        private void cbbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bNonPnP;
            string strTemp;


            // Get the handle fromt he text being shown
            //
            strTemp = cbbChannel.Text;
            strTemp = strTemp.Substring(strTemp.IndexOf('(') + 1, 3);

            strTemp = strTemp.Replace('h', ' ').Trim(' ');

            // Determines if the handle belong to a No Plug&Play hardware 
            //
            m_PcanHandle = Convert.ToUInt16(strTemp, 16);
            bNonPnP = m_PcanHandle <= PCANBasic.PCAN_DNGBUS1;
            // Activates/deactivates configuration controls according with the 
            // kind of hardware
            //
            cbbHwType.Enabled = bNonPnP;
            cbbIO.Enabled = bNonPnP;
            cbbInterrupt.Enabled = bNonPnP;
        }

        private void cbbBaudrates_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Saves the current selected baudrate register code
            //
            switch (cbbBaudrates.SelectedIndex)
            {
                case 0:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_1M;
                    break;
                case 1:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_800K;
                    break;
                case 2:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_500K;
                    break;
                case 3:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_250K;
                    break;
                case 4:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_125K;
                    break;
                case 5:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_100K;
                    break;
                case 6:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_95K;
                    break;
                case 7:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_83K;
                    break;
                case 8:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_50K;
                    break;
                case 9:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_47K;
                    break;
                case 10:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_33K;
                    break;
                case 11:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_20K;
                    break;
                case 12:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_10K;
                    break;
                case 13:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_5K;
                    break;
            }
        }

        private void cbbHwType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Saves the current type for a no-Plug&Play hardware
            //
            switch (cbbHwType.SelectedIndex)
            {
                case 0:
                    m_HwType = TPCANType.PCAN_TYPE_ISA;
                    break;
                case 1:
                    m_HwType = TPCANType.PCAN_TYPE_ISA_SJA;
                    break;
                case 2:
                    m_HwType = TPCANType.PCAN_TYPE_ISA_PHYTEC;
                    break;
                case 3:
                    m_HwType = TPCANType.PCAN_TYPE_DNG;
                    break;
                case 4:
                    m_HwType = TPCANType.PCAN_TYPE_DNG_EPP;
                    break;
                case 5:
                    m_HwType = TPCANType.PCAN_TYPE_DNG_SJA;
                    break;
                case 6:
                    m_HwType = TPCANType.PCAN_TYPE_DNG_SJA_EPP;
                    break;
            }
        }
        #endregion

        #region Button event-handlers
        private void btnHwRefresh_Click(object sender, EventArgs e)
        {
            TPCANStatus stsResult;
            uint iChannelsCount;
            bool bIsFD;

            // Clears the Channel comboBox and fill it again with 
            // the PCAN-Basic handles for no-Plug&Play hardware and
            // the detected Plug&Play hardware
            //
            cbbChannel.Items.Clear();
            try
            {
                // Includes all no-Plug&Play Handles
                for (int i = 0; i < m_NonPnPHandles.Length; i++)
                    cbbChannel.Items.Add(FormatChannelName(m_NonPnPHandles[i]));

                // Checks for available Plug&Play channels
                //
                stsResult = PCANBasic.GetValue(PCANBasic.PCAN_NONEBUS, TPCANParameter.PCAN_ATTACHED_CHANNELS_COUNT, out iChannelsCount, sizeof(uint));
                if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                {
                    TPCANChannelInformation[] info = new TPCANChannelInformation[iChannelsCount];

                    stsResult = PCANBasic.GetValue(PCANBasic.PCAN_NONEBUS, TPCANParameter.PCAN_ATTACHED_CHANNELS, info);
                    if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                        // Include only connectable channels
                        //
                        foreach (TPCANChannelInformation channel in info)
                            if ((channel.channel_condition & PCANBasic.PCAN_CHANNEL_AVAILABLE) == PCANBasic.PCAN_CHANNEL_AVAILABLE)
                            {
                                bIsFD = (channel.device_features & PCANBasic.FEATURE_FD_CAPABLE) == PCANBasic.FEATURE_FD_CAPABLE;
                                cbbChannel.Items.Add(FormatChannelName(channel.channel_handle, bIsFD));
                            }
                }

                cbbChannel.SelectedIndex = cbbChannel.Items.Count - 1;
                btnInit.Enabled = cbbChannel.Items.Count > 0;

                if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                    MessageBox.Show(GetFormatedError(stsResult));
            }
            catch (DllNotFoundException)
            {
                MessageBox.Show("Unable to find the library: PCANBasic.dll !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }
        private List<DateTime> messageTimestamps = new List<DateTime>();
        private void btnInit_Click(object sender, EventArgs e)
        {
            TPCANStatus stsResult;

            // Connects a selected PCAN-Basic channel
            //
            if (m_IsFD)
                stsResult = PCANBasic.InitializeFD(
                    m_PcanHandle,
                    txtBitrate.Text);
            else
                stsResult = PCANBasic.Initialize(
                    m_PcanHandle,
                    m_Baudrate,
                    m_HwType,
                    Convert.ToUInt32(cbbIO.Text, 16),
                    Convert.ToUInt16(cbbInterrupt.Text));

            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                if (stsResult != TPCANStatus.PCAN_ERROR_CAUTION)
                    MessageBox.Show(GetFormatedError(stsResult));
                else
                {
                    IncludeTextMessage("******************************************************");
                    IncludeTextMessage("The bitrate being used is different than the given one");
                    IncludeTextMessage("******************************************************");
                    stsResult = TPCANStatus.PCAN_ERROR_OK;
                }
            else
                // Prepares the PCAN-Basic's PCAN-Trace file
                //
                ConfigureTraceFile();

            // Sets the connection status of the main-form
            //
            SetConnectionStatus(stsResult == TPCANStatus.PCAN_ERROR_OK);

            btnMsgClear_Click(null, null);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            // Releases a current connected PCAN-Basic channel
            //
            PCANBasic.Uninitialize(m_PcanHandle);
            tmrRead.Enabled = false;
            if (m_ReadThread != null)
            {
                m_ReadThread.Abort();
                m_ReadThread.Join();
                m_ReadThread = null;
            }

            // Sets the connection status of the main-form
            //
            SetConnectionStatus(false);
            firstTime = null;
        }

        private void btnMsgClear_Click(object sender, EventArgs e)
        {
            // The information contained in the messages List-View
            // is cleared
            //
            lock (m_LastMsgsList.SyncRoot)
            {
                m_LastMsgsList.Clear();
                listMinMax.Items.Clear();
                minCollection.Clear();
                maxCollection.Clear();
                lastTime.Clear();
            }
            lock (m_LastMsgsList.SyncRoot)
            {
                listTrace.Items.Clear();
            }
        }


        private void btnInfoClear_Click(object sender, EventArgs e)
        {
            // The information contained in the Information List-Box 
            // is cleared
            //
            lbxInfo.Items.Clear();
        }

        private TPCANStatus WriteFrame()
        {
            TPCANMsg CANMsg;
            TextBox txtbCurrentTextBox;

            // We create a TPCANMsg message structure 
            //
            CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];

            // We configurate the Message.  The ID,
            // Length of the Data, Message Type
            // and the data
            //
            return PCANBasic.Write(m_PcanHandle, ref CANMsg);
        }

        private TPCANStatus WriteFrameFD()
        {
            TPCANMsgFD CANMsg;
            TextBox txtbCurrentTextBox;
            int iLength;

            // We create a TPCANMsgFD message structure 
            //
            CANMsg = new TPCANMsgFD();
            CANMsg.DATA = new byte[64];

            // The message is sent to the configured hardware
            //
            return PCANBasic.WriteFD(m_PcanHandle, ref CANMsg);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            TPCANStatus stsResult;

            // Send the message
            //
            stsResult = m_IsFD ? WriteFrameFD() : WriteFrame();

            // The message was successfully sent
            //
            if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                IncludeTextMessage("Message was successfully SENT");
            // An error occurred.  We show the error.
            //			
            else
                MessageBox.Show(GetFormatedError(stsResult));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            TPCANStatus stsResult;

            // Resets the receive and transmit queues of a PCAN Channel.
            //
            stsResult = PCANBasic.Reset(m_PcanHandle);

            // If it fails, a error message is shown
            //
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                MessageBox.Show(GetFormatedError(stsResult));
            else
                IncludeTextMessage("Receive and transmit queues successfully reset");
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            TPCANStatus stsResult;
            String errorName;

            // Gets the current BUS status of a PCAN Channel.
            //
            stsResult = PCANBasic.GetStatus(m_PcanHandle);

            // Switch On Error Name
            //
            switch (stsResult)
            {
                case TPCANStatus.PCAN_ERROR_INITIALIZE:
                    errorName = "PCAN_ERROR_INITIALIZE";
                    break;

                case TPCANStatus.PCAN_ERROR_BUSLIGHT:
                    errorName = "PCAN_ERROR_BUSLIGHT";
                    break;

                case TPCANStatus.PCAN_ERROR_BUSHEAVY: // TPCANStatus.PCAN_ERROR_BUSWARNING
                    errorName = m_IsFD ? "PCAN_ERROR_BUSWARNING" : "PCAN_ERROR_BUSHEAVY";
                    break;

                case TPCANStatus.PCAN_ERROR_BUSPASSIVE:
                    errorName = "PCAN_ERROR_BUSPASSIVE";
                    break;

                case TPCANStatus.PCAN_ERROR_BUSOFF:
                    errorName = "PCAN_ERROR_BUSOFF";
                    break;

                case TPCANStatus.PCAN_ERROR_OK:
                    errorName = "PCAN_ERROR_OK";
                    break;

                default:
                    errorName = "See Documentation";
                    break;
            }

            // Display Message
            //
            IncludeTextMessage(String.Format("Status: {0} (0x{1:X}h)", errorName, stsResult));
        }
        #endregion

        #region Timer event-handler
        private void tmrRead_Tick(object sender, EventArgs e)
        {
            // Checks if in the receive-queue are currently messages for read
            // 

            ReadMessages();
        }
        private void tmrDisplay_Tick(object sender, EventArgs e)
        {
            DisplayMessages();
        }
        private void tmrCalculateDelay_Tick(object sender, EventArgs e)
        {
            tmrCalculateDelay.Enabled = false;
            lock (m_LastMsgsList.SyncRoot)
            {
                foreach (var key in lastTime.Keys)
                {
                    if (!maxCollection.ContainsKey(key) || !this.lastTime.ContainsKey(key)) continue;

                    var systemTime = Environment.TickCount;
                    var lastMessageTime = lastTime[key] / 1000d;
                    var difference = systemTime - lastMessageTime;
                    maxCollection[key] = Math.Max(maxCollection[key], difference);
                    Debug.WriteLine($"sys:{systemTime} last:{lastMessageTime} diff:{difference}");
                }
                for (int i = 0; i < listMinMax.Items.Count; i++)
                {
                    var id = listMinMax.Items[i].SubItems[1].Text;
                    if (!maxCollection.ContainsKey(id)) continue;
                    listMinMax.Items[i].SubItems[3].Text = maxCollection[id].ToString("0.0");
                }
            }
            tmrCalculateDelay.Enabled = true;
        }
        #endregion

        #region Information List-Box event-handler
        private void lbxInfo_DoubleClick(object sender, EventArgs e)
        {
            // Clears the content of the Information List-Box
            //
            btnInfoClear_Click(this, new EventArgs());
        }



        #endregion

        #region Radio- and Check- Buttons event-handlers

        private void rdbTimer_CheckedChanged(object sender, EventArgs e)
        {
            if (!btnRelease.Enabled)
                return;

            // According with the kind of reading, a timer, a thread or a button will be enabled
            //

            // Abort Read Thread if it exists
            //
            if (m_ReadThread != null)
            {
                m_ReadThread.Abort();
                m_ReadThread.Join();
                m_ReadThread = null;
            }

            // Enable Timer
            //
            tmrRead.Enabled = btnRelease.Enabled;
        }

        private void chbCanFD_CheckedChanged(object sender, EventArgs e)
        {
            m_IsFD = chbCanFD.Checked;

            cbbBaudrates.Visible = !m_IsFD;
            cbbHwType.Visible = !m_IsFD;
            cbbInterrupt.Visible = !m_IsFD;
            cbbIO.Visible = !m_IsFD;
            laBaudrate.Visible = !m_IsFD;
            laHwType.Visible = !m_IsFD;
            laIOPort.Visible = !m_IsFD;
            laInterrupt.Visible = !m_IsFD;

            txtBitrate.Visible = m_IsFD;
            laBitrate.Visible = m_IsFD;
        }
        #endregion

        #endregion

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnTrace_Click(object sender, EventArgs e)
        {
            listTrace.Visible = !listTrace.Visible;
            btnTrace.Text = listTrace.Visible ? "Hide Trace" : "Show Trace";
        }
        private void listTrace_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBitrate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
