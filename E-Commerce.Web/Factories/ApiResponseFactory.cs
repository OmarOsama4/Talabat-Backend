using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            var Errors = context.ModelState.Where(M => M.Value.Errors.Any())
                    .Select(M => new Shared.ErrorModels.ValidationError()
                    {
                        Field = M.Key,
                        Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                    });
            var response = new ValidationErrorToReturn()
            {
                ValidationErrors = Errors
            };

            return new BadRequestObjectResult(response);
        }
    }
}
