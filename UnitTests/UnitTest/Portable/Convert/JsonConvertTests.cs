using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kopigi.Portable.Convert;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kopigi.Portable.Object;

namespace UnitTest.Portable.Convert
{
    [TestClass]
    public class JsonConvertTests
    {
        [TestMethod]
        public void ConvertToJson()
        {
            Assert.AreEqual("{\"Libelle\":\"test libellé d'objet\",\"Type\":2}", JsonConvert.Serialize(new TestObjectToConvert{Libelle = "test libellé d'objet", Type = 2}));
        }

        [TestMethod]
        public void ConvertFromJson()
        {
            var objectToCompare = new TestObjectToConvert { Libelle = "test libellé d'objet", Type = 2 };
            var o = JsonConvert.Deserialize<TestObjectToConvert>("{\"Libelle\":\"test libellé d'objet\",\"Type\":2}");
            Assert.AreEqual<TestObjectToConvert>(objectToCompare, JsonConvert.Deserialize<TestObjectToConvert>("{\"Libelle\":\"test libellé d'objet\",\"Type\":2}"));
        }

        [TestMethod]
        public void DynamicObjetConvert()
        {
            var jsonObject = new DynamicObjet();
            jsonObject.Add("Libelle", "test libellé d'objet");
            jsonObject.Add("Type", 2);

            Assert.AreEqual("{\"Libelle\":\"test libellé d'objet\",\"Type\":2}", JsonConvert.ConvertDynamicObjet(jsonObject));
        }
    }

    public class TestObjectToConvert
    {
        public string Libelle { get; set; }
        public int Type { get; set; }
        public override bool Equals(System.Object obj)
        {
            return (Libelle.Equals(((TestObjectToConvert)obj).Libelle) && Type == ((TestObjectToConvert)obj).Type) ;
        }
    }
}
