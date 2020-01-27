using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManagerPoC.Domain.Core
{
    public class Result<TError>
    {
        public List<TError> Errors { get; }

        protected Result()
        {
            Errors = new List<TError>();
        }

        protected Result(TError error)
        {
            Errors = new List<TError>()
            {
                error
            };
        }

        protected Result(IEnumerable<TError> errors)
        {
            Errors = errors.ToList();
        }

        public bool IsSuccess => !Errors.Any();

        public bool IsFailure => !IsSuccess;

        public static Result<TError> Success() => new Result<TError>();

        public static Result<TError> Fail(TError error) => new Result<TError>(error);

        public static Result<TError> Fail(IEnumerable<TError> errors) => new Result<TError>(errors);
    }

    public class Result<TError, TResult> : Result<TError>
    {
        public TResult Item { get; }

        protected Result(TResult result)
        {
            Item = result;
        }

        protected Result(TError error) : base(error)
        {
        }

        protected Result(IEnumerable<TError> errors) : base(errors)
        {       
        }

        public static Result<TError, TResult> Success(TResult result) => new Result<TError, TResult>(result);

        public static Result<TError, TResult> Fail(IEnumerable<TError> errors) => new Result<TError, TResult>(errors);
    }
}
