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
    > dotnet-checker.exe info
    
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
      xxxx::xxxx:xxxx:xxxx:xxxx
      xxx.xxx.xxx.xxx
      ...
    ```
  
  - Check redis
  
    ```shell
    > dotnet-checker.exe redis -c 127.0.0.1:6379
    StringSet is normal.
    StringGet is normal, value is OK.
    KeyDelete is normal.
    
    > dotnet-checker.exe redis get test -c 127.0.0.1:6379
    test
    ```
  
  - Check mongo
    ```shell
    > dotnet-checker.exe mongo -c mongodb://127.0.0.1:27017/test -d test
    InsertOne is normal.
    Find is normal. The value is { "_id" : ObjectId("5efb2db48278cb7ff8ba2fa2"), "id" : "0ed4c50c-d9aa-44a5-8ec9-f8fa7b9b09fe", "name" : "dotnet-checker", "timestamp" : NumberLong(1593519540) }
    DeleteOne is normal.
    DropCollection is normal.
    ```
  
- Linux
  
  - Copy the `dotnet-checker` to server and execute `chmod +x dotnet-checker`.
  
  - Get server info
  
    ```bash
    > ./dotnet-checker info
    
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
      xxxx::xxxx:xxxx:xxxx:xxxx
      xxx.xxx.xxx.xxx
      ...
    ```

  - Check redis
  
    ```bash
    > ./dotnet-checker -c 127.0.0.1:6379
    StringSet is normal.
    StringGet is normal, value is OK.
    KeyDelete is normal.
    
    > ./dotnet-checker redis get test -c 127.0.0.1:6379
    test
    ```

  - Check mongo
  
    ```bash
    > ./dotnet-checker mongo -c mongodb://127.0.0.1:27017/test -d test
    InsertOne is normal.
    Find is normal. The value is { "_id" : ObjectId("5efb2db48278cb7ff8ba2fa2"), "id" : "0ed4c50c-d9aa-44a5-8ec9-f8fa7b9b09fe", "name" : "dotnet-checker", "timestamp" : NumberLong(1593519540) }
    DeleteOne is normal.
    DropCollection is normal.
    ```
    
## Best Practice

1. Install [MinIO](https://min.io/)  
2. Upload dotnet-checker to [MinIO](https://min.io/)    
3. Download dotnet-checker in your server or docker container  
4. Execute your checking
