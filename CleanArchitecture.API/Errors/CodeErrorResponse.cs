namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public CodeErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "The request has errors",
                401 => "You're not allowed to access this resource",
                404 => "The resource you're trying to access doesn't exist",
                500 => "Server errors ocurred",
                _ => string.Empty
            };
        }
    }
}
