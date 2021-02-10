using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsLibrary.ViewModels
{
    public class StoryViewModel : InputViewModel
    {
        private readonly IStoriesService storyService;

        public StoryViewModel(IStoriesService storyService,
                            IMapper mapper) : base(mapper)
        {
            this.storyService = storyService;
        }
    }
}
