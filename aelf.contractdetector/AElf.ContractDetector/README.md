# AElf.ContractDetector

This replaces AElf.ContractDeployer. Instead of generating a manifest file, this detector include all `Contract` dlls  as embedded resources in the dll.
And hence it will be easier for dependency management.

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
nuget add .\bin\Debug\AElf.ContractDetector.1.0.0.nupkg -Source $env:UserProfile\LocalNuget
```

### Reference the tool in a `.csproject` and include the Contract dependencies

```xml
    <ItemGroup>
        <PackageReference Include="AElf.ContractDetector" Version="1.0.0" />
    </ItemGroup>
    <ItemGroup>
        <PackageReference Include="AElf.Contracts.Configuration" Version="1.2.2">
            <ReferenceOutputAssembly>false</ReferenceOutputAssembly>
            <OutputItemType>Contract</OutputItemType>
            <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
        </PackageReference>
    </ItemGroup>
```
