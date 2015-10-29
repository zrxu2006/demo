using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBDocumentEditor.Domain;

namespace Utility.Test
{
    [TestClass]
    public class DBDocumentTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var repo = DocumentFactory.CreateRepository("CityGroup");
            Assert.AreEqual(1, repo.Documents.Count);
        }
    }
}
