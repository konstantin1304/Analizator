using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizator
{
    class Program
    {
        static void Main(string[] args)
        {
            string Expr = "((2+2)*2+2*(2+2*(2+2))+2*2)+2+2*2*2+2";
            Console.WriteLine(Expr+ "=" + MathExpression.Solve(Expr));//44

            Expr = "2+2*(2+2*2+(2+2*(2+2)))";
            Console.WriteLine(Expr + "=" + MathExpression.Solve(Expr));//34

            Expr = "(((25+227)/2)+134)*2+((48+2)*(2+2))";
            Console.WriteLine(Expr + "=" + MathExpression.Solve(Expr));//720


            Console.ReadLine();
            
        }
    }
}
