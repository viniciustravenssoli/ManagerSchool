using FluentValidation;
using Manager.Domain.Entities;

namespace Manager.Domain.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade nao pode ser vazia")

                .NotNull()
                .WithMessage("A entidade nao pode ser nula.");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O nome nao pode ser nulo")

                .NotEmpty()
                .WithMessage("O nome nao pode ser vazio")

                .MinimumLength(3)
                .WithMessage("O nome deve ter 3 ou mais caracteres")

                .MaximumLength(80)
                .WithMessage("O Nome deve ter no maximo 80 caracteres");

            RuleFor(x => x.Cpf)
                .NotNull()
                .WithMessage("The CPF cannot be empty")

                .NotEmpty()
                .WithMessage("The CPF cannot be null")

                .MinimumLength(11)
                .WithMessage("The CPF cannot be less than 11 characters")

                .MaximumLength(11)
                .WithMessage("CPF cannot be more than 11 characters");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("O email nao pode ser nulo")

                .NotEmpty()
                .WithMessage("O email não pode ser vazio")

                .MinimumLength(8)
                .WithMessage("O email deve ter no minimo 10 caracteres")

                .MaximumLength(180)
                .WithMessage("O email deve ter no maximo 180 caracteres")

                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                .WithMessage("O email informado não é válido.");
            
            RuleFor(x => x.Phone)
                .NotNull()
                .WithMessage("The CPF cannot be empty")

                .NotEmpty()
                .WithMessage("The CPF cannot be null")

                .MinimumLength(11)
                .WithMessage("The CPF cannot be less than 11 characters")

                .MaximumLength(11)
                .WithMessage("CPF cannot be more than 11 characters");

            RuleFor(x => x.Rgm)
                .NotNull()
                .WithMessage("The CPF cannot be empty")

                .NotEmpty()
                .WithMessage("The CPF cannot be null")

                .MinimumLength(11)
                .WithMessage("The CPF cannot be less than 11 characters")

                .MaximumLength(11)
                .WithMessage("CPF cannot be more than 11 characters");
        }
    }
}