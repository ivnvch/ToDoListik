namespace ToDoList.Domain.Result
{
    public class BaseResult
    {
        public bool IsSuccess => ErrorMessage == null;
        public string ErrorMessage { get; set; }
        public int? ErrorCode { get; set; }
    }

    public class BaseResult<TEntity> : BaseResult
    {
        public TEntity Data { get; set; }

        public BaseResult()
        {
            
        }
        public BaseResult(string errorMessage, int errorCode, TEntity data)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}


