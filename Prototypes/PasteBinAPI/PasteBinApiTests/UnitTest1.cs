using System;
using NUnit.Framework;
using PasteBinApi;

namespace PasteBinApiTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            HttpApi api = new HttpApi();
            var text = api.GetPaste().Result;
            Console.WriteLine(text);
            Assert.Pass();
        }
    }
}