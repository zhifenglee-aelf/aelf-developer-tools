# AElf.Tools
An MsBuild tool to compile AElf contracts using protobuf and an extension tool. It's based on the [gRPC tool](https://github.com/grpc/grpc/tree/master/src/csharp).

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
nuget add .\bin\Debug\AElf.Tools.1.0.0.nupkg -Source $env:UserProfile\LocalNuget
```

### Reference the tool in an AElf Contract project and include the protobuf sources

```xml
    <ItemGroup>
        <PackageReference Include="AElf.Tools" Version="1.0.0">
          <PrivateAssets>all</PrivateAssets>
          <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
        </PackageReference>
    </ItemGroup>

    <ItemGroup>
        <Protobuf Include="Protobuf/base/*.proto">
            <ContractOutputOptions>nocontract</ContractOutputOptions>
        </Protobuf>
        <Protobuf Include="Protobuf/contract/*.proto"/>
        <Protobuf Include="Protobuf/message/*.proto">
            <ContractOutputOptions>nocontract</ContractOutputOptions>
        </Protobuf>
        <Protobuf Include="Protobuf/reference/*.proto">
            <ContractOutputOptions>reference</ContractOutputOptions>
            <Access>Internal</Access>
        </Protobuf>
        <Protobuf Include="Protobuf/stub/*.proto">
            <ContractOutputOptions>stub</ContractOutputOptions>
            <Access>Internal</Access>
        </Protobuf>
    </ItemGroup>
```

## Open Items and Improvements
- [x] ~~Clean up gRPC stuff (e.g. gRPC supports codegen for C++ which we don't need)~~
- [x] ~~Currently only Windows x64 binaries are included, we need to add binaries for other platforms~~
- [x] ~~Currently proto files have to be specified in `.csproj` file, we can add direct detection for convention folders under `Protobuf`, subfolders include:~~
   - ~~`base` for protos which requires `ContractBase` to be generated~~
   - ~~`contract` for protos which requires `ContractCode` to be generated~~
   - ~~`message` for protos which only require protobuf messages to be generated~~
   - ~~`reference` for protos which require `ReferenceState` to be generated~~
   - ~~`stub` for protos which require `Stub`s to be generated~~
- [ ] Currently this tool include `aelf/options.proto` and `aelf/core.proto` (together with google native protos), we can consider adding `acs` protos as well
- [x] ~~Add tests~~
- [ ] Publish nuget packages
