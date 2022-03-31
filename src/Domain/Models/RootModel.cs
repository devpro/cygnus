using System.Collections.Generic;

namespace Cygnus.Domain.Models
{
    public class RootModel
    {
        public List<EntityModel> Entities { get; set; } = new List<EntityModel>();
    }
}
