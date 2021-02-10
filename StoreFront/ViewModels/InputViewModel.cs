using AutoMapper;
using StoreFront.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.ViewModels
{
    public abstract class InputViewModel
    {
        protected readonly IMapper mapper;

        public InputViewModel(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}
