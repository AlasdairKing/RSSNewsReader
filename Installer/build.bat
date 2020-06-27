@echo off
@title Windows Installer using WiX
@echo Copying files
@copy ..\RSSNewsReader\bin\Release\RSSNewsReader.exe SourceDir\RSSNewsReader.exe
@copy ..\RSSNewsReader\bin\Release\Common.Language.xml SourceDir\Common.Language.xml
@copy ..\RSSNewsReader\bin\Release\RSSNewsReader.exe.config SourceDir\RSSNewsReader.exe.config
@copy ..\RSSNewsReader\bin\Release\RSSNewsReader.Help-en.rtf SourceDir\RSSNewsReader.Help-en.rtf
@copy ..\RSSNewsReader\bin\Release\RSSNewsReader.Language.xml SourceDir\RSSNewsReader.Language.xml
@copy ..\RSSNewsReader\bin\Release\RSSNewsReader.ico SourceDir\RSSNewsReader.ico
@copy ..\RSSNewsReader\bin\Release\default.opml SourceDir\default.opml
@copy ..\RSSNewsReader\bin\Release\opmldoc.ico SourceDir\opmldoc.ico
@copy ..\RSSNewsReader\bin\Release\WebbIEUpdater.dll SourceDir\WebbIEUpdater.dll
@if exist RSSNewsReader.wixobj del RSSNewsReader.wixobj
@"%wix%\bin\candle" RSSNewsReader.wxs -nologo -ext WixNetfxExtension -ext WixUtilExtension -ext WixUiExtension
@if exist RSSNewsReader.msi del RSSNewsReader.msi 
@"%wix%\bin\light" RSSNewsReader.wixobj -spdb -sice:ICE91 -nologo -ext WixNetfxExtension -ext WixUtilExtension -ext wixTagExtension -ext WixUiExtension
@pause

