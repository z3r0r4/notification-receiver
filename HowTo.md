dotnet add package  WindowsAPICodePack-Core
 dotnet add package WindowsAPICodePack-Shell
 dotnet add package WindowsAPICodePack-ShellExtensions

copy
Program.cs
ShellHelper.cs
from https://github.com/psantosl/ConsoleToast

add 
<PropertyGroup>
...
<TargetPlatformVersion>10.0.10586</TargetPlatformVersion>
...
<PropertyGroup>

<ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Runtime" />
    <Reference Include="System.Runtime.InteropServices.WindowsRuntime" />
    <Reference Include="Windows">
      <HintPath>..\..\..\..\..\Program Files (x86)\Windows Kits\10\UnionMetadata\Facade\Windows.WinMD</HintPath>
    </Reference>
    <Reference Include="WindowsBase" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.Toolkit.Uwp.Notifications" Version="6.1.1" />
    <PackageReference Include="WindowsAPICodePack-Core" Version="1.1.2" />
    <PackageReference Include="WindowsAPICodePack-Shell" Version="1.1.1" />
    <PackageReference Include="WindowsAPICodePack-ShellExtensions" Version="1.1.1" />
  </ItemGroup>
  to *.csproj

  dotnet run

  source: http://blog.plasticscm.com/2016/08/how-to-send-windows-toast-notifications.html