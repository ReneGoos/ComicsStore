using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model;

public class DigitalBook : MainTable
{
    public DigitalBook()
        : base()
    {
        Active = Active.active;
        Checked = YesNoInd.no;
    }

    public string Extension { get; set; }
    public int? Length { get; set; }
    [EnumDataType(typeof(BookType), ErrorMessage = "Book type value doesn't exist within enum")]
    public BookType BookType { get; set; }
    [EnumDataType(typeof(Active), ErrorMessage = "Active value doesn't exist within enum")]
    public Active Active { get; set; }
    public int FirstYear { get; set; }
    public int? ThisYear { get; set; }
    public YesNoInd Checked { get; set; }
}