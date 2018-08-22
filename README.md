![Alturos.PanTilt](doc/logo-banner.png)

# Alturos.PanTilt

This project delivers an interface for general pan tilt communication. Additionally we deliver an implementation for [Eneo VPT-601](https://eneo-security.com/de/produkte/positioniersysteme/schwenk-neigekoepfe.html) unit.
We have three different communication providers udp/tcp/serial available.

The library supports the following primary features
- [x] Move to absolute position
- [x] Move with relative speed
- [x] Position feedback
- [x] Get limits

And some Eneo special features
- [x] Set limits
- [x] Get temperature
- [x] Set relay

### nuget
The package is available on [nuget](https://www.nuget.org/packages/Alturos.PanTilt)
```
PM> install-package Alturos.PanTilt
```

### Network communication to RS-422/RS-485

Do you want to talk about the network with your pan tilt unit you can use a serial device server.

Manufacturer | Product | Communications |
--- | --- | --- |
iccdesigns.com | ETH-1000 | RS-485 |
moxa.com | NPort 5130 | RS-422/RS-485 |
atop.com.tw | SE5001 | RS-422/RS-485 |
aten.com | SN3101 | RS-422/RS-485 |


### Examples

#### Move to position pan 50° and tilt 0° with udp communication
```cs
var ipAddress = IPAddress.Parse("192.168.1.10");
using (ICommunication communication = new UdpNetworkCommunication(ipAddress, 4003, 4003))
using (IPanTiltControl control = new EneoPanTiltControl(communication))
{
	control.PanAbsolute(50);
	control.TiltAbsolute(0);
}
```

#### Move to position pan 50° and tilt 0° with tcp communication
```cs
var ipAddress = IPAddress.Parse("192.168.1.10");
using (ICommunication communication = new TcpNetworkCommunication(new IPEndPoint(ipAddress, 4003)))
using (IPanTiltControl control = new EneoPanTiltControl(communication))
{
	control.PanAbsolute(50);
	control.TiltAbsolute(0);
}
```

#### Move to position pan 50° and tilt 0° with serial communication
```cs
var serialPort = "COM1";
using (ICommunication communication = new SerialPortCommunication(serialPort))
using (IPanTiltControl control = new EneoPanTiltControl(communication))
{
	control.PanAbsolute(80);
	control.TiltAbsolute(0);
}
```

#### Activate pt head position feedback
```cs
var serialPort = "COM1";
using (ICommunication communication = new SerialPortCommunication(serialPort))
using (IPanTiltControl control = new EneoPanTiltControl(communication))
{
	control.Start();
	Thread.Sleep(200); //delay for response, only need if you stop very fast
	var currentPosition = control.GetPosition();
	//example: {0,02/0,98}
	control.Stop();
}
```
