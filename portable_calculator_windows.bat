@echo off
setlocal ENABLEEXTENSIONS

REM Portable Windows calculator with no extra dependencies.
REM Supports integer operations: + - * /
REM Usage:
REM   portable_calculator_windows.bat 10 + 5

if "%~1"=="" goto :usage
if "%~2"=="" goto :usage
if "%~3"=="" goto :usage

set "A=%~1"
set "OP=%~2"
set "B=%~3"

if not "%~4"=="" goto :usage

if "%OP%"=="+" goto :calc
if "%OP%"=="-" goto :calc
if "%OP%"=="*" goto :calc
if "%OP%"=="/" goto :check_div

echo Error: unsupported operation "%OP%".
exit /b 1

:check_div
if "%B%"=="0" (
    echo Error: division by zero.
    exit /b 1
)

:calc
set /a RESULT=%A% %OP% %B% 2>nul
if errorlevel 1 (
    echo Error: use integer numbers only. Example: 10 + 5
    exit /b 1
)

echo %RESULT%
exit /b 0

:usage
echo Usage: portable_calculator_windows.bat ^<int_a^> ^<op^> ^<int_b^>
echo Example: portable_calculator_windows.bat 10 + 5
echo Supported operations: + - * /
exit /b 1
