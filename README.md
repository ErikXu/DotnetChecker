# DotnetChecker

## Introduction
A tool helps you to check the server informaton and check the .Net Core capable for softwares like redis, mongo etc, while .Net Core runtime or sdk is not required.

## Build

  The [.NET 3.1 SDK](https://dotnet.microsoft.com/download) is requird when building the checker.
  
- Using in Windows

  Execute `src/build_windows.bat` to generate.

- Using in Linux

  Execute `src/build_linux.bat` to generate.

## Usage

- Windows

  - Copy the `dotnet-checker.exe` to server. 

  - Get server info
  
  ```shell
  dotnet-checker info
  
  Machine Name: xxx
  Host Name: xxx
  User Domain Name: xxx
  User Name: Administrator
  OS Description: Microsoft Windows 10.0.18362
  OS Architecture: X64
  Process Architecture: X64
  System PageSize: 4096
  Processors: 8
  Memory: Total 32727(M), Used 8680(M), Free 24047(M)
  Ip Addresses:
    xxxx::xxxx:xxxx:xxxx:cb20%3
    xxxx::xxxx:xxxx:xxxx:2e35%13
    xxxx::xxxx:xxxx:xxxx:e06e%16
    xxx.xxx.xxx.xxx
    192.168.193.1
    192.168.204.1
  ```
  
- Linux
  
  - Copy the `dotnet-checker` to server and execute `chmod +x dotnet-checker`.
  
  - Get server info
  
  ```bash
  ./dotnet-checker info
  
  Machine Name: xxx
  Host Name: xxx
  User Domain Name: xxx
  User Name: root
  OS Description: Linux 3.10.0-1062.18.1.el7.x86_64 #1 SMP Tue Mar 17 23:49:17 UTC 2020
  OS Architecture: X64
  Process Architecture: X64
  System PageSize: 4096
  Processors: 4
  Memory: Total 7812(M), Used 4040(M), Free 405(M)
  Ip Addresses:
    xxxx::xxxx:xxxx:xxxx:ae27%3
    xxxx::xxxx:xxxx:xxxx:eeee%63
    xxxx::xxxx:xxxx:xxxx:eeee%64
    xxxx::xxxx:xxxx:xxxx:eeee%65
    xxx.xxx.xxx.xxx
    172.17.0.1
    10.244.36.64
  ```
