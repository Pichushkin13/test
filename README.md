# Beautiful Calculator (Windows Native App)

Проект переписан на **C# / WinForms** для более предсказуемого запуска на Windows.
Итоговый файл — **самодостаточный EXE** (self-contained), Python не нужен.

## Почему это стабильнее

- Приложение нативно для Windows (.NET WinForms).
- Сборка делает self-contained single-file EXE.
- На ПК пользователя не требуется Python/pyinstaller.

## Как получить готовый EXE

1. Откройте репозиторий на GitHub.
2. Перейдите в **Actions**.
3. Запустите workflow **Build Windows Calculator EXE**.
4. Скачайте artifact `BeautifulCalculator-exe`.
5. Запустите `BeautifulCalculator.exe`.

## Что в репозитории

- `src/Program.cs` — GUI-калькулятор (Windows Forms).
- `src/BeautifulCalculator.csproj` — конфигурация .NET проекта.
- `.github/workflows/build-windows-exe.yml` — CI-сборка self-contained EXE.
