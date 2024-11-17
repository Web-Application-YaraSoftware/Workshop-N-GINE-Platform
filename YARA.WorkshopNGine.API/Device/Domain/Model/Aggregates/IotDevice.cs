using YARA.WorkshopNGine.API.Device.Domain.Model.Enitites;

namespace YARA.WorkshopNGine.API.Device.Domain.Model.Aggregates;

public partial class IotDevice
{
    public long Id { get;  }
    public long VehicleId { get; private set; }
    public List<Code> CodeList { get; private set; }
    
    public IotDevice() { }

    
    public IotDevice(List<Code> codeList, long vehicleId)
    {
        VehicleId = vehicleId;
        CodeList = codeList;
    }
    
}