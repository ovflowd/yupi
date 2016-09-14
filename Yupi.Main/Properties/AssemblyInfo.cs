#region Header

// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.
//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

#endregion Header

using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.
[assembly: AssemblyTitle("Yupi! Private HH Emulator")]
[assembly: AssemblyDescription("Private Open Source HH Emulator for Cool Guys")]
[assembly: AssemblyCompany("UIoT")]
[assembly: AssemblyProduct("Yupi! Emulator")]
[assembly: AssemblyCopyright("Claudio A. Santoro W., Kessiler R., Sulake Corp. Oy")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.
[assembly: System.Reflection.AssemblyVersion(
        ThisAssembly.Git.SemVer.Major + "." + ThisAssembly.Git.SemVer.Minor + "." + ThisAssembly.Git.SemVer.Patch)]
[assembly: System.Reflection.AssemblyFileVersion(
        ThisAssembly.Git.SemVer.Major + "." + ThisAssembly.Git.SemVer.Minor + "." + ThisAssembly.Git.SemVer.Patch)]
[assembly: System.Reflection.AssemblyInformationalVersion(
               ThisAssembly.Git.SemVer.Major + "." +
               ThisAssembly.Git.SemVer.Minor + "." +
               ThisAssembly.Git.SemVer.Patch + "-" +
               ThisAssembly.Git.Branch + "+" +
               ThisAssembly.Git.Commit)]