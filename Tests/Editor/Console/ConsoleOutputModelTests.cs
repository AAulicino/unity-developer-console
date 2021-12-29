using NUnit.Framework;
using UnityDevConsole.Models.Console;
using UnityEngine;

namespace UnityDevConsole.Tests.Console
{
    public class ConsoleOutputModelTests : MonoBehaviour
    {
        ConsoleOutputModel model;

        [SetUp]
        public void Setup ()
        {
            model = new ConsoleOutputModel();
        }

        [Test]
        public void WriteLine_Raises_On_ContentUpdate ()
        {
            bool called = false;
            model.OnContentUpdate += x => called = true;
            model.WriteLine("");

            Assert.IsTrue(called);
        }

        [Test]
        public void ContentUpdate_Outputs_Appends_Content ()
        {
            string output = null;
            model.OnContentUpdate += x => output = x;
            model.WriteLine("Foo");

            Assert.AreEqual("Foo\r\n", output);
        }

        [Test]
        public void ContentUpdate_Outputs_Appends_Content_In_New_Line ()
        {
            string output = null;
            model.OnContentUpdate += x => output = x;
            model.WriteLine("Foo");
            model.WriteLine("Bar");

            Assert.AreEqual("Foo\r\nBar\r\n", output);
        }

        [Test]
        public void Clear_Raises_OnContentUpdate ()
        {
            bool called = false;
            model.OnContentUpdate += x => called = true;
            model.Clear();

            Assert.IsTrue(called);
        }

        [Test]
        public void Clear_Raises_OnContentUpdate_With_Empty_Argument ()
        {
            string output = null;
            model.OnContentUpdate += x => output = x;
            model.Clear();

            Assert.AreEqual("", output);
        }

        [Test]
        public void Clear_Resets_WriteLine_Output ()
        {
            string output = null;

            model.OnContentUpdate += x => output = x;

            model.WriteLine("foo");
            model.Clear();
            model.WriteLine("bar");

            Assert.AreEqual("bar\r\n", output);
        }
    }
}
