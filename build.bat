REM build script for MyGet builds

REM *** NOTE *** When running this locally, remove the quotes from around the executable variables (e.g. %GitPath%).
REM              MyGet requires the quotes, but the command line doesn't like them.  Don't ask me...

REM Start build
set config=%1
if "%config%" == "" (
   set config=Release
)
set PackageVersion=

REM Restore packages
call powershell "& .\nuget-restore.ps1"

REM Run build
call dotnet build Manatee.Trello.sln /p:Configuration="%config%" /m:1 /v:m /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
if not "%errorlevel%"=="0" goto failure

REM package
REM call dotnet publish Manatee.Trello\Manatee.Trello.csproj /t:pack /p:Configuration="%config%"
REM if not "%errorlevel%"=="0" goto failure

:success
exit 0

:failure
exit -1 
