# Windows registry supports classes by Schizo

The library contains auxiliary utility for writing and reading data from the windows registry.

# Adding to the project

#### .NET CLI
```CLI
> dotnet add package Hopex.RegEdit --version 23.0.1
```

#### Package Manager
```CLI
PM> NuGet\Install-Package Hopex.RegEdit -Version 23.0.1
```

#### PackageReference
```XML
<PackageReference Include="Hopex.RegEdit" Version="23.0.1" />
```

#### Paket CLI
```CLI
> paket add Hopex.RegEdit --version 23.0.1
```

#### Script & Interactive
```CLI
> #r "nuget: Hopex.RegEdit, 23.0.1"
```

#### Cake
```
// Install Hopex.RegEdit as a Cake Addin
#addin nuget:?package=Hopex.RegEdit&version=23.0.1

// Install Hopex.RegEdit as a Cake Tool
#tool nuget:?package=Hopex.RegEdit&version=23.0.1
```

# How to use

### Writing data

```C#
public void WriteToRegistry()
{
    // Create a registry instance
    RegEdit regEdit = new RegEdit(registryKey: Registry.CurrentUser);
    
    // Initialize the registry variables
    string registryPath = @"Software\YourOrganization\YourApplicationName\Settings";
    string registryParameter = "Theme";
    KnownColor registryValue = Color.Red.ToKnownColor();
    
    // Writing data to the register
    regEdit.Write(
        path: registryPath, 
        parameter: registryParameter, 
        value: registryValue
    );
}
```

### Reading data

```C#
    // Create a registry instance
    RegEdit regEdit = new RegEdit(registryKey: Registry.CurrentUser);
    
    // Initialize the registry variables
    string registryPath = @"Software\YourOrganization\YourApplicationName\Settings";
    string registryParameter = "Theme";
    
    // Get the saved value
    var savedValue = regEdit.Read(
        path: registryPath,
        parameter: registryParameter
    );
    
    Console.WriteLine(savedValue); // output color name for console application
    BackColor = Color.FromName((string)savedValue); // changing background color for WinForm application
    
    // You can also get the number of keys in the specified section and the number of child sections
    Console.WriteLine(regEdit.GetSubKeyCount(path: registryPath)); // Number of subsections for the specified section
    Console.WriteLine(regEdit.GetValueCount(path: registryPath)); // Number of values in the specified section
```

## License

MIT License
