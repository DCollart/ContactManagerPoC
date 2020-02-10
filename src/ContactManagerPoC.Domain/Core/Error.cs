using System.Collections.Generic;

namespace ContactManagerPoC.Domain.Core
{
    public class Error : ValueObject
    {
        public ErrorType ErrorType { get; }
        public string FieldName { get; }
        public string Message { get; }

        private Error(string message, string fieldName, ErrorType errorType)
        {
            ErrorType = errorType;
            FieldName = fieldName;
            Message = message;
        }

        public static Error Create(string message, string fieldName = null, ErrorType errorType = ErrorType.Validation)
        {
            return new Error(message, fieldName, errorType);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ErrorType;
            yield return FieldName;
            yield return Message;
        }
    }
}
