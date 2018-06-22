using AutoMapper;
using EveCM.Managers.Bulletin.Contracts;
using EveCM.Models.Bulletin;
using EveCM.Models.Bulletin.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EveCM.Controllers
{
    [Route("bulletin")]
    public class BulletinController : Controller
    {
        private readonly IBulletinManager _bulletinManager;
        private readonly IMapper _mapper;

        public BulletinController(IBulletinManager bulletinManager, IMapper mapper)
        {
            _bulletinManager = bulletinManager;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            IEnumerable<Bulletin> bulletinData = _bulletinManager.GetBulletins(out int totalCount);
            IEnumerable<BulletinDto> bulletingViewData = Mapper.Map<IEnumerable<BulletinDto>>(bulletinData);

            return Ok(bulletingViewData);
        }
    }
}