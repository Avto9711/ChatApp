namespace ChatApp.Core.BaseModel.BaseDto
{
    public interface IBaseDto
    {
        int? Id { get; set; }
        bool Deleted { get; set; }
    }
}
