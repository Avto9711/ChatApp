namespace ChatApp.Core.BaseModel.BaseDto
{
    public class BaseDto : IBaseDto
    {
        public virtual int? Id { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
