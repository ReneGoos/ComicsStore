using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Search;
using System;

namespace ComicsLibrary.ViewModels
{
    public class CodeViewModel : BasicTableViewModel<ICodesService, CodeInputModel, CodeInputModel, CodeOutputModel, BasicSearchModel, CodeEditModel>
    {
        public CodeViewModel(ICodesService codesService,
            IMapper mapper) : base(codesService, mapper)
        {
        }
    }
}
