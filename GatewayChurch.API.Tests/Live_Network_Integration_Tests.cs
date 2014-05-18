using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading;
using System.Collections.Generic;
using GatewayChurch.API.Networking;
using GatewayChurch.API.Models;

namespace GatewayChurch.API.Tests
{
    [TestClass]
    public class Live_Network_Integration_Tests
    {
        [TestMethod]
        public void should_return_years()
        {
            IGatewayService sut = new GatewayService();
            ManualResetEvent reset = new ManualResetEvent(false);

            Exception tEX = new Exception(); ;
            ICollection<string> tYears = new List<string>();

            sut.GetYears((results, ex) =>
            {
                tEX = ex;
                tYears = results;

                reset.Set();
            });

            reset.WaitOne();

            Assert.IsNull(tEX);
            Assert.IsTrue(tYears.Count > 0, "Should contain the years");


        }

        [TestMethod]
        public void should_return_authors()
        {
            ICollection<Speaker> _speakers = new List<Speaker>();
            Exception _ex = new Exception();
            ManualResetEvent done = new ManualResetEvent(false);

            IGatewayService sut = new GatewayService();
            sut.GetAuthors((authors, ex) =>
            {
                _speakers = authors;
                _ex = ex;

                done.Set();
            });

            done.WaitOne();
            Assert.IsNull(_ex);
            Assert.IsTrue(_speakers.Count > 0);
        }

        [TestMethod]
        public void should_get_latest_sermons()
        {
            ICollection<Sermon> _results = new List<Sermon>();
            Exception _ex = new Exception();
            ManualResetEvent done = new ManualResetEvent(false);

            IGatewayService sut = new GatewayService();
            sut.GetLastestSermons((results, ex) =>
            {
                _ex = ex;
                _results = results;
                done.Set();
            });

            done.WaitOne();
            Assert.IsNull(_ex);
            Assert.IsTrue(_results.Count > 0);

        }
        [TestMethod]
        public void should_get_sermons_by_series()
        {
            ICollection<Sermon> _results = new List<Sermon>();
            Exception _ex = new Exception();
            ManualResetEvent done = new ManualResetEvent(false);
            string SeriesId = string.Empty;

            IGatewayService sut = new GatewayService();
            sut.GetSeries((Series, ex) =>
            {
                foreach (var s in Series)
                {
                    SeriesId = s.Id;
                    break;
                }
                sut.GetSermonBySeries(SeriesId, (results, ex2) =>
                {
                    _ex = ex2;
                    _results = results;
                    done.Set();
                });
            });

            done.WaitOne();

            Assert.IsNull(_ex);
            Assert.IsTrue(_results.Count > 0);


        }

        [TestMethod]
        public void should_get_series_list()
        {
            ICollection<Series> _results = new List<Series>();
            Exception _ex = new Exception();
            ManualResetEvent done = new ManualResetEvent(false);

            IGatewayService sut = new GatewayService();
            sut.GetSeries((results, ex) =>
            {
                _ex = ex;
                _results = results;
                done.Set();
            });

            done.WaitOne();
            Assert.IsNull(_ex);
            Assert.IsTrue(_results.Count > 0);
        }
    }
}
