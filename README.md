# SyslogdListener

.Net 6 program that listens to UDP port 514 for syslog messages broadcasted across the network that
- Displays to the console
- Logs to a CSV file

This has been tested with Netgear routers and Dell iDrac logging.

## To build the SyslogdListener

1. git clone https://github.com/warpkez/SyslogdListener.git
2. Make a build directory eg mkdir build 
3. cd SyslogdListener
4. dotnet build --output ..\build --configuration Release .

## Peculiarities 

It has been observed that the console output may be written twice.  This has occured on the preview version of .Net 6.0.4