# Beautiful Calculator (Windows GUI)

Это GUI-калькулятор в стиле Windows (базовые функции: `+`, `-`, `*`, `/`, `%`, `+/-`, `.`).

## Если EXE не открывается

Симптом «мелькает консоль и ничего не происходит» обычно связан с:
- блокировкой антивирусом/SmartScreen,
- проблемой распаковки `--onefile` EXE,
- падением приложения до отображения окна.

Я добавил более стабильную схему запуска:
1. `BeautifulCalculator.exe` (`--onefile`).
2. `BeautifulCalculator_portable.zip` (`--onedir`) — fallback, обычно стабильнее на некоторых ПК.

Также добавлено логирование ошибок запуска в файл:
- `%USERPROFILE%\BeautifulCalculator_error.log`

## Как получить и запустить на Windows

1. Откройте GitHub → **Actions**.
2. Запустите workflow **Build Windows Calculator EXE**.
3. Скачайте artifact `BeautifulCalculator-builds`.
4. Сначала попробуйте `BeautifulCalculator.exe`.
5. Если не запускается — распакуйте `BeautifulCalculator_portable.zip` и запустите `BeautifulCalculator_portable.exe`.

## Что есть в репозитории

- `calculator_gui.py` — GUI-калькулятор.
- `.github/workflows/build-windows-exe.yml` — сборка двух вариантов EXE (onefile + onedir fallback).
