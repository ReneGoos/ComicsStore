using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<List<ExportBooksOutputModel>> GetAsync(ExportBooksSearchModel searchModel)
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

        public Task<string> GetExportAsync(ExportBooksSearchModel searchModel)
        {
            return Reports.Reports.DataExport(_exportBooksRepository, searchModel);
        }
    }
}
