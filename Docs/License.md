# License

Manages the license used with Manatee.Trello. A license can be purchased at .

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- License

## Methods

### static void RegisterLicense(string license)

Register the specified license with Manatee.Trello. A license can be purchased at .

**Parameter:** license

The license text to register.

#### Remarks

The recommended way to register the license key is to call Manatee.Trello.License.RegisterLicense(System.String) once during application start up. In ASP.NET web applications it can be placed in the &lt;c&gt;Startup.cs&lt;/c&gt; or &lt;c&gt;Global.asax.cs&lt;/c&gt;, in WPF applications it can be placed in the &lt;c&gt;Application.Startup&lt;/c&gt; event, and in Console applications it can be placed in the &lt;c&gt;static void Main(string[] args)&lt;/c&gt; meethod.

#### Example

This sample shows how to register a Manatee.Trello license with the Manatee.Trello.License.RegisterLicense(System.String) method.
&lt;code&gt; 
// replace with your license key 
string licenseKey = &quot;manatee-json-license-key&quot;; 
License.RegisterLicense(licenseKey); 
&lt;/code&gt;

