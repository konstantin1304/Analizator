using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizator
{
    static class MathExpression
    {
        /// <summary>
        /// Словарь приоритетов операторов
        /// </summary>
        private static Dictionary<char, int> OperatorPriority;
        /// <summary>
        /// Статический конструктор
        /// </summary>
        static MathExpression()
        {
            OperatorPriority = new Dictionary<char, int>();
            OperatorPriority.Add('(', 10);
            OperatorPriority.Add(')', 7);
            OperatorPriority.Add('*', 9);
            OperatorPriority.Add('/', 9);
            OperatorPriority.Add('+', 8);
            OperatorPriority.Add('-', 8);
        }
        /// <summary>
        /// Решает математическое выражение
        /// </summary>
        /// <param name="Expr">Выражение</param>
        /// <returns></returns>
        public static double Solve(string Expr)
        {
            Stack<double> numbers = new Stack<double>();
            Stack<char> operators = new Stack<char>();
            string dig = "";
            Expr = "(" + Expr.Replace(" ", "") + ")";
            for (int i = 0; i < Expr.Length; i++)
            {
                char c = Expr[i];
                if (c >= '0' && c <= '9' || c == '.')
                {
                    dig += c;
                }
                else 
                {
                    //Обрабатывает последнюю цифру числа, если таковая была
                    if (dig != "")
                    {
                        AddNumber(Convert.ToDouble(dig), numbers, operators);
                    }
                    dig = "";
                    if (!OperatorPriority.ContainsKey(c))
                    {
                        throw new Exception("Обнаружен неизвестный оператор");
                    }
                    int priority = OperatorPriority[c];
                    if (c == ')')
                    {
                        CollapseStacks(numbers, operators, priority);

                        if (operators.Count == 0 || operators.Pop() != '(')
                        {
                            throw new Exception("Неверный баланс скобок");
                        }

                        continue;
                    }
                    else
                    if (operators.Count > 0 && priority < OperatorPriority[operators.Peek()])
                    {
                        CollapseStacks(numbers, operators, priority);
                    }

                    operators.Push(c);
                }
            }

            if ((numbers.Count == 1) && (operators.Count == 0))
            {
                return numbers.Peek();
            }
            throw new Exception($"Ошибка при обработке стеков чисел или операторов, количество чисел {numbers.Count}, количество операторов {operators.Count}");

        }
        /// <summary>
        /// Добавляет число в числовой стек, модифицирует операторы
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="numbers">Стек чисел</param>
        /// <param name="operators">Стек операторов</param>
        private static void AddNumber(double number, Stack<double> numbers, Stack<char> operators)
        {
            if (operators.Peek() == '-')
            {
                numbers.Push(-number);
                operators.Pop();
                operators.Push('+');
            }
            else if (operators.Peek() == '/')
            {
                numbers.Push(1 / number);
                operators.Pop();
                operators.Push('*');
            }
            else
            {
                numbers.Push(number);
            }
        }
        /// <summary>
        /// Разбирает стек пока не найдет оператор с завершающим приоритет
        /// </summary>
        /// <param name="numbers">Стек чисел</param>
        /// <param name="operators">Стек операторов</param>
        /// <param name="breakPriority">Приоритет оператора инициировавшего разборку стека</param>
        private static void CollapseStacks(Stack<double> numbers, Stack<char> operators, int breakPriority)
        {
            while (true)
            {
                if (operators.Count == 0)
                {
                    if (numbers.Count > 1)
                    {
                        throw new Exception("Обнаружено два или более чисел при пустом стеке операторов");
                    }
                    return;
                }
                if (numbers.Count == 0)
                {
                    return;
                }
                if (numbers.Count < 2)
                {
                    if (operators.Count > 0 && operators.Peek() != '(')
                        throw new Exception("Найдено меньше двух чисел, при бинарном операторе");

                    return;
                }

                //Если находим оператор, приоритет которого меньше либо равен текущему(т. е. последовательность приоритетов операторов становится возрастающей)
                if (OperatorPriority[operators.Peek()] <= breakPriority || operators.Peek() == '(')
                {
                    return;
                }

                char op = operators.Pop();
                double b = numbers.Pop();
                double a = numbers.Pop();

                switch (op)
                {
                    case '+':
                        numbers.Push(a + b);
                        break;
                    case '-':
                        numbers.Push(a - b);
                        break;
                    case '*':
                        numbers.Push(a * b);
                        break;
                    case '/':
                        numbers.Push(a / b);
                        break;
                    default:
                        throw new Exception("Неизвестный оператор");
                }
            }
        }

    }
}
