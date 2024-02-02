using FluentValidation;
using PicPay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Validators
{
    public class UserValidator : AbstractValidator<UsuarioModel>
    {

        public UserValidator() {


           
            RuleFor(usuario => usuario.nomeCompleto).NotEmpty().WithMessage("O nome completo é obrigatório.");
            RuleFor(usuario => usuario.cpf).NotEmpty().WithMessage("O CPF é obrigatório.")
                                           .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$").WithMessage("Formato de CPF inválido.");
            RuleFor(usuario => usuario.email).NotEmpty().WithMessage("O e-mail é obrigatório.")
                                             .EmailAddress().WithMessage("Formato de e-mail inválido.");
            RuleFor(usuario => usuario.senha).NotEmpty().WithMessage("A senha é obrigatória.")
                                             .MinimumLength(4).WithMessage("A senha deve ter no mínimo 4 caracteres.");
            RuleFor(usuario => usuario.saldo).GreaterThanOrEqualTo(0).WithMessage("O saldo não pode ser negativo.");
            RuleFor(usuario => usuario.telefone).NotEmpty().WithMessage("O telefone é obrigatório.")
                                                .Matches(@"^\(\d{2,}\) \d{4,}-\d{4}$").WithMessage("Formato de telefone inválido.");


        }
    }
}
