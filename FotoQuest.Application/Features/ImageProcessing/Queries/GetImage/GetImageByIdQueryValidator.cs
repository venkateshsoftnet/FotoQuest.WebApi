using FluentValidation;

using FotoQuest.Application.Features.Images.Queries.GetImageById;
using FotoQuest.Application.Interfaces.Repositories;

namespace FotoQuest.Application.Features.Products.Commands.CreateProduct
{
    public class GetImageByIdQueryValidator : AbstractValidator<GetImageByIdQuery>
    {
        private readonly IImageRepositoryAsync imageRepositoryAsync;

        public GetImageByIdQueryValidator(IImageRepositoryAsync imageRepositoryAsync)
        {
            this.imageRepositoryAsync = imageRepositoryAsync;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ImageType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(customer => customer.CustomSize)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .When(customer => customer.ImageType == Domain.Entities.ImageType.Custom);
        }
    }
}
