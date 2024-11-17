﻿using YARA.WorkshopNGine.API.IAM.Application.Internal.OutboundServices.ACL;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;
using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.IAM.Domain.Repositories;
using YARA.WorkshopNGine.API.IAM.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.IAM.Application.Internal.CommandServices;

public class UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork, ExternalSubscriptionService externalSubscriptionService) : IUserCommandService
{
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} already exists.");
        const long roleOwner = (long)Roles.WorkshopOwner;
        var user = new User(command, roleOwner);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            await externalSubscriptionService.CreateSubscriptionWithTrialActive(user.WorkshopId, user.Id);
        }
        catch (Exception e)
        {
           throw new Exception("An error occurred while trying to sign up the user.", e);
        }
    }

    public async Task<User> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);
        if (user is null || !userRepository.VerifyPassword(command.Username, command.Password))
            throw new Exception("Invalid username or password.");
        return user;
    }

    public async Task<User> Handle(CreateUserWithRoleClientCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} already exists.");
        const long role = (long)Roles.Client;
        var user = new User(command.Username, command.Password, role, command.WorkshopId);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while trying to create the user.", e);
        }
    }

    public async Task<User> Handle(CreateUserWithRoleMechanicCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} already exists.");
        const long role = (long)Roles.Mechanic;
        var user = new User(command.Username, command.Password, role, command.WorkshopId);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while trying to create the user.", e);
        }
    }
}