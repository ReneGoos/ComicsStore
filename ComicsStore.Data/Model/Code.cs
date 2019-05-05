using System.Collections.Generic;

namespace ComicsStore.Data.Model
{
    public class Code : MainTable
    {
        public Code()
            : base()
        {
            Series = new HashSet<Series>();
            Story = new HashSet<Story>();
        }

        public ICollection<Series> Series { get; set; }
        public ICollection<Story> Story { get; set; }
    }
}
