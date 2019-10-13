# Licensing

As of v3.10.3, licenses will no longer be required.  For previous versions, please use the following key, which will remove all imposed limitations.

```
656622119-qUT8+J05IvRlosEnAaIZTsGeQBA7mcVNDiTaW49xIOgOq6O/Ay5z9dVFK0dJaQaalYMPLtMj5NeajqSG/Rmkykbi1a13COTZoy15wajYcG/SkcD1eWFwmWFR5ucBWwPOcjhJPfIoofUwe2qhaPd9CEcSayv2zlUlBlSSqI0cM1t7IklkIjo2NTY2MjIxMTksIkV4cGlyeURhdGUiOiIyMDIwLTA4LTAxVDAwOjAwOjAwIiwiVHlwZSI6IlRyZWxsb0J1c2luZXNzIn0=
```

## Applying your license

To register your license key, the pass the content to the `License.RegisterLicense()` static method.

```csharp
var content = File.ReadAllText("[path to your license file]");
License.RegisterLicense(content);
```