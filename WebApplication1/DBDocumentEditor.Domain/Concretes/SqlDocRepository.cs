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

        public bool UpdateDescription(string fieldName, string value)
        {
            var dbDoc = (from doc in _dbDocList
                        where doc.TableName == _tableName 
                                && string.IsNullOrEmpty(fieldName)
                                    ?true: doc.FieldName==fieldName
                        select doc)
                        .FirstOrDefault();

            if (dbDoc == null)
            {
                return false;
            }
            
            if (string.IsNullOrEmpty(fieldName))
            {
                // 表说明更新
                return UpdateTableDescription(dbDoc);
            }
            else
            {
                // 表字段更新
                return UpdateFieldDescription(dbDoc);
            }
        }

        private List<DBDocument> SelectAll()
        {
            using (var context = new EFDocumentContext())
            {
                string sql = @"SELECT  o.name TableName ,
                                        '' FieldName ,
                                        p.value N'Description'
                                FROM    sys.objects o
                                        LEFT JOIN sys.extended_properties p ON o.object_id = p.major_id
                                                                               AND p.minor_id = 0
                                WHERE   o.type = 'U'
                                        AND o.name = @TableName
                                UNION
                                SELECT  o.name TableName ,
                                        c.name FieldName ,
                                        p.value N'Description'
                                FROM    sys.objects o
                                        INNER JOIN sys.columns c ON o.object_id = c.object_id
                                        LEFT JOIN sys.extended_properties p ON c.object_id = p.major_id
                                                                               AND c.column_id = p.minor_id
                                WHERE   o.type = 'U'
                                        AND o.name = @TableName";

                return context.Database.SqlQuery<DBDocument>(sql, new SqlParameter {
                    ParameterName = "@TableName",
                    DbType= System.Data.DbType.String,
                    Value = _tableName
                }).ToList();               
            }
        }

        /// <summary>
        /// 更新表的说明
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private bool UpdateTableDescription(DBDocument doc)
        {
            using (var context = new EFDocumentContext())
            {
                string sql = @"declare @CurrentUser sysname
                               select @CurrentUser = user_name()
                               -- 删除说明
                               execute sp_dropextendedproperty 'MS_Description', 
                                        'user', @CurrentUser, 'table', @TableName
                               -- 添加说明
                               execute sp_addextendedproperty 'MS_Description', 
                                       @Description,
                                       'user', @CurrentUser, 'table', @TableName";

                return context.Database.ExecuteSqlCommand(sql,
                        new SqlParameter
                        {
                            ParameterName = "@TableName",
                            DbType = System.Data.DbType.String,
                            Value = doc.TableName
                        },
                        new SqlParameter
                        {
                            ParameterName = "@Description",
                            DbType = System.Data.DbType.String,
                            Value = doc.Description
                        }) > 0;
            }
        }

        /// <summary>
        /// 更新表字段的说明
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private bool UpdateFieldDescription(DBDocument doc)
        {
            using (var context = new EFDocumentContext())
            {
                string sql = @"declare @CurrentUser sysname
                               select @CurrentUser = user_name()
                               -- 删除说明
                               execute sp_dropextendedproperty 'MS_Description', 
                                        'user', @CurrentUser, 'table', @TableName, 'column', @FiledName
                               -- 添加说明
                               execute sp_addextendedproperty 'MS_Description', 
                                       @Description,
                                       'user', @CurrentUser, 'table', @TableName, 'column', @FiledName";

                return context.Database.ExecuteSqlCommand(sql,
                        new SqlParameter
                        {
                            ParameterName = "@TableName",
                            DbType = System.Data.DbType.String,
                            Value = doc.TableName
                        },
                        new SqlParameter
                        {
                            ParameterName = "@FiledName",
                            DbType = System.Data.DbType.String,
                            Value = doc.FieldName
                        },
                        new SqlParameter
                        {
                            ParameterName = "@Description",
                            DbType = System.Data.DbType.String,
                            Value = doc.Description
                        }) > 0;
            }
        }

        public List<DBDocument> Documents
        {
            get
            {                
                if (_dbDocList == null)
                {
                    _dbDocList = SelectAll();
                    
                    //_dbDocList = new List<DBDocument>();                    
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
