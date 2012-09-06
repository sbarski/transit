using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;

namespace Transit.Data
{
    public class DocumentStoreFactory : IDocumentStoreFactory
    {
        private static DocumentStore _documentStore;

        public static IDocumentStore DocumentStore
        {
            get { return _documentStore; }
        }

        public DocumentStoreFactory()
        {
            _documentStore = new DocumentStore() { ConnectionStringName = "Server" };

            _documentStore.Initialize();
        }
    }
}
