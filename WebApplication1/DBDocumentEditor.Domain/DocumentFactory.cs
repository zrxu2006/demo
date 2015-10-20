using DBDocumentEditor.Domain.Abstract;
using DBDocumentEditor.Domain.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDocumentEditor.Domain
{
    public class DocumentFactory
    {
        public static IDBDocRepository CreateRepository(string tableName)
        {
            return new SqlDocRepository(tableName);
        }

    }
}
