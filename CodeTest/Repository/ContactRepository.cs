using CodeTest.Interfaces;
using CodeTest.Model;
using CodeTest.Utils;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Repository
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        private readonly ISequenceRepository sequence;
        private readonly IAddedColumnRepository addedColumn;
        public ContactRepository(ISequenceRepository sequence,IAddedColumnRepository addedColumn,IMongoContext context) : base(context)
        {
            this.sequence = sequence;
            this.addedColumn = addedColumn;
        }
        public override async Task<Contact> Add(Contact contact)
        {
            contact.ID = sequence.GetNextSequenceValue(typeof(Contact).Name);
            return await base.Add(contact);
        }

        public override async Task<IEnumerable<Contact>> GetFiltered(List<ColumnFilterInfo> filters, PaginationInfo paginationInfo)
        {
            IEnumerable<AddedColumn> addedColumns = await addedColumn.GetAll();

            IEnumerable<AddedColumn> contactAddedCols = addedColumns.Where(t => t.EntityName == typeof(Contact).Name);
            IEnumerable<AddedColumn> companyAddedCols = addedColumns.Where(t => t.EntityName == typeof(Company).Name);

            ConfigDbSet();
            FilterDefinition<Contact> filterDefinition =
                Builders<Contact>.Filter.Empty;

            foreach (ColumnFilterInfo filter in filters)
            {
                if (filter.EntityName == typeof(Contact).Name)
                {
                    switch (filter.ColumnName) { 
                        case "ID":
                            filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.ColumnName, Converters.ToInt( filter.Value));
                            break;
                        case "Name":
                            filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.ColumnName,  Converters.ToString(filter.Value));
                            break;
                        default:
                            AddedColumn col = contactAddedCols.FirstOrDefault(t =>t.ColumnName == filter.ColumnName);
                            if (col != null)
                            {
                                switch (col.ColumnType.ToLower()) {
                                    case "text":
                                    case "date":
                                        filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.ColumnName, Converters.ToString(filter.Value));
                                        break;
                                    case "number":
                                        filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.ColumnName, Converters.ToInt(filter.Value));
                                        break;
                                }
                            }
                            break;
                    }
                }
                else if (filter.EntityName == typeof(Company).Name)
                {
                    switch (filter.ColumnName)
                    {
                        case "ID":
                            filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.EntityName + "." + filter.ColumnName, Converters.ToInt(filter.Value));
                            break;
                        case "Name":
                            filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.EntityName + "." + filter.ColumnName, Converters.ToString(filter.Value));
                            break;
                        case "NumberOfEmployees":
                            filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.EntityName + "." + filter.ColumnName, Converters.ToInt(filter.Value));
                            break;
                        default:
                            AddedColumn col = companyAddedCols.FirstOrDefault(t =>t.EntityName == filter.EntityName && t.ColumnName == filter.ColumnName);
                            if (col != null)
                            {
                                switch (col.ColumnType.ToLower())
                                {
                                    case "text":
                                    case "date":
                                        filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.EntityName + "." + filter.ColumnName, Converters.ToString(filter.Value));
                                        break;
                                    case "number":
                                        filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.EntityName + "." + filter.ColumnName, Converters.ToInt(filter.Value));
                                        break;
                                }
                            }
                            break;
                    }
                    //filterDefinition = filterDefinition & Builders<Contact>.Filter.Eq(filter.EntityName + "." + filter.ColumnName, filter.Value);
                }

            }
            var all = await DbSet.FindAsync(filterDefinition);
            return all.ToList();
        }
    }
}
