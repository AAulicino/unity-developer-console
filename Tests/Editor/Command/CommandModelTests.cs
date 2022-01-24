using System;
using System.Reflection;
using NUnit.Framework;
using UnityDevConsole.Models.Command;
using UnityEngine;

namespace UnityDevConsole.Tests.Command
{
    public class CommandModelTests : MonoBehaviour
    {
        MethodInfo barMethodInfo;

        [SetUp]
        public void Setup ()
        {
            barMethodInfo = typeof(Foo).GetMethod("Bar");
        }

        [Test]
        public void CommandName ()
        {
            string expected = "test";
            CommandModel model = new CommandModel(
                commandName: expected,
                barMethodInfo,
                default,
                default,
                default
            );
            Assert.AreEqual(expected, model.Name);
        }

        [Test]
        public void DeveloperOnly_True ()
        {
            CommandModel model = new CommandModel(
                default,
                barMethodInfo,
                default,
                developerOnly: true,
                default
            );
            Assert.IsTrue(model.DeveloperOnly);
        }

        [Test]
        public void Parameters_Equals ()
        {
            CommandModel model = new CommandModel(
                default,
                method: barMethodInfo,
                default,
                default,
                default
            );

            ParameterInfo[] parameters = barMethodInfo.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
                Assert.AreEqual(parameters[i], model.Parameters[i]);
        }

        [Test]
        public void Hidden_True ()
        {
            CommandModel model = new CommandModel(
                default,
                 barMethodInfo,
                default,
                default,
                hidden: true
            );
            Assert.IsTrue(model.Hidden);
        }

        [Test]
        public void Invoke_Invokes_On_Context ()
        {
            Foo foo = new Foo();

            bool called = false;
            foo.OnBarCalled += (x, y) => called = true;

            CommandModel model = new CommandModel(
                default,
                barMethodInfo,
                context: foo,
                default,
                hidden: true
            );
            model.Invoke(new object[] { 1, false });
            Assert.IsTrue(called);
        }

        class Foo
        {
            public event Action<int, bool> OnBarCalled;

            public void Bar (int a, bool b) => OnBarCalled?.Invoke(a, b);
        }
    }
}
