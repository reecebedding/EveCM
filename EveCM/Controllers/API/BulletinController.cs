using AutoMapper;
using EveCM.Managers.Bulletin.Contracts;
using EveCM.Models.Bulletin;
using EveCM.Models.Bulletin.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EveCM.Controllers.API
{
    [Route("api/Bulletin")]
    [Authorize(Roles = "Corp_Member, Admin")]
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
            IEnumerable<BulletinDto> bulletingViewData = _mapper.Map<IEnumerable<BulletinDto>>(bulletinData);

            return Ok(bulletingViewData);
        }

        [HttpGet("{id}")]
        public IActionResult GetBulletin(int id)
        {
            Bulletin bulletin = _bulletinManager.GetBulletin(id);
            return Ok(bulletin);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public IActionResult NewBulletin([FromBody]BulletinDto bulletinDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Bulletin bulletin = _mapper.Map<Bulletin>(bulletinDto);

            Bulletin savedBulletin = _bulletinManager.SaveNewBulletin(bulletin, User);

            string url = Url.Action("GetBulletin", "Bulletin", new { id = bulletin.Id });

            BulletinDto savedBulletinDto = _mapper.Map<BulletinDto>(savedBulletin);

            return Created(url, savedBulletinDto);
        }
    }
}