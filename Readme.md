# Tyrrrz Settings Library

My settings library.

- Settings file name and location are configurable
- Uses Newtonsoft.Json
- Serializes and deserializes itself in place
- Handles any type that can be serialized by Newtonsoft.Json
- Can have default values
- Supports versioning
- Implements INotifyPropertyChanged
- Implements ICloneable with deep copy via Newtonsoft.Json
- Has safe methods for when a file cannot be loaded/saved due to an exception
- Offers a staging system, which exposes two instances of settings: one (stable) for application and another (dirty) for making changes
- More...

**Download:**

- Using nuget: `Install-Package Tyrrrz.Settings`

**Dependencies:**

- [Newtonsoft.Json](http://www.newtonsoft.com/json) - save to/load from file

**Sample usage:**

- Basic quick start

```c#
using System;
using Tyrrrz.Settings;

public class Settings : SettingsManager
{
    public static Settings Default { get; } = new Settings();

    public string UserName { get; set; }
    public int UserId { get; set; } = -1;
    public DateTime LastLogin { get; set; }
}

...

Settings.Default.Load(); // load settings from file
Console.WriteLine(Settings.UserName); // properties are populated
Console.WriteLine(Settings.UserId); // properties can have default values
Console.WriteLine(Settings.LastLogin); // works with any .NET type, including your own

...

Settings.Default.UserId = 32; // make changes directly
Settings.Default.Save(); // save settings to file

```

- Making use of INotifyPropertyChanged

```c#
using System;
using Tyrrrz.Settings;

public class Settings : SettingsManager
{
    public static Settings Default { get; } = new Settings();

    private string _userName;
    private int _userId = -1;
    private DateTime _lastLogin;

    public string UserName
    {
        get { return _userName; }
        set { Set(ref _userName, value); }
    }

    public string UserId
    {
        get { return _userId; }
        set { Set(ref _userId, value); }
    }

    public string LastLogin
    {
        get { return _lastLogin; }
        set { Set(ref _lastLogin, value); }
    }
}

...

Settings.Default.PropertyChanged += (sender, args) => { }; // handle the event or use WPF bindings

```

- Using the Stager

```c#
using System;
using Tyrrrz.Settings;

public class Settings : SettingsManager
{
    public static Stager<Settings> Stager { get; } = new Stager<Settings>();

    private string _userName;
    private int _userId = -1;
    private DateTime _lastLogin;

    public string UserName
    {
        get { return _userName; }
        set { Set(ref _userName, value); }
    }

    public string UserId
    {
        get { return _userId; }
        set { Set(ref _userId, value); }
    }

    public string LastLogin
    {
        get { return _lastLogin; }
        set { Set(ref _lastLogin, value); }
    }
}

...

Stager.Load(); // load from file

...

public void SomeBackgroundMethod()
{
    Console.WriteLine(Stager.Current.UserName); // can access the stable snapshot, where all the settings are saved
}

...

Stager.Staging.UserName = "new username"; // can change values on the staging instance, without affecting the current instance

...

Stager.Save(); // saves settings to file and synchronizes instances

```
