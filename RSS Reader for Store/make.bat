@echo off
rem 
rem This batch file converts a packages a Win32 application into an AppX for the Windows 10 Store. I don't know how to do MSIX yet, I guess kind of the same way. 
rem 
rem Alasdair, 27 June 2020.
rem
rem INSTRUCTIONS
rem 1 Install the Windows SDK. 10,  I guess.
rem 2 Update the sdkpath variable below to point to the folder with makeappx in it. (Why isn't the SDK bin on the system path? Too many paths nowadays, maybe?)
rem 3 Update the appname variable below.
rem 4 Make a local folder called "App" and put in it all the files for your application - EXEs, DLLs and so on. 
rem 5 Also in that folder you need an "AppxManifest.xml" file, which you will now have to open and edit to reflect this application. There is a publisher entry which starts CN which you get from the Windows Store (compulsory? If not, what do you put in here? Don't know.)
rem 6 All in that folder will be a folder called "Assets" which needs loads of PNG files that are used in the store and the installer. 

rem
set appname="RSS News Reader"
set sdkpath=C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x86
rem makeappx is found in the Windows SSK. At the time of writing it is in C:\Program Files (x86)\Windows Kits\10\bin.
rem "C:\Program Files (x86)\Windows Kits\10\bin\x64\makeappx.exe" pack /f mappings.ini /p %appname%.appx
"%sdkpath%\makeappx.exe" pack /d App /p %appname%.appx /o
rem We can now sign it with a developer licence key for testing or shipping to a site licencee, or (better) submit it to the Windows Store.
pause