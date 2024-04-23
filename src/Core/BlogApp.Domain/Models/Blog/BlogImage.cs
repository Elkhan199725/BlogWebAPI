using BlogApp.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Models.Blog;

public class BlogImage : BaseEntity, IBaseAuditable
{
    public string ImageUrl { get; set; }
    public int BlogId { get; set; }
    public Blog? Blog { get; set; }

    // Auditable properties
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
}
