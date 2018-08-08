using AutoMapper;
using BlogEngine.Data.Entities;
using BlogEngine.Data.Interface;
using BlogEngine.Data.Interfaces;
using BlogEngine.Service.Dtos;
using BlogEngine.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogEngine.Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork<Guid> _uow;
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        public BlogService(IUnitOfWork<Guid> uow, IMapper mapper, IBlogRepository blogRepository)
        {
            _uow = uow;
            _mapper = mapper;
            _blogRepository = blogRepository;
        }
        public IList<BlogDTO> GetAll()
        {
            var lst = _blogRepository.GetAll();
            var dtos = _mapper.Map<IList<BlogDTO>>(lst);
            return dtos;
        }
    }
}
