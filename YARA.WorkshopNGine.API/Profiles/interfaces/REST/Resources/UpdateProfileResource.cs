﻿namespace YARA.WorkshopNGine.API.Profiles.interfaces.REST.Resources;

public record UpdateProfileResource(long Id,string FirstName, string LastName, int Dni, string Email, int Age, string Location, long UserId);