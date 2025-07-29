using TaskManager.Dtos;

namespace TaskManager.Validators;
using FluentValidation;

public class TaskDtoValidator: AbstractValidator<TaskDto>
{
    public TaskDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
    }
}