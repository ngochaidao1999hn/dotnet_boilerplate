namespace Application.Models
{
    public class ApiResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public IList<T>? ListData { get; set; }

        public void ResponseOk(string? message = null, T? data = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public void responseOk(string? message = null, IList<T>? listData = null)
        {
            Success = true;
            Message = message;
            ListData = listData;
        }

        public void ResponseError(string? message = null, T? data = null)
        {
            Success = false;
            Message = message;
            Data = data;
        }
    }
}