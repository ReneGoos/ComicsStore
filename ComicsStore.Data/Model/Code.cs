using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Code
    {
        public Code()
        {
            Series = new HashSet<Series>();
            Story = new HashSet<Story>();

            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Code Name is required"), MaxLength(5)]
        [RegularExpression("^[A-Z]{5}$", ErrorMessage = "Code Name must be five uppercase characters")]
        public string CodeName { get; set; }
        [Required(ErrorMessage = "Description of Code is required"), MaxLength(255)]
        public string Remark { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public ICollection<Series> Series { get; set; }
        public ICollection<Story> Story { get; set; }
    }
}
