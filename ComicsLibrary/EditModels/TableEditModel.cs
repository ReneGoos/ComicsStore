namespace ComicsLibrary.EditModels
{
    public class TableEditModel : BasicEditModel
    {
        private string _name;
        private string _remark;
        private int? _id;

        public TableEditModel()
        { 
        }

        public int? Id { get => _id; set { Set(ref _id, value); } }
        public string Name { get => _name; set { Set(ref _name, value); } }
        public string Remark { get => _remark; set { Set(ref _remark, value); } }
    }
}
