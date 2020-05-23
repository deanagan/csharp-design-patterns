using System.Linq;
using System.Collections.Generic;

namespace Interpreter
{
    public class RomanNumeralInterpreter : IInterpreter
    {
        private IDictionary<string, int> _lookup = new Dictionary<string, int> {
            {"M", 1000},
            {"CM", 900},
            {"D", 500},
            {"CD", 400},
            {"C", 100},
            {"XC", 90},
            {"L", 50 },
            {"XL", 40},
            {"X", 10},
            {"IX", 9},
            {"V", 5},
            {"IV", 4},
            {"I", 1},
        };

        public int Interpret(string expression)
        {
            var expressionStack = new Stack<string>();
            var result = 0;
            var lastNumericValue = 0;
            var repetitionStack = new Stack<char>();

            foreach (var ch in expression)
            {
                var numericValue = _lookup[ch.ToString()];

                if (repetitionStack.Count == 0 || repetitionStack.Peek() != ch)
                {
                    repetitionStack.Clear();
                    repetitionStack.Push(ch);
                }
                else if (repetitionStack.Count < 3)
                {
                    repetitionStack.Push(ch);
                }
                else
                {
                    return 0;
                }

                if (lastNumericValue == 0 || lastNumericValue >= numericValue)
                {
                    result += numericValue;
                    expressionStack.Push(ch.ToString());
                    lastNumericValue = numericValue;
                }
                else
                {
                    if (expressionStack.Count == 0)
                    {
                        return 0;
                    }
                    var lookUpStr = expressionStack.Pop().ToString() + ch.ToString();

                    if (_lookup.ContainsKey(lookUpStr))
                    {
                        result += (_lookup[lookUpStr]) - lastNumericValue;
                        lastNumericValue = _lookup[lookUpStr];
                    }
                    else
                    {
                        return 0;
                    }
                }

                if (result >= 4000)
                {
                    return 0;
                }
            }

            if (lastNumericValue >= 10)
            {
                return 0;
            }
            return result;
        }
    }
}
