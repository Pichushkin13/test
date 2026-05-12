Param(
    [string]$OutputDir = "dist"
)

$ErrorActionPreference = "Stop"

if (-not (Get-Command py -ErrorAction SilentlyContinue) -and -not (Get-Command python -ErrorAction SilentlyContinue)) {
    throw "Python is not installed. Install Python first to build EXE."
}

$pythonCmd = if (Get-Command py -ErrorAction SilentlyContinue) { "py" } else { "python" }

& $pythonCmd -m pip install --upgrade pip pyinstaller
& $pythonCmd -m PyInstaller --onefile --name calculator calculator.py

Write-Host "EXE build complete. Output: .\\$OutputDir\\calculator.exe"
