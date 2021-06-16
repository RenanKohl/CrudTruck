using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.CrudTruck.Application.Models
{
    public class BaseModel<T>
    {
        public BaseModel(bool error, string message, T data, ValidationFailure[] validationResults) : this(error, message, data) => this.ValidationResults = validationResults;
        public BaseModel(bool error, string message, T data) : this(error, message) => this.Data = data;
        public BaseModel(bool error, string message)
        {            
            Message = message;            
            Error = error;
        }

        public T Data { get; set; }

        public string Message { get; set; }

        public ValidationFailure[] ValidationResults { get; set; }

        public bool Error { get; set; }

    }
}
