using NUnit.Framework;
using UnityDevConsole.Models.Command;

namespace UnityDevConsole.Tests.Command
{
    public class CommandRunnerModelTests
    {
        CommandRunnerModel Model;
        TestCommandsCollectionModel commandsCollection;
        TestCommandParser commandParser;

        [SetUp]
        public void Setup ()
        {
            commandsCollection = new TestCommandsCollectionModel();
            commandParser = new TestCommandParser();

            Model = new CommandRunnerModel(
                commandsCollection,
                commandParser
            );
        }

        [Test]
        public void ExecuteCommand_Returns_Null_On_Empty_Parameter ()
        {
            Assert.IsNull(Model.ExecuteCommand(""));
        }

        [Test]
        public void ExecuteCommand_Invokes_Command ()
        {
            TestCommandModel command = new TestCommandModel();

            object[] result = null;
            command.OnInvokeCalled += x => result = x;
            commandsCollection.Commands.Add("foo", command);

            commandParser.ParseArgsReturnValue = new object[] { 3 };

            Model.ExecuteCommand("foo", new[] { "3" });

            Assert.AreEqual(3, result[0]);
        }

        [Test]
        public void ExecuteCommand_From_String_Invokes_Command ()
        {
            TestCommandModel command = new TestCommandModel();

            object[] result = null;
            command.OnInvokeCalled += x => result = x;
            commandsCollection.Commands.Add("foo", command);

            commandParser.ParseCommandReturnValue = ("foo", new[] { "3" });
            commandParser.ParseArgsReturnValue = new object[] { 3 };

            Model.ExecuteCommand("foo 3");

            Assert.AreEqual(3, result[0]);
        }

        [Test]
        public void ExecuteCommand_Non_Existing_Returns_Error_Log ()
        {
            TestCommandModel command = new TestCommandModel();

            bool called = false;
            command.OnInvokeCalled += x => called = true;
            commandsCollection.Commands.Add("foo", command);

            Model.ExecuteCommand("bar", new[] { "3" });

            Assert.IsFalse(called);
        }
    }
}
