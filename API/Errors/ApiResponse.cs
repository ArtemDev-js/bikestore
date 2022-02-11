namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode); // ?? if left side is null take the right one
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }
    

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
				{
					400 => "A bad request",
					401 => "You are not authorized",
					404 => "Resource was not found",
					500 => "Server error",
					_ => null   // default case
				};
    }

		}
}
