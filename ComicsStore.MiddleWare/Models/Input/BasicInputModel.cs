namespace ComicsStore.MiddleWare.Models.Input
{
    public abstract class BasicInputModel : IBasicInputModel
    {
        public string Name { get; set; }
        public string Remark { get; set; }
    }
}
