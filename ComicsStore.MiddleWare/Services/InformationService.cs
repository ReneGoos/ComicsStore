using AutoMapper;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.Data.Model.Output;

namespace ComicsStore.MiddleWare.Services;

public class InformationService : IInformationService
{
    private readonly IViewRepository<ExportBook, IdSearch> _informationRepository;
    private readonly IMapper _mapper;

    public InformationService(IViewRepository<ExportBook, IdSearch> informationRepository,
        IMapper mapper)
    {
        _informationRepository = informationRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<ExportBooksOutputModel>> GetAsync(IdSearch searchModel)
    {
        var exportBooks = await _informationRepository.GetAsync(searchModel);

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

    public async Task<string> GetExportAsync(IdSearch searchModel)
    {
        return Reports.Reports.DataExport(await _informationRepository.GetAsync(searchModel));
    }
}
