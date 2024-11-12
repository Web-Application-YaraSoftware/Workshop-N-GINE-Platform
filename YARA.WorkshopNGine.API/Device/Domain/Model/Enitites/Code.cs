using YARA.WorkshopNGine.API.Device.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Device.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Device.Domain.Model.Enitites;

public partial class Code
{
    public long Id { get; }
    public string Component { get; private set; }
    public string ErrorCode { get; private set; }
    public string Description { get; private set; }
    public DateTime lastUpdated { get; private set; }
    public ECodeState State { get; private set; }
    
    public IotDevice IotDevice { get; private set; }
    public long IotDeviceId { get; }
    
    public Code(string component, string errorCode, string description, ECodeState state)
    {
        Component = component;
        ErrorCode = errorCode;
        Description = description;
        lastUpdated = DateTime.Now;
        State = state;
    }

    
    
}