using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class PublisherInputModel : BasicInputModel
    {
        [Required(ErrorMessage = "Publisher Name is required"), MaxLength(255)]
        public string PublisherName { get; set; }
    }
}