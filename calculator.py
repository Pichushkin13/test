#!/usr/bin/env python3
"""Простой CLI-калькулятор."""

from __future__ import annotations

import argparse
import operator


OPERATIONS = {
    "+": operator.add,
    "-": operator.sub,
    "*": operator.mul,
    "/": operator.truediv,
}


def calculate(a: float, op: str, b: float) -> float:
    """Вычисляет результат бинарной операции.

    Args:
        a: Первое число.
        op: Оператор (+, -, *, /).
        b: Второе число.

    Returns:
        Результат вычисления.

    Raises:
        ValueError: Если оператор не поддерживается.
        ZeroDivisionError: При делении на ноль.
    """
    if op not in OPERATIONS:
        raise ValueError(f"Неподдерживаемая операция: {op}")
    return OPERATIONS[op](a, b)


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(
        description="Калькулятор: выполняет операцию над двумя числами"
    )
    parser.add_argument("a", type=float, help="первое число")
    parser.add_argument("op", choices=OPERATIONS.keys(), help="операция")
    parser.add_argument("b", type=float, help="второе число")
    return parser.parse_args()


def main() -> None:
    args = parse_args()
    result = calculate(args.a, args.op, args.b)
    print(result)


if __name__ == "__main__":
    main()
