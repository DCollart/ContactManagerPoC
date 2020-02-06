using System.Linq;
using ContactManagerPoC.Domain.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContactManagerPoC.WebAPI.Core
{
    public static class ModelStateExtensions
    {
        public static void UpdateFromResult(this ModelStateDictionary modelState, Result result)
        {
            result.Errors.Where(e => e.ErrorType == ErrorType.AggregateNotFound)
                .ToList()
                .ForEach(e => modelState.AddModelError(e.FieldName ?? string.Empty, e.Message));
        }
    }
}