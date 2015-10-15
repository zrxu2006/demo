using DBDocumentEditor.Domain.Abstract;
using DBDocumentEditor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDocumentEditor.Domain.Concretes
{
    internal class SqlDocRepository:IDBDocRepository
    {
        readonly string _tableName;
        List<DBDocument> _dbDocList = null;

        internal SqlDocRepository(string tableName)
        {
            _tableName = tableName;            
        }

        public bool UpdateTableDescription(string description)
        {
            
            //d.Database.ExecuteSqlCommand("",)
            throw new NotImplementedException();
        }

        public bool UpdateFieldDescription(string fieldName, string value)
        {
            throw new NotImplementedException();
        }

        private List<DBDocument> SelectAll()
        {
            using (var context = new EFDocumentContext())
            {
                string sql = @"SELECT TOP 1 1 ObjectId,'TableName1' TableName,'FieldName1' FieldName,'Description1' Description
                               FROM CityGroup";
                return context.Database.SqlQuery<DBDocument>(sql).ToList();               
            }
        }

        public List<DBDocument> Documents
        {
            get
            {
                _dbDocList = SelectAll();
                if (_dbDocList == null)
                {
                    _dbDocList = new List<DBDocument>();                    
                    //_dbDocList.Add(new DBDocument
                    //{
                    //    Description = "城市分组111",
                    //    FieldName = "",
                    //    TableName = "CityGroup"
                    //});
                    //_dbDocList.Add(new DBDocument
                    //{
                    //    Description = "城市分组Id",
                    //    FieldName = "GroupId",
                    //    TableName = "CityGroup"
                    //});
                    //_dbDocList.Add(new DBDocument
                    //{
                    //    Description = "城市分组名称",
                    //    FieldName = "GroupName",
                    //    TableName = "CityGroup"
                    //});
                }

                return _dbDocList;
            }
        }
    }
}
