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
            Assert.AreEqual(6, namespaceTwoTypes.Count);
            Assert.AreEqual(3, namespaceWithRecursionTypes.Count);
            Assert.AreEqual(4, testLibraryTypes.Count);
        }

        [TestMethod]
        public void CheckAmountOfStaticClasses()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> staticClasses = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary.NamespaceTwo").Types
                .Where(t => t.Modifiers.Item4 == StaticEnum.Static).ToList();
            Assert.AreEqual(1, staticClasses.Count);
        }

        [TestMethod]
        public void CheckAmountOfAbstractClasses()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> abstractClasses = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types
                .Where(t => t.Modifiers.Item3 == AbstractEnum.Abstract).ToList();
            Assert.AreEqual(2, abstractClasses.Count);
        }

        [TestMethod]
        public void CheckAmountOfClassesWithGenericArguments()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> genericClasses = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary.NamespaceTwo").Types.Where(t => t.GenericArguments != null)
                .ToList();
            Assert.AreEqual(1, genericClasses.Count);
        }

        [TestMethod]
        public void CheckAmountOfInterfaces()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> interfaces = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Type == TypeEnum.Interface).ToList();
            Assert.AreEqual(1, interfaces.Count);
        }

        [TestMethod]
        public void CheckAmountOfClassesWithBaseType()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> classesWithBaseType = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.BaseType != null).ToList();
            Assert.AreEqual(1, classesWithBaseType.Count);
        }

        [TestMethod]
        public void CheckAmountOfPublicClasses()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> publicClasses = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Modifiers.Item1 == AccessLevel.Public).ToList();
            Assert.AreEqual(4, publicClasses.Count);
        }

        [TestMethod]
        public void CheckAmountOfClassesWithImplementedInterfaces()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> classesWithImplementedInterfaces = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.ImplementedInterfaces.Count > 0).ToList();
            Assert.AreEqual(1, classesWithImplementedInterfaces.Count);
        }

        [TestMethod]
        public void CheckAmountOfClassesWithNestedTypes()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> classesWithNestedTypes = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary.NamespaceTwo").Types.Where(t => t.NestedTypes.Count > 0).ToList();
            Assert.AreEqual(2, classesWithNestedTypes.Count);
        }

        [TestMethod]
        public void CheckAmountOfPropertiesInClass()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> classes = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Name == "PublicClass").ToList();
            Assert.AreEqual(1, classes.First().Properties.Count);
        }

        [TestMethod]
        public void CheckAmountOfMethodsInClass()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> classes = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Name == "PublicClass").ToList();
            Assert.AreEqual(3, classes.First().Methods.Count);
        }

        [TestMethod]
        public void CheckAmountOfConstructorsInClass()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> classes = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Name == "PublicClass").ToList();
            Assert.AreEqual(2, classes.First().Constructors.Count);
        }

        [TestMethod]
        public void CheckAmountOfFieldsInClass()
        {
            Reflector reflector = new Reflector(path);
            List<TypeModel> classes = reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Name == "PublicClass").ToList();
            Assert.AreEqual(2, classes.First().Fields.Count);
        }
    }
}
