using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTest.Data.Models.Entities
{
    public class AddedColumn : BaseEntity<ObjectId>
    {
        public string EntityName { set; get; }
        public string ColumnName { set; get; }
        public string ColumnType { set; get; }
    }
}
