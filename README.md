# Calculator

## Готовый `.exe` (без ручной сборки локально)

Я добавил автоматическую сборку `.exe` через GitHub Actions.
После запуска workflow вы сможете скачать уже готовый `calculator.exe` как artifact.

Файл workflow:
- `.github/workflows/build-windows-exe.yml`

### Как получить готовый EXE

1. Откройте репозиторий на GitHub.
2. Перейдите во вкладку **Actions**.
3. Выберите workflow **Build Windows EXE**.
4. Нажмите **Run workflow**.
5. После завершения откройте запуск и скачайте artifact `calculator-exe`.
6. Внутри будет готовый файл `calculator.exe`.

Запуск:

```powershell
.\calculator.exe 10 + 5
```

## Почему я не приложил EXE прямо в этом коммите

В этой среде нет доступа к установке `pyinstaller` из сети (ошибка доступа к пакетному индексу),
поэтому собрать бинарник здесь напрямую не получилось.
Workflow собирает EXE на `windows-latest` в GitHub, где это ограничение обычно отсутствует.

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

## Вариант 2: EXE через локальную сборку

Если вы всё же хотите собрать локально, есть скрипт `build_windows_exe.ps1`:

```powershell
powershell -ExecutionPolicy Bypass -File .\build_windows_exe.ps1
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
