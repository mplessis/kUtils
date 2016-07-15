using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kopigi.Net45.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Net45.Cryptography
{
    [TestClass]
    public class Sha256Tests
    {
        [TestMethod]
        public void GenerateSha256()
        {
            Assert.AreEqual("8fe7416b1c5f60493c617536469aca0a24a29353636520c130ec625ef0c33e68", Sha256.HashValue("07101981"));
        }
    }
}
