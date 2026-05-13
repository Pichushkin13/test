#!/usr/bin/env python3
"""Simple Windows-style calculator GUI with robust startup diagnostics."""

from __future__ import annotations

import datetime as dt
import traceback
from pathlib import Path
import tkinter as tk
from tkinter import messagebox, ttk


LOG_PATH = Path.home() / "BeautifulCalculator_error.log"


class CalculatorApp:
    def __init__(self, root: tk.Tk) -> None:
        self.root = root
        self.root.title("Calculator")
        self.root.geometry("360x520")
        self.root.minsize(320, 460)

        self.expression = ""
        self.display_value = tk.StringVar(value="0")

        self._build_ui()

    def _build_ui(self) -> None:
        self.root.configure(bg="#1f1f1f")
        container = ttk.Frame(self.root, padding=12)
        container.pack(fill="both", expand=True)

        style = ttk.Style()
        style.theme_use("clam")
        style.configure(
            "Display.TLabel",
            background="#1f1f1f",
            foreground="#ffffff",
            font=("Segoe UI", 28),
            anchor="e",
        )
        style.configure("Calc.TButton", font=("Segoe UI", 14), padding=8)

        display = ttk.Label(container, textvariable=self.display_value, style="Display.TLabel")
        display.pack(fill="x", pady=(0, 12))

        grid = ttk.Frame(container)
        grid.pack(fill="both", expand=True)

        buttons = [
            ("C", 0, 0, self.clear),
            ("⌫", 0, 1, self.backspace),
            ("%", 0, 2, lambda: self.append("%")),
            ("/", 0, 3, lambda: self.append("/")),
            ("7", 1, 0, lambda: self.append("7")),
            ("8", 1, 1, lambda: self.append("8")),
            ("9", 1, 2, lambda: self.append("9")),
            ("*", 1, 3, lambda: self.append("*")),
            ("4", 2, 0, lambda: self.append("4")),
            ("5", 2, 1, lambda: self.append("5")),
            ("6", 2, 2, lambda: self.append("6")),
            ("-", 2, 3, lambda: self.append("-")),
            ("1", 3, 0, lambda: self.append("1")),
            ("2", 3, 1, lambda: self.append("2")),
            ("3", 3, 2, lambda: self.append("3")),
            ("+", 3, 3, lambda: self.append("+")),
            ("+/-", 4, 0, self.negate),
            ("0", 4, 1, lambda: self.append("0")),
            (".", 4, 2, lambda: self.append(".")),
            ("=", 4, 3, self.evaluate),
        ]

        for text, row, col, command in buttons:
            btn = ttk.Button(grid, text=text, command=command, style="Calc.TButton")
            btn.grid(row=row, column=col, sticky="nsew", padx=4, pady=4)

        for i in range(5):
            grid.rowconfigure(i, weight=1)
        for i in range(4):
            grid.columnconfigure(i, weight=1)

    def append(self, value: str) -> None:
        if self.display_value.get() == "0" and value.isdigit():
            self.expression = value
        else:
            self.expression += value
        self.display_value.set(self.expression or "0")

    def clear(self) -> None:
        self.expression = ""
        self.display_value.set("0")

    def backspace(self) -> None:
        self.expression = self.expression[:-1]
        self.display_value.set(self.expression or "0")

    def negate(self) -> None:
        if not self.expression:
            self.expression = "-"
        elif self.expression.startswith("-"):
            self.expression = self.expression[1:]
        else:
            self.expression = f"-{self.expression}"
        self.display_value.set(self.expression or "0")

    def evaluate(self) -> None:
        try:
            result = eval(self.expression.replace("%", "/100"), {"__builtins__": {}}, {})
            if isinstance(result, float) and result.is_integer():
                result = int(result)
            self.expression = str(result)
            self.display_value.set(self.expression)
        except Exception:
            self.expression = ""
            self.display_value.set("Error")


def _write_error_log(exc: BaseException) -> Path:
    details = "\n".join(
        [
            f"[{dt.datetime.now().isoformat()}] Startup failure:",
            "",
            "".join(traceback.format_exception(type(exc), exc, exc.__traceback__)),
        ]
    )
    LOG_PATH.write_text(details, encoding="utf-8")
    return LOG_PATH


def main() -> None:
    try:
        root = tk.Tk()
        CalculatorApp(root)
        root.mainloop()
    except Exception as exc:
        log_path = _write_error_log(exc)
        try:
            fallback = tk.Tk()
            fallback.withdraw()
            messagebox.showerror(
                "BeautifulCalculator error",
                "Приложение не запустилось.\n"
                f"Лог сохранен: {log_path}",
            )
            fallback.destroy()
        except Exception:
            pass


if __name__ == "__main__":
    main()
