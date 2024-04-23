using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Models.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public bool IsActive { get; set; } = true;
}
