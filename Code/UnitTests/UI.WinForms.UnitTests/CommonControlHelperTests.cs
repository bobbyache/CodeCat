﻿using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.UI.Resources;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using NUnit.Framework;
using System.Drawing;
using System.Windows.Forms;

namespace UI.WinForms.UnitTests
{
    [TestFixture]
    [Category("UI_Tests")]
    [Category("Tests.UnitTests")]
    public class CommonControlHelperTests
    {
        // https://sqa.stackexchange.com/questions/3136/how-to-test-the-graphical-contents-of-a-bitmap-or-a-pdf-file

        private IImageResources imageResources = new ImageResources();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CommonControlHelpers_ToolBar_CreateButton_CreatesToolstripButton_PrefixedName()
        {
            ToolStripButton btn = Gui.ToolBar.CreateButton("Edit Button", imageResources.GetImage(ImageKeys.EditSnippet), 
                new System.EventHandler((s, e) => { }));

            Assert.AreEqual("btnEditButton", btn.Name, "Expect that creating a button name will legalize the name and prefix it correctly.");
        }

        [Test]
        public void CommonControlHelpers_ToolBar_CreateButton_CreatesToolstripButton_Image()
        {
            ToolStripButton btn = Gui.ToolBar.CreateButton("Edit", imageResources.GetImage(ImageKeys.EditSnippet),
                new System.EventHandler((s, e) => { }));

            Assert.IsNotNull(btn.Image, "Expect that the applicble image is added to the button.");
        }

        [Test]
        public void CommonControlHelpers_ToolBar_CreateButton_CreatesToolstripButton_CommonProperties()
        {
            ToolStripButton btn = Gui.ToolBar.CreateButton("Edit", imageResources.GetImage(ImageKeys.EditSnippet),
                new System.EventHandler((s, e) => { }));

            Assert.AreEqual(Color.Magenta, btn.ImageTransparentColor);
            Assert.AreEqual(new Size(24, 24), btn.Size);
            Assert.AreEqual("Edit", btn.Text);
            Assert.AreEqual(ToolStripItemDisplayStyle.Image, btn.DisplayStyle);
        }

        [Test]
        public void CommonControlHelpers_ToolBar_CreateButton_AsImageAndText_CreatesToolstripButton_WithBothImageAndText()
        {
            ToolStripButton btn = Gui.ToolBar.CreateButton("Edit", imageResources.GetImage(ImageKeys.EditSnippet),
                new System.EventHandler((s, e) => { }), true);

            Assert.AreEqual(ToolStripItemDisplayStyle.ImageAndText, btn.DisplayStyle);
        }

        [Test]
        public void CommonControlHelpers_ToolBar_CreateButton_CreatesToolstripButton_CorrectlyAssignsHandler()
        {
            bool called = false;

            ToolStripButton btn = Gui.ToolBar.CreateButton("Edit", imageResources.GetImage(ImageKeys.EditSnippet),
                new System.EventHandler((s, e) => { called = true; }));

            btn.PerformClick();

            Assert.AreEqual(true, called);
        }
    }
}
