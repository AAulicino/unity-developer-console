using System.Linq;
using NUnit.Framework;
using UnityDevConsole.Models.Command;
using UnityDevConsole.Tests.Command.Factory;
using UnityDevConsole.Tests.Settings;
using UnityEngine.TestTools;

namespace UnityDevConsole.Tests.Command
{
    public class CommandsCollectionModelTests
    {
        CommandsCollectionModel model;
        TestConsoleSettings settings;
        TestConsoleCommandFactory commandFactory;

        [SetUp]
        public void Setup ()
        {
            settings = new TestConsoleSettings();
            commandFactory = new TestConsoleCommandFactory();
            model = new CommandsCollectionModel(
                settings,
                commandFactory
            );
        }

        [Test]
        public void Initialize_Registers_Commands ()
        {
            TestCommandModel expected = new TestCommandModel();
            commandFactory.CreateFromAssembliesCommand.Add("", expected);
            model.Initialize();

            Assert.AreEqual(expected, model.Commands.Values.Single());
        }

        [Test]
        public void Initialize_Registers_Commands_Multiple ()
        {
            commandFactory.CreateFromAssembliesCommand.Add("a", new TestCommandModel());
            commandFactory.CreateFromAssembliesCommand.Add("b", new TestCommandModel());
            model.Initialize();

            Assert.AreEqual(2, model.Commands.Count);
        }

        [Test]
        public void RegisterRuntimeCommand_Registers_Command ()
        {
            TestCommandModel expected = new TestCommandModel();
            commandFactory.CreateCommand = expected;
            model.RegisterRuntimeCommand("", default, default, default, default);

            Assert.AreEqual(expected, model.Commands.Values.Single());
        }

        [Test]
        public void RegisterRuntimeCommand_Success_Returns_True ()
        {
            commandFactory.CreateCommand = new TestCommandModel();
            bool actual = model.RegisterRuntimeCommand("", default, default, default, default);

            Assert.IsTrue(actual);
        }

        [Test]
        public void RegisterRuntimeCommand_Returns_False_On_Duplicate ()
        {
            commandFactory.CreateCommand = new TestCommandModel();
            model.RegisterRuntimeCommand("", default, default, default, default);

            LogAssert.ignoreFailingMessages = true;

            bool actual = model.RegisterRuntimeCommand("", default, default, default, default);

            Assert.IsFalse(actual);
        }

        [Test]
        public void UnregisterRuntimeCommand_Unregisters_Command ()
        {
            commandFactory.CreateCommand = new TestCommandModel();
            model.RegisterRuntimeCommand("foo", default, default, default, default);
            model.UnregisterRuntimeCommand("foo");
            Assert.AreEqual(0, model.Commands.Count);
        }
    }
}
