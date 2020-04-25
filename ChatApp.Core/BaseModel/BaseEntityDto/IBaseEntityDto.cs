using ChatApp.Core.BaseModel.BaseDto;

namespace ChatApp.Core.BaseModel.BaseEntityDto
{
    public interface IBaseEntityDto : IBaseDto
    {
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}
