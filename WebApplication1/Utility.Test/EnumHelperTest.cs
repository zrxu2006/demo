using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using m = System.ComponentModel;
using Utility.Extension;
using Moq;

namespace Utility.Test
{
    [TestClass]
    public class EnumHelperTest
    {
        [TestMethod]
        public void Value_Description_List()
        {
            var dict = EnumHelper<TestEnum>.GetValueAndDescList();

            Assert.AreEqual(4, dict.Count);
        }

        [TestMethod]
        public void Name_Description_List()
        {
            var dict = EnumHelper<TestEnum>.GetNameAndDescList();

            Assert.AreEqual(4, dict.Count);
        }
        
        [TestMethod]
        public void Name_Value_List()
        {
            var dict = EnumHelper<TestEnum>.GetNameAndValueList();

            Assert.AreEqual(4, dict.Count);
            
        }

        [TestMethod]
        public void Parse()
        {
            TestEnum? t = EnumHelper<TestEnum>.Parse("T01");
            Assert.AreEqual(t, null);

            TestEnum? t1 = EnumHelper<TestEnum>.Parse("T0");
            Assert.AreEqual(t1, TestEnum.T0);
        }

        [TestMethod]
        public void TestMoq()
        {
            Mock<ITest> t = new Mock<ITest>();
            t.Setup(e => e.test0).Returns(1);
            t.Setup(e => e.GetTest()).Returns("11");

            Assert.AreEqual(t.Object.test0, 1);
            Assert.AreEqual(t.Object.GetTest(), "11");
        }
    }

    public interface ITest
    {
        int test0 { get; set; }
        string GetTest();
    }
    public enum TestEnum
    {
        
        T0=-1,
        [m.Description("Enum-1")]
        T1,
        [m.Description("Enum-2")]
        T2,
        [m.Description("Enum-3")]
        T3
    }
}
