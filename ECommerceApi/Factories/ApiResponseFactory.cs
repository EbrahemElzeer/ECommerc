using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerceApi.Factories
{
    public class ApiResponseFactory
    {
       public static IActionResult GenerateApiValidationResponse(ActionContext actionContext) {

          
                var erors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                var problrm = new ProblemDetails()
                {
                    Title = "Validation Errors",
                    Detail = "One or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                    Extensions =

                        { { "errors", erors }
                        },

                };
                return new BadRequestObjectResult(problrm);

            
        }
    }
}
