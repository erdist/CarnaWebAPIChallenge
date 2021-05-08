namespace Application.Helpers
{
    public class TResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static TResponse<T> GetResult(int code, string message, T data = default(T))
        {
            return new TResponse<T>
            {
                Code = code,
                Message = message,
                Data = data
            };
        }
    }
}