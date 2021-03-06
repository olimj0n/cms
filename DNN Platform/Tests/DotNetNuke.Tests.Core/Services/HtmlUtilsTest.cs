#region Copyright
// 
// DotNetNukeŽ - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
// 
// Hotcakes Commerce - https://hotcakes.org
// Copyright (c) 2017
// by Hotcakes Commerce, LLC
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
using DotNetNuke.Common.Utilities;

using NUnit.Framework;

namespace DotNetNuke.Tests.Core.Services
{
    /// <summary>
    /// </summary>
    [TestFixture]
    public class HtmlUtilsTests
    {
        private const string HtmlStr =
            "Hello World!<br /><br />This is a sample HTML text for testing!<br /><br /><img alt=\"HappyFaceAlt\" title=\"HappyFaceTitle\" test=\"noShow\" src=\"/hotcakes/Portals/0/Telerik/images/Emoticons/1.gif\" /><br /><br /><img alt=\"\" src=\"http://localhost/hotcakes/Portals/0/aspnet.gif\" /><br /><br /><a href=\"https://hotcakes.org\">Hotcakes Commerce</a>";

        private const string Filters = "alt|href|src|title";
        private string _expected = "";

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        [Timeout(2000)]
        public void DNN_12430_IsHtml_RegExTimeout()
        {
            var result =
                HtmlUtils.IsHtml(
                    @"'New Event: <a href=\""http://localhost/hcc540/Home/tabid/40/ModuleID/389/ItemID/8/mctl/EventDetails/Default.aspx"">Test</a> on Saturday, May 08, 2010 2:00 AM to Saturday, May 08, 2010 2:30 AM  - One time event - has been added");

            Assert.IsTrue(result);
        }

        [Test]
        [Timeout(2000)]
        public void DNN_12926_IsHtml_Detection()
        {
            var result = HtmlUtils.IsHtml("this is a test of hccmail: <a href='https://hotcakes.org'>Hotcakes Commerce</a>");

            Assert.IsTrue(result);
        }

        [TearDown]
        public void TearDown()
        {
        }

        #region HTML Cleaning Tests

        [Test]
        public void HtmlUtils_CleanWithTagInfo_Should_Return_Clean_Content_With_Attribute_Values()
        {
            // Arrange
            SetUp();
            _expected =
                "Hello World This is a sample HTML text for testing Hotcakes Commerce HappyFaceAlt HappyFaceTitle /hotcakes/Portals/0/Telerik/images/Emoticons/1.gif http://localhost/hotcakes/Portals/0/aspnet.gif https://hotcakes.org ";

            // Act
            object retValue = HtmlUtils.CleanWithTagInfo(HtmlStr, Filters, true);
            // Assert
            Assert.AreEqual(_expected, retValue);

            // TearDown
            TearDown();
        }

        [Test]
        public void HtmlUtils_CleanWithTagInfo_Should_Return_Clean_Content_Without_Attribute_Values()
        {
            // Arrange
            SetUp();
            _expected = "Hello World This is a sample HTML text for testing Hotcakes Commerce ";

            // Act
            object retValue = HtmlUtils.CleanWithTagInfo(HtmlStr, " ", true);
            // Assert
            Assert.AreEqual(_expected, retValue);

            // TearDown
            TearDown();
        }

        [Test]
        public void HtmlUtils_StripUnspecifiedTags_Should_Return_Attribute_Values()
        {
            // Arrange
            SetUp();
            _expected =
                "Hello World!This is a sample HTML text for testing!Hotcakes Commerce \"HappyFaceAlt\" \"HappyFaceTitle\" \"/hotcakes/Portals/0/Telerik/images/Emoticons/1.gif\" \"\" \"http://localhost/hotcakes/Portals/0/aspnet.gif\" \"https://hotcakes.org\"";

            // Act
            object retValue = HtmlUtils.StripUnspecifiedTags(HtmlStr, Filters, false);
            // Assert
            Assert.AreEqual(_expected, retValue);

            // TearDown
            TearDown();
        }

        [Test]
        public void HtmlUtils_StripUnspecifiedTags_Should_Not_Return_Attribute_Values()
        {
            // Arrange
            SetUp();
            _expected = "Hello World!This is a sample HTML text for testing!Hotcakes Commerce";

            // Act
            object retValue = HtmlUtils.StripUnspecifiedTags(HtmlStr, " ", false);
            // Assert
            Assert.AreEqual(_expected, retValue);

            // TearDown
            TearDown();
        }

        #endregion
    }
}