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
    public class Base64Tests
    {
        [TestMethod]
        public void GenerateToBase64()
        {
            Assert.AreEqual("VGVzdCBCYXNlIDY0", Base64.Encode("Test Base 64"));
        }

        [TestMethod]
        public void GenerateFromBase64()
        {
            Assert.AreEqual("Test Base 64", Base64.Decode("VGVzdCBCYXNlIDY0"));
        }
    }
}
