﻿namespace YARA.WorkshopNGine.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Username, string Password, long RoleId, long WorkshopId);