using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;

namespace ComicsStore.MiddleWare.Services.Interfaces;

public interface IInformationService
{
    Task<ICollection<ExportBooksOutputModel>> GetAsync(IdSearch searchModel);
    Task<string> GetExportAsync(IdSearch searchModel);
}