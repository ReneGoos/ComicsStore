using AutoMapper;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.Data.Model.Output;

namespace ComicsStore.MiddleWare.Services
{
    public class ViewService : IViewService
    {
        private readonly IViewRepository<ExportBook, ViewSearch> _exportBooksRepository;
        private readonly IMapper _mapper;

        public ViewService(IViewRepository<ExportBook, ViewSearch> exportBooksRepository,
            IMapper mapper)
        {
            _exportBooksRepository = exportBooksRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ExportBooksOutputModel>> GetAsync(ViewSearch searchModel)
        {
            var exportBooks = await _exportBooksRepository.GetAsync(searchModel);

            try
            {
                var exportBooksOutput = _mapper.Map<ICollection<ExportBooksOutputModel>>(exportBooks);

                return exportBooksOutput;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetExportAsync(ViewSearch searchModel)
        {
            return Reports.Reports.DataExport(await _exportBooksRepository.GetAsync(searchModel));
        }
    }
}
