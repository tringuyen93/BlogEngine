using BlogEngine.Service.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace BlogEngine.Service.Interfaces
{
    public interface IBlogService
    {
        IList<BlogDTO> GetAll();
    }
}
