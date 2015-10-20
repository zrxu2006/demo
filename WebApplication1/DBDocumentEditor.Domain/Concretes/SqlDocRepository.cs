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
        List<DBTable> _dbTableList = null;

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
                string sql = @" DECLARE @CurrentUser SYSNAME
                                SELECT  @CurrentUser = USER_NAME()
                               
                                IF EXISTS ( SELECT  1
                                            FROM    sys.extended_properties p
                                            WHERE   p.major_id = OBJECT_ID(@TableName)
                                                    AND p.minor_id = 0 ) 
                                BEGIN
	                                -- 删除说明
                                    EXECUTE sp_dropextendedproperty 'MS_Description', 'user', @CurrentUser,
                                        'table', @TableName
                                END

                                -- 添加说明
                                EXECUTE sp_addextendedproperty 'MS_Description', @Description, 'user',
                                    @CurrentUser, 'table', @TableName";

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
                            Value = doc.Description ?? string.Empty
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
                string sql = @" DECLARE @CurrentUser SYSNAME
                                SELECT  @CurrentUser = USER_NAME()
                               
                                IF EXISTS ( SELECT  1
                                            FROM    sys.extended_properties p
                                            WHERE   p.major_id = OBJECT_ID(@TableName)
                                                    AND p.minor_id = ( SELECT   c.column_id
                                                                       FROM     sys.columns c
                                                                       WHERE    c.object_id = p.major_id
                                                                                AND c.name = @FiledName
                                                                     ) )  
                                BEGIN
	                                -- 删除说明
                                    EXECUTE sp_dropextendedproperty 'MS_Description', 'user', @CurrentUser,
                                        'table', @TableName, 'column', @FiledName
                                END

                                -- 添加说明
                                EXECUTE sp_addextendedproperty 'MS_Description', @Description, 'user',
                                    @CurrentUser, 'table', @TableName, 'column', @FiledName";

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
                            Value = doc.Description ?? string.Empty
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

        public List<DBTable> Tables
        {
            get
            {
                if (_dbTableList == null)
                {
                    _dbTableList = GetDBTables();
                }
                return _dbTableList;
            }
        }

        private List<DBTable> GetDBTables()
        {
            using (var context = new EFDocumentContext())
            {
                string sql = @" SELECT Name,object_id AS ObjectID FROM sys.tables WHERE type = 'U'
	                            ORDER BY name ";

                return context.Database.SqlQuery<DBTable>(sql).ToList();  
            }
        }
    }
}
