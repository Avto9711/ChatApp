namespace ChatApp.Core.BaseModel.BaseEntityDto
{
    public class BaseEntityDto : BaseDto.BaseDto, IBaseEntityDto
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
