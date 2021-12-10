namespace FA.JustBlog.Services.Results
{
    public class ResponseResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public ResponseResult()
        {
            IsSuccess = true;
        }

        public ResponseResult(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }
}