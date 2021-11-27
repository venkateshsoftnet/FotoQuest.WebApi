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

            RuleFor(x => x.Files)
                .Must(x => x.Count > 0).WithMessage("Files must have a total of more than 0");

            RuleForEach(x => x.Files)
                .Must(file => !string.IsNullOrEmpty(file.FileName)).WithMessage("File name is required");

            RuleForEach(x => x.Files)
                .Must(file => file.Length > 0 ).WithMessage("File content is required");

        }
    }
}
