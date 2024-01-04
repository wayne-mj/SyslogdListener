#### Syslogd logging
##### Background

When I started out with Unix systems administration, all logging was performed either to the console or the system log file, 
SNMP was an option but in some cases it was a licensed option. If managing a small number of machines, 
it was no trouble 'telneting' into system to check the logs, but checking the logs of a large number of machines could get tiresome fast.

I originally wrote this program in Visual Basic to recieve the broadcasted messages and write them to a Access data file. 

Further iterations included SQL databases.

This version, written in C#, simply echos the messages to the screen and writes it out to a CSV file.

[Repository location](https://github.com/wayne-mj/SyslogdListener.git)

##### About the code

.Net 6 program that listens to UDP port 514 for syslog messages broadcasted across the network that
- Displays to the console
- Logs to a CSV file

This has been tested with Netgear routers and Dell iDrac logging.

##### To build the SyslogdListener

1. git clone https://github.com/wayne-mj/SyslogdListener.git
2. Make a build directory eg mkdir build 
3. cd SyslogdListener
4. dotnet build --output ..\\\\build --configuration Release .

##### Screen grabs
![Screen grab of running session!](https://diqda2sl5kdc9.cloudfront.net/images/syslogd.png/images/syslogd.png)

##### Peculiarities 

It has been observed that the console output may be written twice. This has occured on the preview version of .Net 6.0.4"
