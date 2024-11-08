using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

public record UpdateInterventionCommand(long VehicleId, long MechanicLeaderId, string Description, InterventionTypes Type);