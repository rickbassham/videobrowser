﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaBrowser.Library.Entities;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Net;
using MediaBrowser.Library.Extensions;
using TestMediaBrowser.SupportingClasses;
using NUnit.Framework;
using System.Threading;
using MediaBrowser.Library.Network;
using MediaBrowser.Library;
using System.Globalization;
using System.Reflection;


namespace TestMediaBrowser {
    [TestFixture]
    public class TestVodCast {

        [Test]
        public void TestParseCrappyDate()
        {
            DateTime date = DateTime.Now;
            DateTimeFormatInfo dtfi = CultureInfo.InvariantCulture.DateTimeFormat;
            var dateVal = date.ToString(dtfi.RFC1123Pattern, dtfi);

            MethodInfo objMethod = typeof(Rss20FeedFormatter).GetMethod("DateFromString", BindingFlags.NonPublic | BindingFlags.Static);
            objMethod.Invoke(null, new object[] { dateVal, null });


          //  var str = "Wed, 22 Dec 2010 12:22:22 +0:00";
          //  var date = DateTime.ParseExact(str, "ddd',' dd MMM yyyy HH:mm:ss zzz", null);
        }
      
        [Test]
        public void TestTEDPodcastFetching()
        {
            var backup = Kernel.Instance.MediaLocationFactory;

            MockMediaLocation location = new MockMediaLocation("test.vodcast");
            location.Contents = "url : http://feeds.feedburner.com/TedtalksHD?format=xml";

            // our kernel needs to know how to retrieve our location.
            Kernel.Instance.MediaLocationFactory = new MockMediaLocationFactory(location);

            VodCast vodcast = new VodCast();
            vodcast.Assign(location, null, Guid.NewGuid());

            Assert.IsTrue(vodcast.Children.Count > 0);

            Kernel.Instance.MediaLocationFactory = backup;

        }


        [Test]
        public void TestPodcastFetching() {
            var backup = Kernel.Instance.MediaLocationFactory;

            MockMediaLocation location = new MockMediaLocation("test.vodcast");
            location.Contents = "url : http://www.abc.net.au/atthemovies/vodcast_wmv.xml";

            // our kernel needs to know how to retrieve our location.
            Kernel.Instance.MediaLocationFactory = new MockMediaLocationFactory(location);

            VodCast vodcast = new VodCast();
            vodcast.Assign(location, null, Guid.NewGuid());

            Assert.IsTrue(vodcast.Children.Count > 0);

            Kernel.Instance.MediaLocationFactory = backup;

        }

        [Test]
        public void TestPodcastInitialization() {
            
            MockMediaLocation location = new MockMediaLocation("test.vodcast");
            location.Contents = "url : http://somewhere.com\n";
            location.Contents += "download_policy : FirstPlay\n";
            location.Contents += "files_to_retain : 10\n";

            VodCast vodcast = new VodCast();
            vodcast.Assign(location, null, Guid.NewGuid());

            Assert.AreEqual(vodcast.Url, "http://somewhere.com");
            Assert.AreEqual(vodcast.DownloadPolicy, DownloadPolicy.FirstPlay);
            Assert.AreEqual(vodcast.FilesToRetain, 10);
        } 
    }
}
