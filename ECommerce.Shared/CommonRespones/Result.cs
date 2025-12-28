using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CommonRespones
{
    public class Result
    {
        private readonly List<Error> _errors = [];
        public bool IsSuccess => _errors.Count == 0;
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Error> Errors => _errors;

        protected Result()
        {
        }
        protected Result(Error error)
        {
            _errors.Add(error);
        }
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }
        public static Result Ok()
        {
            return new Result();
        }
        public static Result Fail(Error error)
        {
            return new Result(error);
        }
        public static Result Fail(List<Error> errors)
        {
            return new Result(errors);
        }
    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue Value =>IsSuccess?   _value :throw new InvalidOperationException("You Cannot Access the value in case of failure cenario");
        private Result(TValue value): base()
        {
            _value = value;
        }

        private Result(Error error) : base(error)
        {
            _value = default!;
        }

        private Result(List<Error> errors) : base(errors)
        {
            _value = default!;
        }
        public static Result<TValue> Ok(TValue value)
        {
            return new Result<TValue>(value);
        }
        public static new Result<TValue> Fail(Error error)
        {
            return new Result<TValue>(error);
        }
        public static new Result<TValue> Fail(List<Error> errors)
        {
            return new Result<TValue>(errors);
        }

    }
}
