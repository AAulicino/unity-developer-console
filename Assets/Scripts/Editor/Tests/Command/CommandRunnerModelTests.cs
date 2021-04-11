using System;
using System.Reflection;
using NUnit.Framework;
using UnityDevConsole.Models.Command;

namespace UnityDeveloperConsole.Tests.Command
{
    public class CommandRunnerModelTests
    {
        CommandRunnerModel Model;
        TestCommandsCollectionModel commandsCollection;
        TestTypeParserModel typeParser;

        MethodInfo barMethodInfo;

        [SetUp]
        public void Setup ()
        {
            barMethodInfo = typeof(Foo).GetMethod("Bar");

            commandsCollection = new TestCommandsCollectionModel();
            typeParser = new TestTypeParserModel();

            Model = new CommandRunnerModel(
                commandsCollection,
                typeParser
            );
        }

        [Test]
        public void ExecuteCommand_Returns_Null_On_Empty_Parameter ()
        {
            Assert.IsNull(Model.ExecuteCommand(""));
        }

        [Test]
        public void ExecuteCommand_Correctly_Parses_Commands_And_Invoke_Command ()
        {
            TestCommandModel command = new TestCommandModel
            {
                Parameters = barMethodInfo.GetParameters()
            };

            object[] result = null;
            command.OnInvokeCalled += x => result = x;
            commandsCollection.Commands.Add("foo", command);
            Model.ExecuteCommand("foo 3");

            Assert.AreEqual(3, result[0]);
        }

        class Foo
        {
            public event Action<int> OnBarCalled;

            public void Bar (int a) => OnBarCalled?.Invoke(a);
        }
    }
}
