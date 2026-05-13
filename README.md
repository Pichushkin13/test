# Beautiful Calculator (Windows Native App)

Проект на **C# / WinForms** для предсказуемого запуска на Windows.
Итоговый файл — **самодостаточный EXE** (self-contained), Python не нужен.

## Новый workflow (чтобы легко отличать в Actions)

Теперь workflow называется:

- **Build BeautifulCalculator EXE v2 (WinForms)**

И у каждого запуска будет понятный заголовок вида:

- `Build EXE v2 • <branch> • run #<number>`

Также артефакт переименован в:

- `BeautifulCalculator-exe-v2`

## Как получить готовый EXE

1. Откройте репозиторий на GitHub.
2. Перейдите в **Actions**.
3. Выберите workflow **Build BeautifulCalculator EXE v2 (WinForms)**.
4. Нажмите **Run workflow**.
5. Скачайте artifact `BeautifulCalculator-exe-v2`.
6. Запустите `BeautifulCalculator.exe`.

## Что в репозитории

- `src/Program.cs` — GUI-калькулятор (Windows Forms).
- `src/BeautifulCalculator.csproj` — конфигурация .NET проекта.
- `.github/workflows/build-windows-exe.yml` — CI-сборка self-contained EXE.
