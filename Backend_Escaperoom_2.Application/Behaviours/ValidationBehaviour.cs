using AutoMapper;
using FluentValidation;
using MediatR;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Behaviours
{
    /// <summary>
    /// Clase generica que me permite ejecutar las validaciones mediante un pipeline hacia MediatR
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, LanguagesHelper languagesHelper, IMapper mapper)
        {
            _validators = validators;
            _mapper = mapper;
            _languagesHelper = languagesHelper;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (this._validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(this._validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count() != 0)
                    throw new Exceptions.ValidationException(this._mapper.Map<IEnumerable<ValidationFailureResponse>>(failures), this._languagesHelper.ErrorValidation);

            }

            return await next();
        }
    }
}
