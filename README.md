# FlashEx

This tool created for my self for fun but I decided to share with others :coffee:

## Features
- Flash firmwares
- Erase flash
- Backup flash
- Reset device state
- Monitor Serial Port
- Read info from ESP devices
- Set WIFI credentials for Tasmota firmwares
- Set MQTT credentials for Tasmota firmwares
- Download Tasmota and any custom configured URLs firmware
- Customizable Database for dataseets and one summary picture of the device
- Debug: Send commands to device
- App will save the last used firmware and baudrate

## Settings
Add more firmware urls: `config/firmware_urls.txt`

Firmwares folder: `firmwares`

If you want to add more items into `Database` just
- Create new folder in `database` folder then place `info.png` image into it.
- All pdf files in this folder listed in `Docs` box on the UI.


## Screenshots

<p align="center">
    <img src=./ProjectImages/MainPage1.png>    
</p>

<p align="center">
    <img src=./ProjectImages/TasmotaPage1.png>    
</p>

<p align="center">
    <img src=./ProjectImages/DatabasePage.png>    
</p>

## Sources
- [ESPtool](https://github.com/espressif/esptool)
- [FontAwesome.Sharp](https://github.com/awesome-inc/FontAwesome.Sharp)
- [Logo made by Playground](https://playground.com/)
