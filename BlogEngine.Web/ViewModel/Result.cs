using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.WebApi.ViewModel
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; set; }

        public string Message { get; set; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }

    public class ValidationResultModel
    {
        public bool IsError { get; set; }
        public string ExceptionMessage { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
        public ValidationResultModel(string message)
        {
            IsError = true;
            ExceptionMessage = message;
        }
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            IsError = true;
            ExceptionMessage = "Validation Failed";
            ValidationErrors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }

    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public IEnumerable<ValidationError> Erros { get; set; }

        public CustomException(string message, int statusCode = 500, IEnumerable<ValidationError> errors = null) : base(message)
        {
            StatusCode = statusCode;
            Erros = errors;
        }

        public CustomException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
