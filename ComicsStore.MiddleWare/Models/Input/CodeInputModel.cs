using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class CodeInputModel : BasicInputModel
    {
        [Required(ErrorMessage = "Code Name is required"), MaxLength(5)]
        [RegularExpression("^[A-Z]{5}$", ErrorMessage = "Code Name must be five uppercase characters")]
        public string CodeName { get; set; }
    }
}
