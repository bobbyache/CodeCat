﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CygSoft.CodeCat.Search.KeywordIndex;
using System.Collections.Generic;

namespace UnitTests.Search.KeywordIndex
{
    [TestClass]
    public class KeyPhrases_Tests
    {
        [TestMethod]
        public void KeyPhrases_Ensure_No_Exception_WithEmptyAdd()
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            keyPhrases.AddKeyPhrases(",,");

            Assert.AreEqual(0, keyPhrases.Phrases.Length);
            Assert.IsTrue(!keyPhrases.PhraseExists(""));

            keyPhrases.AddKeyPhrases(" , ");
            Assert.AreEqual(0, keyPhrases.Phrases.Length);
            Assert.IsTrue(!keyPhrases.PhraseExists(""));

        }

        [TestMethod]
        public void KeyPhrases_Ensure_No_Empty_Keyword_Appended()
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            keyPhrases.AddKeyPhrases("yellow,black,");

            Assert.AreEqual(2, keyPhrases.Phrases.Length);
            Assert.AreEqual("BLACK", keyPhrases.Phrases[0]);
            Assert.AreEqual("YELLOW", keyPhrases.Phrases[1]);

            Assert.IsTrue(keyPhrases.PhraseExists("yellow"));
            Assert.IsTrue(keyPhrases.PhraseExists("black"));
            Assert.IsTrue(!keyPhrases.PhraseExists(""));
        }

        [TestMethod]
        public void KeyPhrases_Ensure_No_Empty_Keyword_In_Array()
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            keyPhrases.AddKeyPhrases(new string[] { "yellow", "black", "" });

            Assert.AreEqual(2, keyPhrases.Phrases.Length);
            Assert.AreEqual("BLACK", keyPhrases.Phrases[0]);
            Assert.AreEqual("YELLOW", keyPhrases.Phrases[1]);

            Assert.IsTrue(keyPhrases.PhraseExists("yellow"));
            Assert.IsTrue(keyPhrases.PhraseExists("black"));
            Assert.IsTrue(!keyPhrases.PhraseExists(""));
        }

        [TestMethod]
        public void KeyPhrases_Ensure_No_Empty_Keyword_SavedFromParameter_List()
        {
            KeyPhrases keyPhrases = new KeyPhrases(new List<string> { "yellow", "black", "" });

            Assert.AreEqual(2, keyPhrases.Phrases.Length);
            Assert.AreEqual("BLACK", keyPhrases.Phrases[0]);
            Assert.AreEqual("YELLOW", keyPhrases.Phrases[1]);

            Assert.IsTrue(keyPhrases.PhraseExists("yellow"));
            Assert.IsTrue(keyPhrases.PhraseExists("black"));
            Assert.IsTrue(!keyPhrases.PhraseExists(""));
        }
    }
}
