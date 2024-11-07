using System.Reflection.Metadata;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.IAM.Domain.Services;

public interface IUserCommandService
{
    Task Handle(SignUpCommand command);
    Task<User> Handle(SignInCommand command);

    Task<User> Handle(CreateUserWithRoleClientCommand command);
    
    Task<User> Handle(CreateUserWithRoleMechanicCommand command);
}