using AplicaçãoWebCompleta.Models.Request;
using FluentValidation;

namespace AplicaçãoWebCompleta.Validators
{
    public class UsuarioRequestValidator : AbstractValidator<UsuarioRequest>
    {
        public UsuarioRequestValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório.")
                .Length(2, 100).WithMessage("O Nome deve ter entre 2 e 100 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O Email é obrigatório.")
                .Matches(@"^[^@\s]+@[^@\s]+\.(com|com\.br)$").WithMessage("O Email deve ser válido, como por exemplo: exemplo@dominio.com ou exemplo@dominio.com.br");

            RuleFor(u => u.Telefone)
                .NotEmpty().WithMessage("O Telefone é obrigatório.")
                .Matches(@"^\(\d{2}\)\d{5}-\d{4}$|^\(\d{2}\)\d{4}-\d{4}$").WithMessage("O Telefone deve estar no formato (XX)XXXXX-XXXX ou (XX)XXXX-XXXX.");


            RuleFor(u => u.EnderecoRequest)
                .NotNull().WithMessage("O Endereço é obrigatório.")
                .SetValidator(new EnderecoRequestValidator()); 
        }
    }
}
