using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsLibrary.EditModels
{
    public class TableEditModel : BasicEditModel
    {
        private string _name;
        private string _remark;
        private int? _id;

        public int? Id { get => _id; set { _id = value; RaisePropertyChanged(); } }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public string Remark
        {
            get => _remark;
            set
            {
                _remark = value;
                RaisePropertyChanged();
            }
        }
    }
}
