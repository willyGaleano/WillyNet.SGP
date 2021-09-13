using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Features.Estados.Commands.CreateEstado;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Core.Application.Specifications.EstadoSpecification;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Core.Application.Features.Estados.Commands.CreateEstados
{
    public class CreateEstadoCommandValidator : AbstractValidator<CreateEstadoCommand>
    {
        private readonly IRepositoryAsync<Estado> _repositoryAsync;
        public CreateEstadoCommandValidator(IRepositoryAsync<Estado> repositoryAsync)
        {
            RuleFor(p => p.EstadNomb)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(20).WithMessage("El nombre solo debe de tener {MaxLength} caracteres.")
                .MustAsync(IsUniqueNombreEstado).WithMessage("Ya existe ese estado");

            _repositoryAsync = repositoryAsync;
        }

        private async Task<bool> IsUniqueNombreEstado(string nombre, CancellationToken cancellationToken)
        {
            var result = await _repositoryAsync.ListAsync(new ExistsEstadoSpecification(nombre));
            return !(result.Count > 0);
        }
    }
}
