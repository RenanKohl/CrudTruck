using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Volvo.CrudTruck.Application.Models
{
    public class BaseModel<T>
    {
        public BaseModel(bool error, string message, T data, ValidationResult[] validationResults) : this(error, message, data) => this.ValidationResults = validationResults;
        public BaseModel(bool error, string message, T data) : this(error, message) => this.Data = data;
        public BaseModel(bool error, string message)
        {            
            Message = message;            
            Error = error;
        }

        public T Data { get; set; }

        public string Message { get; set; }

        public ValidationResult[] ValidationResults { get; set; }

        public bool Error { get; set; }


        public static ValidationResult[] SerializeErrors(List<FluentValidation.Results.ValidationFailure> errors)
        {
            List<ValidationResult> validations = new List<ValidationResult>();
            foreach (var failure in errors)
            {
                validations.Add(new ValidationResult(failure.ErrorMessage, new[] { failure.PropertyName}));
            }
            return validations.ToArray();
        }
    }
}
