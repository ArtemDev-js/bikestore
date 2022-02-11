namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)  //Bad request
        {
        }
        public IEnumerable<string> Errors { get; set; }
        
        
    }
}