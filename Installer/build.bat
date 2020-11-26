@echo off
@title Windows Installer using WiX
@if exist RSSNewsReader.wixobj del RSSNewsReader.wixobj
@"%wix%\bin\candle" RSSNewsReader.wxs -nologo -ext WixNetfxExtension -ext WixUtilExtension -ext WixUiExtension
@if exist RSSNewsReader.msi del RSSNewsReader.msi 
@"%wix%\bin\light" RSSNewsReader.wixobj -spdb -sice:ICE91 -nologo -ext WixNetfxExtension -ext WixUtilExtension -ext wixTagExtension -ext WixUiExtension
@pause

