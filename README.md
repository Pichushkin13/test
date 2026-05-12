# Calculator

## Запуск на "неподготовленном" Windows (без установки Python)

Если на компьютере нет Python и ничего нельзя устанавливать, используйте файл:

- `portable_calculator_windows.bat`

Это полностью автономный BAT-скрипт (работает в стандартном CMD/PowerShell Windows).

Пример запуска:

```bat
portable_calculator_windows.bat 10 + 5
```

> Ограничение автономного BAT-варианта: поддерживаются только **целые числа**.

## Вариант на Python (если Python уже установлен)

- `calculator.py`
- `run_calculator_windows.bat`

Пример:

```bat
run_calculator_windows.bat 10 + 5
```

## Linux / macOS

```bash
python3 calculator.py 10 '+' 5
```

## Тесты Python-версии

```bash
python -m pytest -q
```
