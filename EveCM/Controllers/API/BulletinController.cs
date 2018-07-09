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
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveBulletin(int id)
        {
            Bulletin bulletinToRemove = _bulletinManager.GetBulletin(id);

            Bulletin removedBulletin = _bulletinManager.RemoveBulletin(bulletinToRemove);
            BulletinDto removedBulletinDto = _mapper.Map<BulletinDto>(removedBulletin);
            return Ok(removedBulletinDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ReplaceBulletin(int id, [FromBody]Bulletin bulletin)
        {
            Bulletin bulletinToReplace = _bulletinManager.GetBulletin(id);
            if (bulletinToReplace != null)
                _bulletinManager.Detach(bulletinToReplace);

            if (ModelState.IsValid && bulletinToReplace != null)
            {
                bulletinToReplace.Title = bulletin.Title;
                bulletinToReplace.Content = bulletin.Content;

                Bulletin bulletinReplaced = _bulletinManager.ReplaceBulletin(bulletinToReplace);
                BulletinDto bulletinReplacedDto = _mapper.Map<BulletinDto>(bulletinReplaced);
                return Ok(bulletinReplacedDto);
            }
            else
                return BadRequest();
        }
    }
}