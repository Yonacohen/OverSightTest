using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OverSightTest;

public record Response<TResult> : IResponse
{
    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    /// <value>
    /// The result.
    /// </value>      
    public TResult? Result { get; set; }

    public bool Success => Code == Codes.Success;
    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    /// <value>
    /// The code.
    /// </value>       
    public int Code { get; set; } = Codes.Success;

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>       
    public List<string>? Errors { get; set; } = null;


    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>
    public void SetError(int code, string error)
    {
        Code = code;
        Errors ??= new List<string>();
        Errors.Add(error);
    }

}

public static class Codes
{
    public const int Success = 0;
    public const int GeneralError = 3;
    public const int InvalidArg = 4;
}
public interface IResponse
{
    public bool Success => Code == Codes.Success;
    public int Code { get; set; }

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>       
    public List<string>? Errors { get; set; }
}

public record Response : IResponse
{
    public bool Success => Code == Codes.Success;
    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    /// <value>
    /// The code.
    /// </value>       
    public int Code { get; set; } = Codes.Success;

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>       
    public List<string>? Errors { get; set; } = null;

    /// <summary>
    /// populate the response from another response
    /// </summary>
    /// <param name="response"></param>
    public void FromResponse(IResponse response)
    {
        Code = response.Code;
        if (response.Errors != null)
            AppendError(response.Errors);
    }
    /// <summary>
    /// append errors to the response
    /// </summary>
    /// <param name="error">errors to append</param>
    public void AppendError(IEnumerable<string> error)
    {
        Errors ??= new List<string>();
        Errors.AddRange(error);
    }
    /// <summary>
    /// append an error to the response
    /// </summary>
    /// <param name="error">error to append</param>
    public void AppendError(string error)
    {
        Errors ??= new List<string>();
        Errors.Add(error);
    }

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>
    public void SetError(int code, string error)
    {
        Code = code;
        Errors ??= new List<string>();
        Errors.Add(error);
    }
}


internal static class ResponseExtension
{
    /// <summary>
    /// Returns an ActionResult based on the response code
    /// </summary>
    /// <param name="response"></param>
    /// <returns>an <see cref="ActionResult"/> </returns>
    public static ActionResult ToActionResult(this IResponse response)
    {
        switch (response.Code)
        {
            case Codes.Success:
                return new OkObjectResult(response);
           
            case Codes.GeneralError:
                return new ObjectResult(response) { StatusCode = StatusCodes.Status500InternalServerError };
           
            case Codes.InvalidArg:
                return new ObjectResult(response) { StatusCode = StatusCodes.Status500InternalServerError };
           
        }

        return new ObjectResult(response) { StatusCode = StatusCodes.Status500InternalServerError };
    }
}

