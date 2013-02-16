using System.Collections.Generic;

namespace TaskManagement.Web.Models.Data
{
    public interface IAreaRepository : IRepoistory<Area>
    {
        IEnumerable<EnumEntity> FindAllAndWrapToEnumEntity();
    }
}
