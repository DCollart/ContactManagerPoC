using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManagerPoC.Domain.Core
{
    public class Result
    {
        public List<Error> Errors { get; }

        protected Result()
        {
            Errors = new List<Error>();
        }

        protected Result(Error error)
        {
            Errors = new List<Error>()
            {
                error
            };
        }

        protected Result(IEnumerable<Error> errors)
        {
            Errors = errors.ToList();
        }

        public bool IsSuccess => !Errors.Any();

        public bool IsFailure => !IsSuccess;

        public static Result Success() => new Result();

        public static Result Fail(Error error) => new Result(error);

        public static Result Fail(IEnumerable<Error> errors) => new Result(errors);
    }

    public class Result<TResult> : Result
    {
        public TResult Item { get; }

        protected Result(TResult result)
        {
            Item = result;
        }

        protected Result(Error error) : base(error)
        {
        }

        protected Result(IEnumerable<Error> errors) : base(errors)
        {       
        }

        public static Result<TResult> Success(TResult result) => new Result<TResult>(result);

        public static Result<TResult> Fail(IEnumerable<Error> errors) => new Result<TResult>(errors);
    }
}
