using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Movies.API;

public class CustomBadRequest
{
  public string Message { get; set; }
  public IDictionary<string, string[]> Errors { get; set; }
  public int Status { get; set; }
  public CustomBadRequest(ActionContext context)
  {
    Message = "The inputs supplied to the API are invalid";
    Errors = new Dictionary<string, string[]>();
    Status = 400;
            
    ConstructErrorMessages(context);
  }

  private void ConstructErrorMessages(ActionContext context)
  {
    foreach (var keyModelStatePair in context.ModelState)
    {
      var key = keyModelStatePair.Key;
      var errors = keyModelStatePair.Value.Errors;
      if (errors != null && errors.Count > 0)
      {
        if (errors.Count == 1)
        {
          var errorMessage = GetErrorMessage(errors[0]);
          Errors.Add(key, new[] { errorMessage });
        }
        else
        {
          var errorMessages = new string[errors.Count];
          for (var i = 0; i < errors.Count; i++)
          {
            errorMessages[i] = GetErrorMessage(errors[i]);
          }

          Errors.Add(key, errorMessages);
        }
      }
    }
  }

  string GetErrorMessage(ModelError error)
  {
    return string.IsNullOrEmpty(error.ErrorMessage) ?
      "The input was not valid." :
      error.ErrorMessage;
  }
}