# AElf C# smart contract templates

This folder contains templates of AElf smart contracts in C#:
- ContractTemplate
- HelloWorldContract

## Usage

### Add the template

```
cd HelloWorldContract
dotnet new install .\
```

### Create a project using the template
```
mkdir temp\HelloWorldContract
cd temp\HelloWorldContract
dotnet new aelfcontract
```

### Build
```
cd src
dotnet build
```
Please note, place proto files in the Protobuf/xxx/ directory and contracts in the Contract/ directory. And change the nuget package version in csproj file, as well as the nuget repository address.
