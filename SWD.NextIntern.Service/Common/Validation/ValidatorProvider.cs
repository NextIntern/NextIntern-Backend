using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SWD.NextIntern.Service.Common.Validation
{
    public class ValidatorProvider : IValidatorProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IValidator<T> GetValidator<T>()
        {
            return _serviceProvider.GetService<IValidator<T>>()!;
        }
    }
}
