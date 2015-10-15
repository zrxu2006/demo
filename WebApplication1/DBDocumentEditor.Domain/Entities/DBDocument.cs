using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDocumentEditor.Domain.Entities
{
    public class DBDocument
    {
        /// <summary>
        /// 数据库表在数据库中的对象Id
        /// </summary>
        public int ObjectId { get; set; }
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string Description { get; set; }
    }

}
