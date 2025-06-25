@echo off
set path1=%~dp0SGTV2\Resources\Resources.hjson
set path2=%~dp0SGTV2\Generated
set path3=%~dp0SGTV2\
echo %path1%
echo %path2%
echo %path3%

D:\Github\OpenGLTests\OpenglTestConsole\ResourcesClassGenerator\bin\Debug\net8.0\ResourcesClassGenerator.exe %path1% %path2% %path3% "OpenglTestConsole" "AppResources"

