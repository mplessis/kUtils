using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.Portable.Convert;
using Kopigi.Portable.Object;

namespace UnitTest.Portable.Json
{
    [TestClass]
    public class DynamicObjetTest
    {
        [TestMethod]
        public void JsonObjectCreate()
        {
            var jsonObject = new DynamicObjet();
            jsonObject.Add("Libelle", "test libellé d'objet");
            jsonObject.Add("Type", 2);

            Assert.AreEqual("test libellé d'objet", jsonObject.Dynamic.Libelle);
            Assert.AreEqual(2, jsonObject.Dynamic.Type);
        }
    }
}
