using System;
using System.Collections.Generic;
using System.Linq;

namespace TTT.Contracts.Base
{
    public class OperationResult<T>
    {
        public OperationResult()
        {
            this.Errors = new List<Exception>();
        }

        public OperationResult(Exception error)
            : this()
        {
            this.Errors.Add(error);
        }

        public OperationResult(IEnumerable<Exception> errors)
            : this()
        {
            this.Errors.AddRange(errors);
        }

        public bool Success => (this.Errors?.Count ?? 0) == 0 && this.Result != null;

        public T Result { get; set; }

        public string FirstErrorMessage => this.Errors?.FirstOrDefault()?.Message ?? string.Empty;

        public List<Exception> Errors { get; }

        public void AddError(Exception exception)
        {
            this.Errors.Add(exception);
        }

        public void AddErrors(IEnumerable<Exception> exceptions)
        {
            this.Errors.AddRange(exceptions);
        }
    }
}