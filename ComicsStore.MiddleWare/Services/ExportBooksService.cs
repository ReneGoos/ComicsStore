using AutoMapper;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsStore.MiddleWare.Services
{
    public class ExportBooksService : IExportBooksService
    {
        private readonly IExportBooksRepository _exportBooksRepository;

        public ExportBooksService(IExportBooksRepository exportBooksRepository
            )
        {
            _exportBooksRepository = exportBooksRepository;
        }

        public async Task<List<ExportBooksOutputModel>> GetAsync(StorySeriesSearchModel searchModel)
        {
            var exportBooks = await _exportBooksRepository.GetAsync(searchModel);

            try
            {
                var exportBooksOutput = Mapper.Map<List<ExportBooksOutputModel>>(exportBooks);

                return exportBooksOutput;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<string> GetExportAsync(StorySeriesSearchModel searchModel)
        {
            return Reports.Reports.DataExport(_exportBooksRepository, searchModel);
        }
    }
}
