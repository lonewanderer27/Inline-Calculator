using System;

namespace Inline_Calculator
{
    internal class Program
    {
        private static readonly char[] Ops = {'+', '-', '*', '/'};
        private static char _op1 = ' ';
        private static int _op1Index;
        private static char _op2 = ' ';
        private static int _op2Index;
        private static int _foundOperators;
        private static double _operand1;
        private static double _operand2;
        private static double _operand3;

        private static void Test()
        {
            // Initiate numbers for testing
            var rnd = new Random();
            _operand1 = rnd.Next(11);
            _operand2 = rnd.Next(223);
            _operand3 = rnd.Next(3334);

            // Display numbers for testing
            Console.WriteLine($"Operand 1: {_operand1}");
            Console.WriteLine($"Operand 2: {_operand2}");
            Console.WriteLine($"Operand 3: {_operand3}");
            
            Console.WriteLine("\nTest Cases: ");
            foreach (var i in Ops)
            {
                foreach (var j in Ops)
                {
                    string equation = $"{_operand1} {i} {_operand2} {j} {_operand3}";
                    ParseEquation(equation);
                    Console.WriteLine($"{equation} = {CalcEquation()}");
                    _foundOperators = 0;
                }
            }
        }

        public static void Main(string[] args)
        {
            Test(); // for testing, uncomment this line and comment the rest below:
            
            // Console.Write("Enter equation [number operator number]: ");
            // string equation = Console.ReadLine();
            // ParseEquation(equation);
            //
            // // // Display all variables 
            // Console.WriteLine("\nEquation (as raw string): " + equation);
            // Console.WriteLine("\n1st Operand: " + _operand1);
            // Console.WriteLine("2nd Operand: " + _operand2);
            // Console.WriteLine("3rd Operand: " + _operand3);
            // Console.WriteLine("1st Operator: " + _op1 + " | index: " + _op1Index);
            // Console.WriteLine("2nd Operator: " + _op2 + " | index: " + _op2Index);
            //
            // double ans = CalcEquation(); // Calculate the equation
            //
            // if (_foundOperators.Equals(1))
            //     // Display answer for 2 equations
            //     Console.WriteLine($"\n{_operand1} {_op1} {_operand2} = {ans}");
            // else
            //     // Display answer for 3 equations
            //     Console.WriteLine($"\n{_operand1} {_op1} {_operand2} {_op2} {_operand3} = {ans}");
        }

        private static double CalcEquation()
        {
            double CalcTwoEquations(double operand12, double operand22, char op)
            {
                double ans2 = 0;
                switch (op)
                {
                    case '+': ans2 = operand12 + operand22;
                        break;
                    case '-': ans2 = operand12 - operand22;
                        break;
                    case '*': ans2 = operand12 * operand22;
                        break;
                    case '/': ans2 = operand12 / operand22;
                        break;
                }
                return ans2;
            }
            
            double CalcThreeEquations(double operand12, double operand22, double operand3, char op)
            {
                double ans2 = 0;
                switch (op)
                {
                    case '+': ans2 = operand12 + operand22 + operand3;
                        break;
                    case '-': ans2 = operand12 - operand22 - operand3;
                        break;
                    case '*': ans2 = operand12 * operand22 * operand3;
                        break;
                    case '/': ans2 = operand12 / operand22 / operand3;
                        break;
                }
                return ans2;
            }
            
            double ans = 0;
            // Console.WriteLine($"Operators: {_foundOperators}");
            switch (_foundOperators)
            {
                case 1: // 11+11
                    ans = CalcTwoEquations(_operand1, _operand2, _op1);
                    break;
                case 2:
                {
                    if (_op1 == _op2)
                    {
                        double oprd1AndOprd2AndOprd3Result = CalcThreeEquations(
                            _operand1, 
                            _operand2, 
                            _operand3,
                            _op1);
                        ans = oprd1AndOprd2AndOprd3Result;
                    }
                    else if (_op1 == '+' || _op1 == '-' && _op2 == '*' || _op2 == '/')
                    {
                        double operand2AndOperand3Result = CalcTwoEquations(_operand2, _operand3, _op2);
                        ans = CalcTwoEquations(_operand1, operand2AndOperand3Result, _op1);
                    }
                    else
                    {
                        double operand1AndOperand2Result = CalcTwoEquations(_operand1, _operand2, _op1);
                        ans = CalcTwoEquations(operand1AndOperand2Result, _operand3, _op2);
                    }

                    break;
                }
            }
            return ans;
        }

        private static void ParseEquation(string equation)
        {
            for (int i = 0; i < equation.Length; i++)
            {
                foreach (char op in Ops)
                {
                    // Console.WriteLine($"Comparing {op} with {equation[i]}");
                    if (op == equation[i])
                    {
                        // Console.WriteLine($"Found an operator: {equation[i]}");
                        _foundOperators++;
                        switch (_foundOperators)
                        {
                            case 1:
                            {
                                _op1 = op;
                                _op1Index = i;
                            }
                                break;
                            case 2:
                            {
                                _op2 = op;
                                _op2Index = i;
                            }
                                break;
                        }
                        break;
                    }
                }
            }
            _operand1 = Int32.Parse(equation.Substring(0, _op1Index));
            if (_op2 == ' ') // 11*11
                _operand2 = Int32.Parse(
                    equation.Substring(_op1Index + 1, equation.Length - (_op1Index + 1)).Trim());
            else if (_op2 != ' ') // 11*11+11
            {
                _operand2 = Int32.Parse(
                    equation.Substring(_op1Index + 1, _op2Index - (_op1Index + 1)).Trim());
                _operand3 = Int32.Parse(
                    equation.Substring(_op2Index + 1, equation.Length - (_op2Index + 1)).Trim());
            }
        }
    }
}

