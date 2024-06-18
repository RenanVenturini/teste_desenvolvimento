using AplicaçãoWebCompleta.Models.Request;
using FluentValidation;

namespace AplicaçãoWebCompleta.Validators
{
    public class ConsultaCepRequestValidator : AbstractValidator<ConsultaCepRequest>
    {
        public ConsultaCepRequestValidator()
        {
            RuleFor(cep => cep.CEP)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Matches(@"^\d{8}$").WithMessage("O CEP deve conter exatamente 8 dígitos numéricos.");
        }
    }
}
