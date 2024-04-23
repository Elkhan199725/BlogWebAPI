using BlogApp.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Models.Blog;
public class Blog : BaseEntity, IBaseAuditable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public virtual ICollection<BlogImage> BlogImages { get; set; } = new List<BlogImage>();

}
