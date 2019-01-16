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
    public class ExportMementoService : IExportMementoService
    {
        private readonly IExportMementoRepository _exportMementoRepository;

        public ExportMementoService(IExportMementoRepository exportMementoRepository
            )
        {
            _exportMementoRepository = exportMementoRepository;
        }

        public async Task<List<ExportMementoOutputModel>> GetAsync(ExportMementoSearchModel searchModel)
        {
            var exportMemento = await _exportMementoRepository.GetAsync(searchModel);

            try
            {
                var exportMementoOutput = Mapper.Map<List<ExportMementoOutputModel>>(exportMemento);

                return exportMementoOutput;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<string> GetExportAsync()
        {
            return Reports.Reports.DataExport(_exportMementoRepository, "", true, true, true);
        }
    }
}
