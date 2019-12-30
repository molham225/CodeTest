using CodeTest.Enums;
using CodeTest.Interfaces;
using CodeTest.Model;
using MongoDB.Bson;
using Newtonsoft.Json;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeTest.Services
{
    public class ContactService:IContactService
    {
        private readonly IContactRepository _contact;
        private readonly IAddedColumnRepository _addedColumn;
        private readonly ISequenceRepository _sequence;
        public ContactService(IContactRepository _contact,IAddedColumnRepository _addedColumn,ISequenceRepository _sequence)
        {
            this._contact = _contact;
            this._addedColumn = _addedColumn;
            this._sequence = _sequence;
        }

        public async Task<ResultModel<Dictionary<string, object>>> Create(Dictionary<string,object> model)
        {
            try
            {
                IEnumerable<AddedColumn> addedColumns = await _addedColumn.GetAll();
                Contact entity = MapToEntity(model,addedColumns);
                entity = await _contact.Add(entity);
                await _contact.Commit();
                model = MapToModel(entity);
                return ResultModel<Dictionary<string, object>>.GetSuccessResult(model);
            }
            catch (Exception e)
            {

                await _contact.Abort();
                ResultErrorModel error = new ResultErrorModel(e);
                return ResultModel<Dictionary<string, object>>.GetExceptionResult(error);
            }
        }
        
        public async Task<ResultModel<object>> Delete(int Id)
        {
            try
            {
                await _contact.Remove(Id); 
                await _contact.Commit();
                return ResultModel<object>.GetSuccessResult();  
            }
            catch (Exception e)
            {
                await _contact.Abort();
                ResultErrorModel error = new ResultErrorModel(e);
                return ResultModel<object>.GetExceptionResult(error);
            }
        }

       
        public async Task<ResultModel<List<Dictionary<string, object>>>> GetAll(PaginationInfo paginationInfo)
        {
            try
            {
                IEnumerable<Contact> entities = await _contact.GetAll(paginationInfo);
                List<Dictionary<string, object>> data = (from entity in entities.AsParallel().AsOrdered() select MapToModel(entity)).ToList();
                return ResultModel<List<Dictionary<string, object>>>.GetSuccessResult(data);
            }
            catch (Exception e)
            {
                ResultErrorModel error = new ResultErrorModel(e);
                return ResultModel<List<Dictionary<string, object>>>.GetExceptionResult(error);
            }
        }

        public async Task<ResultModel<Dictionary<string, object>>> GetById(int Id)
        {
            try
            {
                Contact entity =  await _contact.GetById(Id);
                if (entity != null)
                {
                    Dictionary<string, object> data = MapToModel(entity);
                    return ResultModel<Dictionary<string, object>>.GetSuccessResult(data);
                }
                return ResultModel<Dictionary<string, object>>.GetFailureResult(new List<ResultErrorModel>() { new ResultErrorModel(ResultErrorTypeEnum.NotFoundError)});
            }
            catch (Exception e)
            {
                ResultErrorModel error = new ResultErrorModel(e);
                return ResultModel<Dictionary<string, object>>.GetExceptionResult(error);
            }
        }

        public async Task<ResultModel<Dictionary<string, object>>> Update(Dictionary<string, object> model)
        {

            try
            {
                Contact entity = await _contact.GetById(int.Parse(model["ID"].ToString()));
                if (entity == null)
                {
                    return ResultModel<Dictionary<string, object>>.GetErrorResult(new ResultErrorModel(ResultErrorTypeEnum.NotFoundError));
                }

                IEnumerable<AddedColumn> addedColumns = await _addedColumn.GetAll();

                entity = MapToEntity(model, entity, addedColumns);

                await _contact.Update(entity);
                await _contact.Commit();
                //Dictionary<string, object> data = MapToModel(entity);
                return ResultModel<Dictionary<string, object>>.GetSuccessResult(model);
            }
            catch (Exception e)
            {
                ResultErrorModel error = new ResultErrorModel(e);
                return ResultModel<Dictionary<string, object>>.GetExceptionResult(error);
            }
        }

        public async Task<ResultModel<List<Dictionary<string, object>>>> Filter(List<ColumnFilterInfo> filters, PaginationInfo paginationInfo)
        {
            try
            {
                if (filters == null || !filters.Any()) {
                    return await GetAll(paginationInfo);
                }
                IEnumerable<Contact> entities = await _contact.GetFiltered(filters, paginationInfo);
                List<Dictionary<string, object>> data = (from entity in entities.AsParallel().AsOrdered() select MapToModel(entity)).ToList();
                return ResultModel<List<Dictionary<string, object>>>.GetSuccessResult(data);
            }
            catch (Exception e)
            {
                ResultErrorModel error = new ResultErrorModel(e);
                return ResultModel<List<Dictionary<string, object>>>.GetExceptionResult(error);
            }
        }

        //private Expression<Func<Contact, bool>> getExpression(List<ColumnFilterInfo> filters)
        //{
        //    var discountFilter = "album => album.Quantity > 0";
        //    var options = ScriptOptions.Default.AddReferences(typeof(Album).Assembly);

        //    Func<Album, bool> discountFilterExpression = await CSharpScript.EvaluateAsync<Func<Album, bool>>(discountFilter, options);

        //    var discountedAlbums = albums.Where(discountFilterExpression);
        //    Expression<Func<Contact, bool>> where = t => true;
        //    foreach (ColumnFilterInfo filter in filters) {
        //        if (filter.EntityName == typeof(Contact).Name) {
        //            switch (filter.FilterType) {
        //                case FilterTypeEnum.Equals.ToString():
        //                    where.
        //                    break;
        //            }
        //        }
        //    }


        //}

        private Contact MapToEntity(Dictionary<string, object> model, IEnumerable<AddedColumn> AddedColumns)
        {
            try
            {
                List<AddedColumn> ContactAddedColumns = AddedColumns.Where(t => t.EntityName == typeof(Contact).Name).ToList();
                List<AddedColumn> CompanyAddedColumns = AddedColumns.Where(t => t.EntityName == typeof(Company).Name).ToList();
                Contact contact = new Contact();
                contact.Name = model["Name"].ToString();

                foreach (AddedColumn column in ContactAddedColumns)
                {
                    BsonElement bsonElement = new BsonElement(column.ColumnName, (BsonValue)model[column.ColumnName]);
                    contact.AddedColumns.Add(bsonElement);
                }

                contact.Company = new List<Company>();
                //JsonObject comp = JsonObject.Parse(model["Company"].ToString());

                JsonArrayObjects companyList = JsonObject.ParseArray(model["Company"].ToString());

                foreach (JsonObject element in companyList.AsEnumerable())
                {
                    Company company = new Company();
                    company.ID = _sequence.GetNextSequenceValue(typeof(Company).Name);
                    company.Name = element["Name"].ToString();
                    company.NumberOfEmployees = int.Parse(element["NumberOfEmployees"].ToString());
                    foreach (AddedColumn column in CompanyAddedColumns)
                    {
                        BsonElement bsonElement = new BsonElement(column.ColumnName, (BsonValue)element[column.ColumnName]);
                        company.AddedColumns.Add(bsonElement);
                    }
                    contact.Company.Add(company);
                }
                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping contact company fields !!");
            }
        }

        private Contact MapToEntity(Dictionary<string, object> model,Contact contact, IEnumerable<AddedColumn> AddedColumns)
        {
            try
            {
                List<AddedColumn> ContactAddedColumns = AddedColumns.Where(t => t.EntityName == typeof(Contact).Name).ToList();
                List<AddedColumn> CompanyAddedColumns = AddedColumns.Where(t => t.EntityName == typeof(Company).Name).ToList();
                contact.Name = model["Name"].ToString();

                foreach (AddedColumn column in ContactAddedColumns)
                {
                    int index = contact.AddedColumns.IndexOfName(column.ColumnName);
                    BsonElement bsonElement = new BsonElement(column.ColumnName, (BsonValue)model[column.ColumnName]);
                    if (index >= 0)
                    {
                        contact.AddedColumns.SetElement(index,bsonElement);
                    }
                    else
                    {
                        contact.AddedColumns.Add(bsonElement);
                    }
                }

                JsonArrayObjects companyList = JsonObject.ParseArray(model["Company"].ToString()); 
                
                //List<Dictionary<string, object>> companies = (List<Dictionary<string, object>>)model["Company"];
                //remove deleted companies
                List<int> CompanyIds = companyList.Select(t => int.Parse(t["ID"] == null?"0": t["ID"].ToString())).Distinct().ToList();
                contact.Company.RemoveAll(t=> !CompanyIds.Any(c=> c == t.ID));

                foreach (JsonObject element in companyList.AsEnumerable())
                {
                    Company company;
                    company = contact.Company.Where((t,i) => t.ID == int.Parse(element["ID"].ToString())).FirstOrDefault();
                    //Handle Added Companies
                    if (company == null)
                    {
                        company = new Company();
                        company.ID = _sequence.GetNextSequenceValue(typeof(Company).Name);
                    }
                    else {
                        contact.Company.Remove(company);
                    }

                    company.Name = (string)element["Name"];
                    company.NumberOfEmployees = int.Parse(element["NumberOfEmployees"].ToString());

                    foreach (AddedColumn column in CompanyAddedColumns)
                    {
                       
                            int index = company.AddedColumns.IndexOfName(column.ColumnName);
                            BsonElement bsonElement = new BsonElement(column.ColumnName, (BsonValue)element[column.ColumnName]);
                            if (index >= 0)
                            {
                                company.AddedColumns.SetElement(index, bsonElement);
                            }
                            else
                            {
                                company.AddedColumns.Add(bsonElement);
                            }
                    }
                    contact.Company.Add(company);
                }
                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping contact company fields !!");
            }
        }
        private Dictionary<string, object> MapToModel(Contact entity)
        {
            try
            {
                Dictionary<string, object> model = new Dictionary<string, object>();
                model["ID"] = entity.ID;
                model["Name"] = entity.Name;
                foreach (BsonElement addedColumn in entity.AddedColumns) {
                    model[addedColumn.Name] = addedColumn.Value;
                }
                List<Dictionary<string, object>> companies = new List<Dictionary<string, object>>();
                foreach (Company company in entity.Company) {
                    Dictionary<string, object> comp = new Dictionary<string, object>();
                    comp["ID"] = company.ID;
                    comp["Name"] = company.Name;
                    comp["NumberOfEmployees"] = company.NumberOfEmployees;
                    foreach (BsonElement addedColumn in company.AddedColumns)
                    {
                        comp[addedColumn.Name] = addedColumn.Value;
                    }
                    companies.Add(comp);
                }
                model["Company"] = companies;
                //var json = JsonConvert.SerializeObject(entity);
                //var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping contact company fields !!");
            }
        }

    }
}
