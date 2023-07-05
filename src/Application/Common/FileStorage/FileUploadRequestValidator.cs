namespace ScreenDrafts.Api.Application.Common.FileStorage;
public sealed class FileUploadRequestValidator : AbstractValidator<FileUploadRequest>
{
    public FileUploadRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("File name is required.")
            .MaximumLength(150);

        RuleFor(x => x.Extension)
            .NotEmpty()
                .WithMessage("Image extension cannot be empty.")
            .MaximumLength(5);

        RuleFor(x => x.Data)
            .NotEmpty()
                .WithMessage("Image data cannot be empty.");
    }
}
