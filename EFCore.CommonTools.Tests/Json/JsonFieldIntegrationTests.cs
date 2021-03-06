﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

#if EF_CORE
namespace EntityFrameworkCore.CommonTools.Tests
#elif EF_6
namespace EntityFramework.CommonTools.Tests
#endif
{
    [TestClass]
    public class JsonFieldIntegrationTests : TestInitializer
    {
        [TestMethod]
        public void TestJsonFieldWithDbContext()
        {
            using (var context = CreateSqliteDbContext())
            {
                var settings = new Settings
                {
                    Key = "first",
                    Value = new { Number = 123, String = "test" },
                };
                
                context.Settings.Add(settings);

                context.SaveChanges();
            }

            using (var context = CreateSqliteDbContext())
            {
                var settings = context.Settings.Find("first");

                Assert.IsNotNull(settings);
                Assert.IsNotNull(settings.Value);
                Assert.AreEqual(123, (int)settings.Value.Number);
                Assert.AreEqual("test", (string)settings.Value.String);
            }
        }
    }
}
