# Branca Command Line Tool

## Build from source

Requires .NET 5 SDK.

### Windows

```cli
dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true
```

### Linux

```cli
dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained false
```
