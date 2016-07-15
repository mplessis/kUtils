using Kopigi.Net45.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Net45.Cryptography
{
    [TestClass]
    public class Md5Tests
    {
        [TestMethod]
        public void GenerateToMd5()
        {
            Assert.AreEqual("5c9965f4dcaa6e6278eb379ecaf9918a", Md5.HashMd5("CP/3491"));
        }

        [TestMethod]
        public void CheckFromMd5()
        {
            Assert.AreEqual(true, Md5.CheckMd5("CP/3491", "5c9965f4dcaa6e6278eb379ecaf9918a"));
        }
    }
}
