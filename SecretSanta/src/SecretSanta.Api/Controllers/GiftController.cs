using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _GiftService;

        public GiftController(IGiftService giftService)
        {
            _GiftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
        }

        // GET api/Gift/5
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Gift> databaseUsers = _GiftService.GetGiftsForUser(userId);

            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        // PUT api/Gift/ID%20%3D%205%3BURL%20%3D%20www.mytoasters.com
        [HttpPut("{updatedGiftInfo}")]
        public ActionResult<bool> UpdateGiftForUser(int userID, string data)
        {
            if(userID <= 0)
            {
                return NotFound();
            }

            string decodedData = System.Web.HttpUtility.UrlDecode(data);

            string[] dataSet = decodedData.Split(';');
            string[] idSet = dataSet[0].Split('=');
            int giftId = int.Parse(idSet[1]);

            User user = _GiftService.FindUser(userID);
            Gift gift = new Gift();
            var gifts = user.Gifts;

            foreach (Gift g in gifts)
            {
                if (g.Id == giftId)
                {
                    gift = g;
                }
            }

            // TODO parse string for updated info for gift

            return _GiftService.EditGift(user, gift);
        }

        // DELETE api/Gift/5/4
        [HttpDelete("{uid/gid}")]
        public ActionResult<bool> DeleteGift(int userId, int giftId)
        {
            if(userId <= 0)
            {
                return NotFound();
            }

            User user = _GiftService.FindUser(userId);
            Gift gift = new Gift();
            var gifts = user.Gifts;

            foreach(Gift g in gifts)
            {
                if(g.Id == giftId)
                {
                    gift = g;
                }
            }

            return _GiftService.DeleteGift(user, gift);
        }
    }
}
