# Calculator

## Почему окно сразу закрывается

Если `.bat` запускать двойным кликом, окно могло закрываться до того, как вы успеваете увидеть результат.
Теперь `portable_calculator_windows.bat` работает в интерактивном режиме и ставит `pause`, поэтому результат остаётся на экране.

## Вариант 1: без установки Python (BAT)

Запустите файл `portable_calculator_windows.bat` двойным кликом.
Он спросит:
- первое число,
- операцию,
- второе число,

и покажет результат.

Также можно запускать аргументами:

```bat
portable_calculator_windows.bat 10 + 5
```

> Ограничение BAT-варианта: только целые числа.

## Вариант 2: EXE файл

Если нужен именно `.exe`, добавлен скрипт сборки `build_windows_exe.ps1`.
Он создаёт `dist\calculator.exe` через PyInstaller.

### Как собрать EXE

```powershell
powershell -ExecutionPolicy Bypass -File .\build_windows_exe.ps1
```

После сборки запускайте:

```powershell
.\dist\calculator.exe 10 + 5
```

## Python-версия (если Python уже установлен)

```powershell
py calculator.py 10 "+" 5
```

## Linux / macOS

```bash
python3 calculator.py 10 '+' 5
```

## Тесты Python-версии

```bash
python -m pytest -q
```
