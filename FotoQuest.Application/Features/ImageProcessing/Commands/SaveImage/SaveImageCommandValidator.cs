using FluentValidation;

using FotoQuest.Application.Interfaces.Repositories;

namespace FotoQuest.Application.Features.Images.Commands.SaveImage
{
    public class SaveImageCommandValidator : AbstractValidator<SaveImageCommand>
    {
        private readonly IImageRepositoryAsync imageRepository;

        public SaveImageCommandValidator(IImageRepositoryAsync imageRepository)
        {
            this.imageRepository = imageRepository;

            //RuleFor(p => p.FileName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            //RuleFor(p => p.FileType)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters.");

            //RuleFor(p => p.FileContent)
            //   .NotEmpty().WithMessage("{PropertyName} is required.")
            //   .NotNull();

        }
    }
}
