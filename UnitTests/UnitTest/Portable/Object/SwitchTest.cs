using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kopigi.Portable.Object;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Portable.Object
{
    [TestClass]
    public class SwitchTest
    {
        [TestMethod]
        public void SwitchCase()
        {
            var objectTest = 2;
            new Switch(objectTest)
                .Case<string>(
                    a =>
                    {
                        Assert.AreSame(objectTest.GetType(), typeof (string));
                    }
                )
                .CaseValueType<int>(
                    a =>
                    {
                        Assert.AreSame(objectTest.GetType(), typeof(int));
                    }
                );
        }
    }
}
