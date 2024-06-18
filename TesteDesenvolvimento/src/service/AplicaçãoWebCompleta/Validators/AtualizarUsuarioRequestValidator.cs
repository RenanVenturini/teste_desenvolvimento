using AplicaçãoWebCompleta.Models.Request;
using FluentValidation;

namespace AplicaçãoWebCompleta.Validators
{
    public class AtualizarUsuarioRequestValidator : AbstractValidator<AtualizarUsuarioRequest>
    {
        public AtualizarUsuarioRequestValidator()
        {
            RuleFor(u => u.UsuarioId)
                .GreaterThan(0).WithMessage("O ID do usuário é Obrigatório.");

            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório.")
                .Length(2, 100).WithMessage("O Nome deve ter entre 2 e 100 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O Email é obrigatório.")
                .EmailAddress().WithMessage("O Email deve ser válido, como por exemplo: exemplo@dominio.com.");

            RuleFor(u => u.Telefone)
                .NotEmpty().WithMessage("O Telefone é obrigatório.")
                .Matches(@"^\d{11}$").WithMessage("O Telefone deve conter exatamente 11 dígitos numéricos.");

            RuleFor(u => u.Endereco)
                .NotNull().WithMessage("O Endereço é obrigatório.")
                .SetValidator(new AtualizarEnderecoRequestValidator()); 
        }
    }
}
