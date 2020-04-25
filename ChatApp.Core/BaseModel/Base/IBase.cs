namespace ChatApp.Core.BaseModel.Base
{
    public interface IBase
    {
        int Id { get; set; }
        bool Deleted { get; set; }
    }
}
