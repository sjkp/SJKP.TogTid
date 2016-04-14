using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SJKP.TogTid.Test
{
    [TestClass]
    public class RejseplanTest
    {
        [TestMethod]
        public async Task GetLocationTest()
        {
            var api = new RejseplanApi();

            var res = await api.GetLocation("København h");

            Assert.IsNotNull(res);
            Assert.AreNotEqual(0, res.Count);
            Assert.AreEqual("København H", res[0].name);
        }

        [TestMethod]
        public async Task GetDepatureTest()
        {
            var api = new RejseplanApi();

            var res = await api.GetDepartures(008600626);

            Assert.IsNotNull(res);
            Assert.AreNotEqual(0, res.Count);
            Assert.AreEqual(DateTime.Now.ToString("dd.MM.yy"), res[0].date);
        }

        [TestMethod]
        public async Task GetDepatureByDateTest()
        {
            var api = new RejseplanApi();

            var res = await api.GetDepartures(008600626, DateTime.Now.AddDays(1));

            Assert.IsNotNull(res);
            Assert.AreNotEqual(0, res.Count);
            Assert.AreEqual(DateTime.Now.AddDays(1).ToString("dd.MM.yy"), res[0].date);
        }
    }
}
