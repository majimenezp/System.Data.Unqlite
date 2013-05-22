using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Unqlite;
namespace UnitTesting
{
    [TestClass]
    public class Connection_Testing
    {
        [TestMethod]
        public void Test_connection_Open_Create()
        {
            UnqliteDB db = UnqliteDB.Create();
            var res=db.Open("test1.db", Unqlite_Open.CREATE);
            if (res)
            {
                db.Close();
            }
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_KeyValue_Store()
        {
            UnqliteDB db = UnqliteDB.Create();
            var res = db.Open("test1.db", Unqlite_Open.CREATE);
            if (res)
            {
                var res1 = db.SaveKeyValue("test", "hello world");
                string value = db.GetKeyValue("test");
                Assert.IsTrue(value == "hello world");
                db.Close();
            }
        }
    }
}
