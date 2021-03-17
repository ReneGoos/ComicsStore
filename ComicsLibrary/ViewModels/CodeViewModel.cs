using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ComicsLibrary.EditModels;

namespace ComicsLibrary.ViewModels
{
    public class CodeViewModel : BasicViewModel
    {
        protected readonly ICodesService _codesService;
        private CodeEditModel _code;

        private ICollection<CodeOutputModel> _codes;
        private CollectionViewSource _codesViewSource;
        private string _codeFilter;

        private void FilterResults(object sender, FilterEventArgs e)
        {
            if (_codeFilter is null || _codeFilter.Length == 0)
            {
                e.Accepted = true;
                return;
            }

            if (e.Item is CodeOutputModel code)
            {
                e.Accepted = code.Name.Contains(_codeFilter, System.StringComparison.InvariantCultureIgnoreCase);
            }
        }

        private async void GetCodesAsync()
        {
            _codes = await _codesService.GetAsync(new ComicsStore.MiddleWare.Models.Search.BasicSearchModel { });
            _codesViewSource = new CollectionViewSource
            {
                Source = _codes
            };
            _codesViewSource.Filter += FilterResults;
            _codesViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        public string CodeFilter
        {
            get
            {
                return _codeFilter;
            }
            set
            {
                _codeFilter = value;
                _codesViewSource.View.Refresh();
            }
        }

        public ICollectionView Codes
        {
            get 
            {
                return _codesViewSource.View;
            }
        }

        public CodeViewModel(ICodesService codesService,
            IMapper mapper) : base(mapper)
        {
            _codesService = codesService;

            GetCodesAsync();
        }

        public async Task<bool> GetCodeAsync(int id, bool links = true, bool forceRefresh = false)
        {
            if (!forceRefresh && id == Code.Id)
            {
                return true;
            }

            Code = Mapper.Map<CodeEditModel>(await _codesService.GetAsync(id));

            if (Code is not null)
            {
                //Code.Stories.Clear();

                //if (links)
                //{
                //    var storiesOutput = await _codesService.GetStoriesAsync(id);
                //    foreach (var s in storiesOutput)
                //    {
                //        Code.Stories.Add(mapper.Map<CodeStoryModel>(s));
                //    }
                //}

                return true;
            }

            return false;
        }

        public CodeEditModel Code
        {
            get => _code;
            private set
            {
                _code = value;
            }
        }
    }
}
