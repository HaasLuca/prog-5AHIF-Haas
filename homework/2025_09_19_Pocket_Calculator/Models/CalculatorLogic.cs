using System;
using System.Data;
using System.Text.RegularExpressions;

namespace _2025_09_19_Pocket_Calculator.Models;

public class CalculatorLogic
{
    public static int CalculateFromString(string expression)
    {
        int num1 = 0;
        int num2 = 0;
        bool minus = false;
        bool wasLastSign = true;
        char sign = '+';

        for (int i = 0; i < expression.Length; i++)
        {
            if ("+-*/".Contains(expression[i]))
            {
                if (wasLastSign && expression[i] == '-')
                {
                    minus = !minus;
                }
                else if (wasLastSign)
                {
                    throw new SyntaxErrorException();
                }
                else
                {
                    sign = expression[i];
                    minus = false;
                    wasLastSign = true;
                }
            }
            else
            {
                num2 *= 10;
                num2 += expression[i] - '0';
                wasLastSign = false;
                if (i == expression.Length - 1 || "+-*/".Contains(expression[i + 1]))
                {
                    if (minus)
                    {
                        num2 *= -1;
                    }
                    switch (sign)
                    {
                        case '+':
                            num1 += num2;
                            break;
                        case '-':
                            num1 -= num2;
                            break;
                        case '*':
                            num1 *= num2;
                            break;
                        case '/':
                            if (num2 == 0) throw new DivideByZeroException();
                            num1 /= num2;
                            break;
                    }
                    num2 = 0;
                }
            }
        }
        
        return num1;
    }
}