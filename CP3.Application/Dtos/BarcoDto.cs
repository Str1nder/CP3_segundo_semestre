using CP3.Domain.Interfaces.Dtos;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CP3.Application.Dtos
{
    public class BarcoDto : IBarcoDto
    {
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Tamanho { get; set; }

        public ValidationResult Validate()
        {
            var validationContext = new ValidationContext(this, serviceProvider: null, items: null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, true);

            // Retorna o ValidationResult
            if (isValid)
            {
                return ValidationResult.Success;
            }

            // Se não for válido, retornamos os erros encontrados
            var errorMessages = string.Join(", ", validationResults.Select(x => x.ErrorMessage));
            return new ValidationResult(errorMessages);
        }

    }


    internal class BarcoDtoValidation : AbstractValidator<BarcoDto>
    {
        public BarcoDtoValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do barco é obrigatório.")
                .Length(1, 100).WithMessage("O nome do barco deve ter entre 1 e 100 caracteres.");

            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage("O modelo do barco é obrigatório.")
                .Length(1, 100).WithMessage("O modelo do barco deve ter entre 1 e 100 caracteres.");

            RuleFor(x => x.Ano)
                .GreaterThan(0).WithMessage("O ano de fabricação deve ser maior que zero.");

            RuleFor(x => x.Tamanho)
                .GreaterThan(0).WithMessage("O tamanho do barco deve ser maior que zero.");
        }
    }


}
