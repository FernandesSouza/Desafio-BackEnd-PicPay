using FluentValidation;
using PicPay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Validators
{
    public class LojistaValidator : AbstractValidator<LojistaModel>
    {

        public LojistaValidator()
        {
            
            RuleFor(lojista => lojista.nomeCompleto).NotEmpty().WithMessage("O nome completo é obrigatório.");
            RuleFor(lojista => lojista.cpf).NotEmpty().WithMessage("O CPF é obrigatório.")
                                           .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$").WithMessage("Formato de CPF inválido.");
            RuleFor(lojista => lojista.email).NotEmpty().WithMessage("O e-mail é obrigatório.")
                                             .EmailAddress().WithMessage("Formato de e-mail inválido.");
            RuleFor(lojista => lojista.senha).NotEmpty().WithMessage("A senha é obrigatória.")
                                             .MinimumLength(4).WithMessage("A senha deve ter no mínimo 4 caracteres.");
            RuleFor(lojista => lojista.saldo).GreaterThanOrEqualTo(0).WithMessage("O saldo não pode ser negativo.");
            RuleFor(lojista => lojista.cnpj).NotEmpty().WithMessage("O CNPJ é obrigatório.")
                                            .Matches(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$").WithMessage("Formato de CNPJ inválido.");
        }

    }
}
