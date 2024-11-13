namespace YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST.Resources;

public record NotificationResource(long Id, string Content, long UserId, long StateId, string Endpoints );