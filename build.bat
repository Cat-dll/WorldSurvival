@ECHO OFF

set mode=%1
set arch=x64

IF NOT DEFINED mode (
    set mode=Debug
)

IF NOT EXIST ".\build\%mode%\net6.0-windows\Data\" (
    MKDIR .\build\%mode%\net6.0-windows\Data\ 
) 
XCOPY ".\Content\bin\Windows\" ".\build\%mode%\net6.0-windows\Data\" /y

ECHO [%mode%]: Building WorldSurvival.exe...
dotnet build -c %mode% 