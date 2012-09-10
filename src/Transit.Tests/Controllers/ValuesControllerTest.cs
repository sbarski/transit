using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transit;
using Transit.Controllers;
using SimpleInjector;
using Raven.Client;

namespace Transit.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        private readonly Container _container;

        public ValuesControllerTest()
        {
            _container = new Container();

            var documentStore = new Raven.Client.Embedded.EmbeddableDocumentStore { RunInMemory = true, UseEmbeddedHttpServer = true };

            //documentStore.Initialize();

            _container.RegisterSingle<IDocumentStore>(documentStore);

            _container.RegisterSingle<IAsyncDocumentSession>(() => documentStore.OpenAsyncSession());

            _container.RegisterSingle<IDocumentSession>(() => documentStore.OpenSession());
        }

        [TestInitialize]
        public void Initialize()
        {
            var documentStore = _container.GetInstance<IDocumentStore>();

            documentStore.Initialize(); //Re-initialize the document store
        }

        [TestMethod]
        public void Blah()
        {
        }

        [TestMethod]
        public void Get()
        {
            var documentSession = _container.GetInstance<IDocumentSession>();
            //var asyncDocumentSession = _container.GetInstance<IAsyncDocumentSession>();

            //// Arrange
            //ValuesController controller = new ValuesController(documentSession, null);

            //// Act
            //IEnumerable<string> result = controller.Get();

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        //[TestMethod]
        //public void GetById()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    string result = controller.Get(5);

        //    // Assert
        //    Assert.AreEqual("value", result);
        //}

        //[TestMethod]
        //public void Post()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Post("value");

        //    // Assert
        //}

        //[TestMethod]
        //public void Put()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Put(5, "value");

        //    // Assert
        //}

        //[TestMethod]
        //public void Delete()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Delete(5);

        //    // Assert
        //}
    }
}
