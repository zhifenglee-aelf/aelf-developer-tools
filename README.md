# AElf Developer Tools

This repository contains tools that helps developers write and test AElf C# smart contracts more easily. The tools are:

1. aelf.tools
   An MsBuild tool that is based on [gRPC Tool](https://github.com/grpc/grpc/tree/master/src/csharp/Grpc.Tools). It helps 
   developers generate protobuf/grpc code.
2. aelf.contractdetector
   Currently it's only used by `aelf.boilerplate.testbase`, it's to detect the dependencies that are Contracts.
3. aelf.boilerplate.testbase
   It contains the `TestBase`, that handles the chain setup and system contract deployments which are required to set up
   the process for testing smart contract functionality.
4. templates
   It contains a `BingoGame` template that can be used in `dotnet new` so that a user can modify it and implement their own
   smart contract.



