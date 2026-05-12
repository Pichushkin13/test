@echo off
setlocal ENABLEEXTENSIONS

REM Double-click friendly portable Windows calculator (integer mode).
REM If arguments are omitted, enters interactive mode.

if "%~1"=="" goto :interactive
if "%~2"=="" goto :usage
if "%~3"=="" goto :usage
if not "%~4"=="" goto :usage

set "A=%~1"
set "OP=%~2"
set "B=%~3"
goto :compute

:interactive
cls
echo ==========================================
echo      Portable Windows Calculator
echo ==========================================
echo Supports integer operations: + - * /
echo.
set /p A=Enter first integer: 
set /p OP=Enter operation (+ - * /): 
set /p B=Enter second integer: 
echo.

:compute
if "%OP%"=="+" goto :check_numbers
if "%OP%"=="-" goto :check_numbers
if "%OP%"=="*" goto :check_numbers
if "%OP%"=="/" goto :check_div

echo Error: unsupported operation "%OP%".
goto :finish_error

:check_div
if "%B%"=="0" (
    echo Error: division by zero.
    goto :finish_error
)

:check_numbers
set /a RESULT=%A% %OP% %B% 2>nul
if errorlevel 1 (
    echo Error: use integer numbers only. Example: 10 + 5
    goto :finish_error
)

echo Result: %RESULT%
goto :finish_ok

:usage
echo Usage: portable_calculator_windows.bat ^<int_a^> ^<op^> ^<int_b^>
echo Example: portable_calculator_windows.bat 10 + 5
goto :finish_error

:finish_ok
echo.
pause
exit /b 0

:finish_error
echo.
pause
exit /b 1
