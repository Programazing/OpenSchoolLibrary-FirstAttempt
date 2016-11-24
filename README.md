# Open School Library

Open School Library is an Open Source library management web application geared towards small private schools build using ASP.NET Core.

## Requirements

- .Net Core 1.0.1
- An SQL Server.
- A Web Server

## [Installation](#installation-instructions)

**From Cloned Repository**

You will need an Internet Connection when both downloading the project and when building it.

- Open the project file in Visual Studio and wait for it to finish fetching the needed packages.
- Build the project and check for Warnings or Errors.
  - If you need help on an unsuccessful build please open an [Issue](
https://github.com/Programazing/Open-School-Library/issues)
- After a successful build navigate to where you downloaded the project folder. Once in the "Open School Library" folder that contains "project.json" make note of the folder path, open a command prompt and switch to that folder.
  - Tip: You can navigate one folder level above that "Open School Library" folder then hold shift and right click it. That will give you the option to "Open a Command Prompt Window Here" from the context menu.
- Type the following command within the command prompt remembering to replace
[Your-Path] with the folder path you wish to build the application in.
```
dotnet publish -o "[Your-Path] -c Release
```
- Once built navigate to [Your-Path] and run the following command to test if the project is working.
```
dotnet Open-School-Library.dll
```
**From Pre-Compiled Build**

- Unzip the project file.
- Navigate to the folder you unzipped the project to and make note of the folder path then open a command prompt and switch to that folder within the command prompt.
  - Tip: You can navigate one folder level above that folder then hold shift and right click it. That will give you the option to "Open a Command Prompt Window Here" from the context menu.
- Run the following command to test if the project is working.
```
dotnet Open-School-Library.dll
```

## Running

**Note:** Since there are multiple ways to deploy an ASP.NET Core application on Windows, Mac, and Linux this section will be ever changing.

**Portable: (Windows 10)**

After following the Installation Instructions all you can run the application from the command prompt by doing is the following:

- Make sure your web server is running
- Make sure your database server and database are accessible
 - If you haven't created a database yet the application will create one for you provided it can connect to your database server.
- Open "appsettings.json" and change the "DefaultConnection" string to use your connection string.
 - If you don't change the "DefaultConnection" string the application will try to create a database using MSSQLLocalDB.
- Navigate to the folder you unzipped the project to and make note of the folder path then open a command prompt and switch to that folder within the command prompt.
  - Tip: You can navigate one folder level above that folder then hold shift and right click it. That will give you the option to "Open a Command Prompt Window Here" from the context menu.
- Run the following command to start the application
```
dotnet Open-School-Library.dll
```
* Once the program is running it will list the internal address it is listening for.
 * Usually LocalHost:5000
