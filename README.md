# Calculator

Простой калькулятор на Python с CLI.

## Готовый файл для Windows

В репозитории есть готовый файл `run_calculator_windows.bat`, который можно запускать сразу из CMD/PowerShell.  
Пример:

```bat
run_calculator_windows.bat 10 + 5
```

Скрипт автоматически пробует `py`, а если его нет — `python`.

## Ручной запуск на Windows

```powershell
py calculator.py 10 "+" 5
```

Если `py` не работает:

```powershell
python calculator.py 10 "+" 5
```

Доступные операции: `+`, `-`, `*`, `/`.

## Linux / macOS

```bash
python3 calculator.py 10 '+' 5
```

## Тесты

```bash
python -m pytest -q
```
