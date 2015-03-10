using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DotNetCommon.Helper;
using DotNetCommon.Web;

namespace DotNetCommon.Test.HelperTest
{
    [TestFixture]
    public class StringHelperTest
    {
        [Test]
        public void Base64Test()
        {
            var encode = StringHelper.EncodeToBase64("utf-8", "hello,ww");
            var decode = StringHelper.DecodeFromBase64("utf-8", encode);

            Assert.AreEqual("hello,ww", decode);
        }

        [Test]
        public void FirstSpellTest()
        {
            var spell = StringHelper.FirstSpell("这个人是谁");
            Console.Out.WriteLine(spell);
            Assert.AreEqual(spell, "ZGRSS");

        }

        [Test]
        public void ClearHTMLTagTest()
        {
            var left = HtmlHelper.ClearHTMLTag("<div id=\"site_nav_under\"><a href=\"http://www.cnblogs.com/\" target=\"_blank\" title=\"开发者的网上家园\">博客园首页</a><a href=\"http://q.cnblogs.com/\" target=\"_blank\" title=\"程序员问答社区\">博问</a><a href=\"http://news.cnblogs.com/\" target=\"_blank\" title=\"IT新闻\">新闻</a><a href=\"http://home.cnblogs.com/ing/\" target=\"_blank\">闪存</a><a href=\"http://job.cnblogs.com/\" target=\"_blank\">程序员招聘</a><a href=\"http://kb.cnblogs.com/\" target=\"_blank\">知识库</a></div>");
            Console.Out.WriteLine(left);
            Assert.AreEqual(left, "博客园首页博问新闻闪存程序员招聘知识库");
        }

    }
}
