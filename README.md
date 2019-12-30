# CodeTest
Add Column To Company Entity: <br />
{"EntityName":"Company","ColumnName":"CreateDate","ColumnType":"date"} <br />
Add Column To Contact Entity: <br />
{"EntityName":"Contact","ColumnName":"CreateDate","ColumnType":"date"} <br />
Insert Contact Company : <br />
{"Name":"Contact1","Company":[{"Name":"Company1","NumberOfEmployees":100,"CreateDate":"24/05/2019"},{"Name":"Company2","NumberOfEmployees":100,"CreateDate":"24/05/2021"}]} <br />
Filter Contact By Company Create Date:
/api/Contact/Filter?CurrentPageNumber=1&PageSize=10 <br/>
[
  {
    "entityName": "Company",
    "columnName": "CreateDate",
    "value": "24/05/2021",
    "filterType": 0
  }
]
