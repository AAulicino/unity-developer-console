using System.Linq;
using NUnit.Framework;
using UnityDevConsole.Models.Console;
using UnityDevConsole.Tests.Settings;

namespace UnityDevConsole.Tests.Console
{
    public class ConsoleInputHistoryModelTests
    {
        ConsoleInputHistoryModel model;
        TestConsoleSettings settings;

        [SetUp]
        public void Setup ()
        {
            settings = new TestConsoleSettings { HistorySize = 1 };
            model = new ConsoleInputHistoryModel(settings);
        }

        [Test]
        public void Add_Adds_To_InputHistory ()
        {
            model.Add("foo");
            Assert.AreEqual("foo", model.InputHistory.Single());
        }

        [Test]
        public void Add_Appends_To_List ()
        {
            settings.HistorySize = 2;
            model.Add("foo");
            model.Add("bar");
            Assert.AreEqual("bar", model.InputHistory[1]);
        }

        [Test]
        public void Add_Duplicate_Removes_Previous ()
        {
            settings.HistorySize = 3;
            model.Add("foo");
            model.Add("bar");
            model.Add("foo");
            Assert.AreEqual("bar", model.InputHistory[0]);
        }

        [Test]
        public void Add_Duplicate_Appends_To_InputHistory ()
        {
            settings.HistorySize = 3;
            model.Add("foo");
            model.Add("bar");
            model.Add("foo");
            Assert.AreEqual("foo", model.InputHistory[1]);
        }

        [Test]
        public void Add_Removes_Oldest_Entry_When_HistorySize_Is_Reached ()
        {
            settings.HistorySize = 2;
            model.Add("foo");
            model.Add("bar");
            model.Add("baz");
            Assert.AreEqual("bar", model.InputHistory[0]);
        }
    }
}
