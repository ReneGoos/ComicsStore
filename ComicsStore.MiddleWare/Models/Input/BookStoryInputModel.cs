using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookStoryInputModel
    {
        public int StoryId { get; set; }

        public StoryInputModel Story { get; set; }
    }
}
