using Microsoft.VisualStudio.TestTools.UnitTesting;
using Analizator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizator.Tests
{
    [TestClass()]
    public class MathExpressionTests
    {
        [TestMethod()]
        public void SolveTest()
        {
            Assert.AreEqual(MathExpression.Solve("((2+2)*2+2*(2+2*(2+2))+2*2)+2+2*2*2+2"), 44);
            Assert.AreEqual(MathExpression.Solve("2+2*(2+2*2+(2+2*(2+2)))"), 34);
            Assert.AreEqual(MathExpression.Solve("(((25+227)/2)+134)*2+((48+2)*(2+2))"), 720);
            Assert.AreEqual(MathExpression.Solve("4*2+3-78+16*(21*3-7)+88/4"),851);

        }

    }
}