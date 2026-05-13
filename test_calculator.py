import pytest

from calculator import calculate


def test_addition():
    assert calculate(2, "+", 3) == 5


def test_subtraction():
    assert calculate(5, "-", 2) == 3


def test_multiplication():
    assert calculate(4, "*", 2.5) == 10


def test_division():
    assert calculate(9, "/", 3) == 3


def test_unsupported_operation():
    with pytest.raises(ValueError):
        calculate(1, "%", 2)


def test_division_by_zero():
    with pytest.raises(ZeroDivisionError):
        calculate(1, "/", 0)
