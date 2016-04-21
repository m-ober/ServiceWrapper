# ServiceWrapper
Run any executable as a Windows service.

## Setup

### Configuration

Configuration is done via `service.xml`, which needs to be placed in the same directory as the `servicewrapper.exe`.

Example:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<Configuration>
	<!-- DO NOT modify once the service has been installed -->        
	<Service>
		<LongName>TeamSpeak3 Server</LongName>
		<ShortName>ts3server</ShortName>
		<Dependencies>tcpip</Dependencies>
	</Service>
	<!-- May always be modified -->
	<Process>
		<Executable>ts3server.exe</Executable>
		<Arguments></Arguments>
	</Process>
</Configuration>
```

### Install

From an (elevated) command prompt run: `servicewrapper.exe /i`

If installation was successful, the service can now be started via `net start ShortName`.

### Uninstall

From an (elevated) command prompt run: `servicewrapper.exe /u`

If you want to re-install the service, you may need to restart the computer before doing so.

### Limitations

* There is no error checking
* If the process dies, the service is stopped (no auto-restart). 
* If the service is stopped, only the parent process is killed
