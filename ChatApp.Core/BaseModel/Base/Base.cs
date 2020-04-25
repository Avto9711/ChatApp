namespace ChatApp.Core.BaseModel.Base
{
    public class Base : IBase
    {
        public virtual int Id { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
