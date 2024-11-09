using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OverSightTest;

public record Response<TResult> 
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


