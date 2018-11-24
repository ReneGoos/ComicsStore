using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicsStore.Data.Model
{
    public abstract class MainTable : BasicsTable
    {
        public MainTable() : base()
        {
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required"), MaxLength(255)]
        public string Name { get; set; }
        public string Remark { get; set; }
    }
}