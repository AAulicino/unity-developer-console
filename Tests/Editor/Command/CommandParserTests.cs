using System;
using System.Reflection;
using NUnit.Framework;
using UnityDevConsole.Models.Command;

namespace UnityDevConsole.Tests.Command
{
    public class CommandParserTests
    {
        CommandParserModel model;
        TestTypeParserModel typeParser;

        [SetUp]
        public void Setup ()
        {
            typeParser = new TestTypeParserModel();
            model = new CommandParserModel(typeParser);
        }

        [Test]
        public void ParseCommand_Correctly_Parses_Command_Name ()
        {
            (string command, _) = model.ParseCommand("Foo 1");
            Assert.AreEqual("Foo", command);
        }

        [Test]
        public void ParseCommand_Correctly_Parses_Command_Name_Complex ()
        {
            (string command, _) = model.ParseCommand("FooBarBoo 1 2 as¢¬ç³²");
            Assert.AreEqual("FooBarBoo", command);

        }

        [Test]
        public void ParseCommand_Correctly_Parses_Command_Name_DQuoted_Param ()
        {
            (string command, _) = model.ParseCommand("FooBarBoo \"f o o\"");
            Assert.AreEqual("FooBarBoo", command);
        }

        [Test]
        public void ParseCommand_Correctly_Parses_Args ()
        {
            (_, string[] args) = model.ParseCommand("Foo 1");
            Assert.AreEqual(new[] { "1" }, args);
        }

        [Test]
        public void ParseCommand_Correctly_Parses_Args_Multiple ()
        {
            (_, string[] args) = model.ParseCommand("Foo 1 2 as¢¬ç³² 32k1");
            Assert.AreEqual(new[] { "1", "2", "as¢¬ç³²", "32k1" }, args);
        }

        [Test]
        public void ParseCommand_Correctly_Parses_DQoted_Arg ()
        {
            (_, string[] args) = model.ParseCommand("FooBarBoo \"f o o\"");
            Assert.AreEqual(new[] { "f o o" }, args);
        }

        [Test]
        public void ParseCommand_Correctly_Parses_DQoted_Arg_Multiple ()
        {
            (_, string[] args) = model.ParseCommand("FooBarBoo \"f o o\" \"b A r\"");
            Assert.AreEqual(new[] { "f o o", "b A r" }, args);
        }

        [Test]
        public void ParseArgs_Ignores_Extra_Args ()
        {
            TestCommandModel cmd = new TestCommandModel
            {
                Parameters = new ParameterInfo[0]
            };
            object[] result = model.ParseArgs(cmd, new[] { "1" });
            Assert.AreEqual(0, result.Length);
        }
    }
}
