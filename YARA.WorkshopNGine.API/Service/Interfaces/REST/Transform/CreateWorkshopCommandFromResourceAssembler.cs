﻿using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class CreateWorkshopCommandFromResourceAssembler
{
    public static CreateWorkshopCommand ToCommandFromResource(CreateWorkshopResource resource)
    {
        return new CreateWorkshopCommand(resource.Name);
    }
}