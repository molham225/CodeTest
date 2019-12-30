using CodeTest.Interfaces;
using CodeTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Services
{
    public class AddedColumnService : IAddedColumnService
    {
        private readonly IAddedColumnRepository _addedColumn;
        private readonly ISequenceRepository _sequence;
        public AddedColumnService(IAddedColumnRepository _addedColumn,ISequenceRepository _sequence)
        {
            this._addedColumn = _addedColumn;
            this._sequence = _sequence;
        }
        public async Task<ResultModel<AddedColumnModel>> Create(AddedColumnCreateModel model)
        {
            try
            {
                AddedColumn entity = MapToEntity(model);
                entity = await _addedColumn.Add(entity);
                await _addedColumn.Commit();
                AddedColumnModel returnModel = MapToModel(entity);
                return ResultModel<AddedColumnModel>.GetSuccessResult(returnModel);
            }
            catch (Exception e)
            {

                await _addedColumn.Abort();
                ResultErrorModel error = new ResultErrorModel(e);
                return ResultModel<AddedColumnModel>.GetExceptionResult(error);
            }
        }

        private AddedColumnModel MapToModel(AddedColumn entity)
        {
            AddedColumnModel model = new AddedColumnModel();
            model.ID = entity.ID;
            model.ColumnName = entity.ColumnName;
            model.ColumnName = entity.ColumnName;
            model.EntityName = entity.EntityName;
            return model;
        }

        private AddedColumn MapToEntity(AddedColumnCreateModel model)
        {
            AddedColumn entity = new AddedColumn();
            entity.ID = (int)_sequence.GetNextSequenceValue(typeof(AddedColumn).Name);
            entity.ColumnName = model.ColumnName;
            entity.ColumnType = model.ColumnType;
            entity.EntityName = model.EntityName;
            return entity;
        }

        
    }
}
