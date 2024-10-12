namespace DevFreela.Application.Models.Result
{
    public class ResultViewModel
    {
        public ResultViewModel(bool isSucess = true, string message = " ")
        {
            IsSucess = isSucess;
            Message = message;
        }

        public bool IsSucess { get; private set; }
        public string Message { get; private set; }

    }
    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data, bool isSucess = true, string message = "")
            : base(isSucess, message) 
        {
            Data = data;
        }

        public T? Data { get; private set; }

        public static ResultViewModel<T> Sucess( T data)
            => new (data);
        public static ResultViewModel<T> Error(string message)
             => new(default, false, message);

    }
}