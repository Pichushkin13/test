using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace BeautifulCalculator;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new CalculatorForm());
    }
}

public sealed class CalculatorForm : Form
{
    private readonly TextBox _display;
    private readonly StringBuilder _expression = new();

    public CalculatorForm()
    {
        Text = "Beautiful Calculator";
        Width = 360;
        Height = 560;
        MinimumSize = new Size(320, 500);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(31, 31, 31);

        _display = new TextBox
        {
            ReadOnly = true,
            Text = "0",
            Dock = DockStyle.Top,
            Height = 80,
            Font = new Font("Segoe UI", 24, FontStyle.Regular),
            TextAlign = HorizontalAlignment.Right,
            BackColor = Color.FromArgb(31, 31, 31),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.None,
        };

        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 5,
            Padding = new Padding(8),
            BackColor = Color.FromArgb(31, 31, 31),
        };

        for (var i = 0; i < 4; i++) panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        for (var i = 0; i < 5; i++) panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

        Controls.Add(panel);
        Controls.Add(_display);

        AddButton(panel, "C", 0, 0, (_, _) => ClearDisplay());
        AddButton(panel, "⌫", 0, 1, (_, _) => Backspace());
        AddButton(panel, "%", 0, 2, (_, _) => AppendOperator("%"));
        AddButton(panel, "/", 0, 3, (_, _) => AppendOperator("/"));

        AddButton(panel, "7", 1, 0, (_, _) => Append("7"));
        AddButton(panel, "8", 1, 1, (_, _) => Append("8"));
        AddButton(panel, "9", 1, 2, (_, _) => Append("9"));
        AddButton(panel, "*", 1, 3, (_, _) => AppendOperator("*"));

        AddButton(panel, "4", 2, 0, (_, _) => Append("4"));
        AddButton(panel, "5", 2, 1, (_, _) => Append("5"));
        AddButton(panel, "6", 2, 2, (_, _) => Append("6"));
        AddButton(panel, "-", 2, 3, (_, _) => AppendOperator("-"));

        AddButton(panel, "1", 3, 0, (_, _) => Append("1"));
        AddButton(panel, "2", 3, 1, (_, _) => Append("2"));
        AddButton(panel, "3", 3, 2, (_, _) => Append("3"));
        AddButton(panel, "+", 3, 3, (_, _) => AppendOperator("+"));

        AddButton(panel, "+/-", 4, 0, (_, _) => ToggleSign());
        AddButton(panel, "0", 4, 1, (_, _) => Append("0"));
        AddButton(panel, ".", 4, 2, (_, _) => AppendDecimalPoint());
        AddButton(panel, "=", 4, 3, (_, _) => Evaluate());
    }

    private void AddButton(TableLayoutPanel panel, string text, int row, int col, EventHandler onClick)
    {
        var button = new Button
        {
            Text = text,
            Dock = DockStyle.Fill,
            Margin = new Padding(4),
            Font = new Font("Segoe UI", 14, FontStyle.Regular),
            BackColor = Color.FromArgb(45, 45, 45),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
        };
        button.FlatAppearance.BorderSize = 0;
        button.Click += onClick;
        panel.Controls.Add(button, col, row);
    }

    private void Append(string value)
    {
        if (_display.Text == "0" && value == "0" && _expression.Length == 0) return;
        _expression.Append(value);
        UpdateDisplay();
    }

    private void AppendOperator(string op)
    {
        if (_expression.Length == 0)
        {
            if (op == "-")
            {
                _expression.Append(op);
                UpdateDisplay();
            }
            return;
        }

        var last = _expression[^1];
        if ("+-*/%".Contains(last))
        {
            _expression[^1] = op[0];
        }
        else
        {
            _expression.Append(op);
        }
        UpdateDisplay();
    }

    private void AppendDecimalPoint()
    {
        var currentNumber = GetCurrentNumberSegment();
        if (currentNumber.Contains('.')) return;

        if (currentNumber.Length == 0) _expression.Append('0');
        _expression.Append('.');
        UpdateDisplay();
    }

    private string GetCurrentNumberSegment()
    {
        var expr = _expression.ToString();
        var idx = expr.LastIndexOfAny(new[] { '+', '-', '*', '/', '%' });
        return idx >= 0 ? expr[(idx + 1)..] : expr;
    }

    private void ToggleSign()
    {
        if (_expression.Length == 0)
        {
            _expression.Append('-');
            UpdateDisplay();
            return;
        }

        if (_expression[0] == '-') _expression.Remove(0, 1);
        else _expression.Insert(0, '-');

        UpdateDisplay();
    }

    private void Backspace()
    {
        if (_expression.Length > 0) _expression.Remove(_expression.Length - 1, 1);
        UpdateDisplay();
    }

    private void ClearDisplay()
    {
        _expression.Clear();
        UpdateDisplay();
    }

    private void Evaluate()
    {
        try
        {
            var result = EvaluateExpression(_expression.ToString());
            _expression.Clear();
            _expression.Append(result.ToString(CultureInfo.InvariantCulture));
            UpdateDisplay();
        }
        catch
        {
            _expression.Clear();
            _display.Text = "Error";
        }
    }

    private static double EvaluateExpression(string expr)
    {
        if (string.IsNullOrWhiteSpace(expr)) return 0;

        var normalized = expr.Replace("%", "*0.01", StringComparison.Ordinal);
        var tokens = Tokenize(normalized);
        return ComputeTokens(tokens);
    }

    private static List<string> Tokenize(string expr)
    {
        var tokens = new List<string>();
        var num = new StringBuilder();

        for (var i = 0; i < expr.Length; i++)
        {
            var ch = expr[i];
            if (char.IsDigit(ch) || ch == '.')
            {
                num.Append(ch);
            }
            else if ("+-*/".Contains(ch))
            {
                if (ch == '-' && (i == 0 || "+-*/".Contains(expr[i - 1])))
                {
                    num.Append(ch);
                    continue;
                }

                FlushNumber(tokens, num);
                tokens.Add(ch.ToString());
            }
            else if (!char.IsWhiteSpace(ch))
            {
                throw new InvalidOperationException("Unsupported character.");
            }
        }

        FlushNumber(tokens, num);
        return tokens;
    }

    private static void FlushNumber(List<string> tokens, StringBuilder num)
    {
        if (num.Length == 0) return;
        tokens.Add(num.ToString());
        num.Clear();
    }

    private static double ComputeTokens(List<string> tokens)
    {
        if (tokens.Count == 0) return 0;

        var stage = new List<string>();
        var i = 0;
        while (i < tokens.Count)
        {
            if (i + 2 < tokens.Count && (tokens[i + 1] == "*" || tokens[i + 1] == "/"))
            {
                var left = Parse(stage.Count > 0 ? stage[^1] : tokens[i]);
                var right = Parse(tokens[i + 2]);
                var value = tokens[i + 1] == "*" ? left * right : left / right;

                if (stage.Count > 0) stage[^1] = value.ToString(CultureInfo.InvariantCulture);
                else stage.Add(value.ToString(CultureInfo.InvariantCulture));

                i += 3;
            }
            else
            {
                stage.Add(tokens[i]);
                i++;
            }
        }

        var result = Parse(stage[0]);
        for (var j = 1; j < stage.Count; j += 2)
        {
            var op = stage[j];
            var val = Parse(stage[j + 1]);
            result = op == "+" ? result + val : result - val;
        }

        return result;
    }

    private static double Parse(string value) => double.Parse(value, CultureInfo.InvariantCulture);

    private void UpdateDisplay()
    {
        _display.Text = _expression.Length == 0 ? "0" : _expression.ToString();
    }
}
