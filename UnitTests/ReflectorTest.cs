using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Model;
using BusinessLogic.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ReflectorTest
    {
        private string path = @"..\..\..\LibraryForTests\bin\Debug\LibraryForTests.dll";

        [TestMethod]
        public void CheckReflectorConstructor()
        {
            Reflector reflector = new Reflector(path);
            Assert.AreEqual("LibraryForTests.dll", reflector.AssemblyModel.Name);
            Assert.ThrowsException<ArgumentNullException>((() => new Reflector(String.Empty)));
        }

        [TestMethod]
        public void CheckAmountOfNamespaces()
        {
            Reflector reflector = new Reflector(path);
            Assert.AreEqual(3, reflector.AssemblyModel.NamespaceModels.Count);
        }

        [TestMethod]
        public void CheckAmountOfClasses()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> testLibraryTypes = reflector.AssemblyModel.NamespaceModels.Find(t=>t.Name == "TestLibrary").Types;
            List<TypeModel> namespaceTwoTypes = reflector.AssemblyModel.NamespaceModels.Find(t => t.Name == "TestLibrary.NamespaceTwo").Types;
            List<TypeModel> namespaceWithRecursionTypes = reflector.AssemblyModel.NamespaceModels.Find(t => t.Name == "TestLibrary.NamespaceWithRecursion").Types;
            Assert.AreEqual(5, namespaceTwoTypes.Count);
            Assert.AreEqual(3, namespaceWithRecursionTypes.Count);
            Assert.AreEqual(2, testLibraryTypes.Count);
        }

    }
}
