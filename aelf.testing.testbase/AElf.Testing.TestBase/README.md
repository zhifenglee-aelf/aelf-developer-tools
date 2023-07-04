# AElf.Testing.TestBase

This package is required in writing unit tests for AElf C# smart contracts.

## Usage

### Preparation

Add a local nuget source - this only needs to be done once.
```
nuget sources Add -Name Local -Source $env:UserProfile\LocalNuget
```

### Pack this tool

```
dotnet pack
```

### Add the local nuget package to local source

```
nuget add .\bin\Debug\AElf.Testing.TestBase.1.0.0-dev.nupkg -Source $env:UserProfile\LocalNuget
```

### Reference the tool in the test project

```xml
    <ItemGroup>
        <PackageReference Include="AElf.Testing.TestBase" Version="1.0.0-dev" />
    </ItemGroup>
```
