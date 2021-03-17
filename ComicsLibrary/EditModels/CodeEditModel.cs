using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ComicsLibrary.EditModels
{
    public class CodeEditModel : TableEditModel
    {
        private ICollection<StoryCodeEditModel> _storyCodes;

        public CodeEditModel()
            : base()
        {
        }

        public ICollection<StoryCodeEditModel> StoryCodes
        {
            get => _storyCodes;
            set
            {
                _storyCodes = value;
                RaisePropertyChanged();
            }
        }
    }
}
