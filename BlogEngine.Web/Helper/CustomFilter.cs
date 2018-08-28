using BlogEngine.WebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VMD.RESTApiResponseWrapper.Core.Extensions;

namespace BlogEngine.WebApi.Helper
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //context.Result = new ValidationFailedResult(context.ModelState);
                throw new CustomException("Validation Error", 400, AllErrors(context.ModelState));
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        private IEnumerable<ValidationError> AllErrors(ModelStateDictionary state)
        {
            var result = state.Keys
                .SelectMany(key => state[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
            return result;
        }
    }

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState) : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
