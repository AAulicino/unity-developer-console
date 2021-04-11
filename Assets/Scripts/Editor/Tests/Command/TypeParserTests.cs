using System;
using NUnit.Framework;
using UnityDevConsole.Models.Command.Parser;

namespace UnityDeveloperConsole.Tests.Command
{
    public class TypeParserTests
    {
        TypeParserModel model;

        [SetUp]
        public void Setup ()
        {
            model = new TypeParserModel();
        }

        [Test]
        public void Fails_To_Parse_Unregistered_Custom_Type ()
        {
            Assert.Throws<FormatException>(() => model.Parse("", typeof(Foo)));
        }

        [Test]
        public void Parses_Registered_Custom_Type ()
        {
            int expected = 1;
            model.RegisterCustomType(typeof(Foo), x => expected);
            object result = model.Parse("", typeof(Foo));

            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnregisterCustomType_Do_Unregister_Custom_Type ()
        {
            model.RegisterCustomType(typeof(Foo), x => 1);
            model.UnregisterCustomType(typeof(Foo));

            Assert.Throws<FormatException>(() => model.Parse("", typeof(Foo)));
        }

        [TestCase("1", typeof(byte), ExpectedResult = 1)]
        [TestCase("1", typeof(sbyte), ExpectedResult = 1)]
        [TestCase("1", typeof(int), ExpectedResult = 1)]
        [TestCase("1", typeof(uint), ExpectedResult = 1u)]
        [TestCase("1", typeof(short), ExpectedResult = 1)]
        [TestCase("1", typeof(ushort), ExpectedResult = 1u)]
        [TestCase("1", typeof(long), ExpectedResult = 1L)]
        [TestCase("1", typeof(ulong), ExpectedResult = 1UL)]
        [TestCase("1", typeof(float), ExpectedResult = 1f)]
        [TestCase("1", typeof(double), ExpectedResult = 1d)]
        [TestCase("1", typeof(char), ExpectedResult = '1')]
        [TestCase("1", typeof(string), ExpectedResult = "1")]
        [TestCase("true", typeof(bool), ExpectedResult = true)]
        public object Parse_Parses_Primitives (string input, Type type)
        {
            return model.Parse(input, type);
        }

        class Foo
        {
        }
    }
}
