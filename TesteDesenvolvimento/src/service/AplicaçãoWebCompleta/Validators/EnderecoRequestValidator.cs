using AplicaçãoWebCompleta.Models.Request;
using FluentValidation;

namespace AplicaçãoWebCompleta.Validators
{
    public class EnderecoRequestValidator : AbstractValidator<EnderecoRequest>
    {
        public EnderecoRequestValidator()
        {
            RuleFor(e => e.CEP)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Matches(@"^\d{5}-\d{3}$").WithMessage("O CEP deve estar no formato XXXXX-XXX.");

            RuleFor(e => e.Rua)
                .NotEmpty().WithMessage("A Rua é obrigatória.")
                .Length(2, 100).WithMessage("A Rua deve ter entre 2 e 100 caracteres.");

            RuleFor(e => e.Numero)
                .NotEmpty().WithMessage("O Número é obrigatório.")
                .Matches(@"^\d+$|S/N").WithMessage("O Número deve ser um número ou 'S/N' para sem número."); 

            RuleFor(e => e.Complemento)
                .MaximumLength(50).WithMessage("O Complemento deve ter no máximo 50 caracteres."); 

            RuleFor(e => e.Bairro)
                .NotEmpty().WithMessage("O Bairro é obrigatório.")
                .Length(2, 50).WithMessage("O Bairro deve ter entre 2 e 50 caracteres.");

            RuleFor(e => e.Cidade)
                .NotEmpty().WithMessage("A Cidade é obrigatória.")
                .Length(2, 50).WithMessage("A Cidade deve ter entre 2 e 50 caracteres.");

            RuleFor(e => e.UF)
                .NotEmpty().WithMessage("O Estado (UF) é obrigatório.")
                .Length(2).WithMessage("O Estado (UF) deve ter 2 caracteres.")
                .Must(uf => ValidarUF(uf)).WithMessage("O Estado (UF) deve ser um código válido."); 
        }

        private bool ValidarUF(string uf)
        {
            var estadosBrasileiros = new[]
            {
                "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
                "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
                "RS", "RO", "RR", "SC", "SP", "SE", "TO"
            };

            return estadosBrasileiros.Contains(uf);
        }
    }
}
