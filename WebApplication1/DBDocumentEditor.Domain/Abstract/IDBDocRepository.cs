﻿using DBDocumentEditor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDocumentEditor.Domain.Abstract
{
    public interface IDBDocRepository
    {
        List<DBDocument> Documents { get; }
        //bool UpdateTableDescription(string description);
        bool UpdateDescription(string fieldName, string value);
        List<DBTable> Tables { get; }
    }
}
