using FluentValidation;

namespace SWD.NextIntern.Service.Common.Validation
{
    public interface IValidatorProvider
    {
        IValidator<T> GetValidator<T>();
    }
}
