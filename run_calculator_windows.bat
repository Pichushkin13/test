@echo off
setlocal

REM Usage:
REM   run_calculator_windows.bat 10 + 5

if "%~1"=="" goto :usage
if "%~2"=="" goto :usage
if "%~3"=="" goto :usage

where py >nul 2>nul
if %ERRORLEVEL%==0 (
    py calculator.py %1 "%~2" %3
) else (
    python calculator.py %1 "%~2" %3
)

exit /b %ERRORLEVEL%

:usage
echo Usage: run_calculator_windows.bat ^<a^> ^<op^> ^<b^>
echo Example: run_calculator_windows.bat 10 + 5
exit /b 1
