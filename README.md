# PCAN-Basic CAN Cycle-Time Monitor

A Windows Forms tool for monitoring CAN / CAN FD traffic through a PEAK-System PCAN interface. It is built on the official **PCAN-Basic** C# sample, extended to measure the **minimum and maximum cycle time of every CAN ID** on the bus, so you can quickly spot messages that arrive too fast, too slowly, or stop arriving altogether.

<!-- Add a screenshot of the main window here -->
<!-- ![Main window](docs/screenshot.png) -->

## Features

- **Connect to any PCAN channel** – USB, PCI, LAN and other plug-and-play devices, plus legacy ISA/Dongle channels.
- **Classic CAN and CAN FD** – choose a standard baud rate, or enter a CAN FD bit-rate string when the *CAN-FD* box is checked.
- **Per-ID statistics table** showing, for every unique message (ID + type):
  - **Type** – STD / EXT, RTR, FD, BRS, ESI, ECHO, STATUS and ERROR frames
  - **ID** – in hex (3 digits for standard, 8 digits for extended IDs)
  - **Min** – shortest observed interval between two frames, in ms
  - **Max** – longest observed interval in ms; this also grows while an ID is silent, so a message that stops being sent shows up immediately
  - **Count** – number of frames received
  - **Data** – latest payload in hex
- **Trace view** – toggle a chronological list of received frames with a timestamp relative to the first frame after connecting.
- **Channel tools** – Reset (clears RX/TX queues), Status (bus state) and Versions (driver/API info).
- **PCAN-Trace file** – a trace is recorded automatically on connect (single file, overwritten each session, max 5 MB).

## Requirements

- Windows
- A PEAK-System PCAN interface (e.g. PCAN-USB, PCAN-USB FD) and its [device driver](https://www.peak-system.com/Drivers.523.0.html?&L=1)
- [PCAN-Basic API](https://www.peak-system.com/PCAN-Basic.239.0.html?&L=1) – `PCANBasic.dll` must be reachable by the application (it is normally installed into the Windows system folder with the driver, or can be placed next to the `.exe`)
- .NET Framework 4.6.1 or later
- Visual Studio 2017 or later to build

> Use the `PCANBasic.dll` that matches the platform you build for. The project defaults to **Any CPU**, which runs as 64-bit on a 64-bit Windows, so the 64-bit DLL is required. x86 and x64 build configurations are also included.

## Getting started

1. Clone the repository:
   ```bash
   git clone https://github.com/<your-username>/<your-repo>.git
   ```
2. Open `PCANBasicExample.sln` in Visual Studio.
3. Restore NuGet packages (right-click the solution → *Restore NuGet Packages*).
4. Build and run (**F5**).

## Usage

1. Plug in your PCAN device and click **Refresh** to list available channels.
2. Select the **Hardware** channel.
3. Choose the **Baudrate** – or tick **CAN-FD** and enter the FD bit-rate string.
4. Click **Initialize**. Received messages start appearing in the table.
5. Watch the **Min / Max** columns to check the cycle times of each ID.
6. Click **Trace** to show or hide the chronological trace list.
7. Use **Clear** to reset the table and statistics, and **Release** to disconnect.

If the connection fails, the reason is shown in a message box; general status messages go to the *Information* box (double-click it to clear).

## Project structure

| File | Description |
| --- | --- |
| `PCANBasic.cs` | PEAK's C# wrapper for the PCAN-Basic API (`PCANBasic.dll`) |
| `Form1.cs` | Main logic: connection handling, message reading, min/max cycle-time calculation, trace list |
| `Form1.Designer.cs` | UI layout |
| `Program.cs` | Application entry point |

### How the timing works

- Frames are read from the receive queue by a 1 ms timer.
- For each ID, the interval between consecutive hardware timestamps is compared against the stored min/max values.
- A separate timer compares the current system time against the last reception of each ID, so **Max** keeps increasing while no new frame arrives.
- The table is refreshed every 50 ms.

## Credits and license

This project is based on the PCAN-Basic sample application by [PEAK-System Technik GmbH](https://www.peak-system.com). `PCANBasic.cs` and the PCAN-Basic API are © PEAK-System Technik GmbH and are subject to PEAK's own license terms, so please check them before redistributing.

<!-- State the license for your own changes here, e.g. MIT -->
