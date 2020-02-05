using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Alturos.PanTilt")]
[assembly: AssemblyDescription("Pan Tilt control")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Alturos Destinations")]
[assembly: AssemblyProduct("Alturos.PanTilt")]
[assembly: AssemblyCopyright("Copyright ©  2018 Alturos Destinations")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("5821939a-053d-4818-b2a5-b8a805e7d40f")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("3.0.5")]
[assembly: AssemblyFileVersion("3.0.5")]

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]