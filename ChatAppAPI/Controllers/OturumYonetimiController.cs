using ChatAppAPI.Servisler.OturumYonetimi;
using ChatAppAPI.Servisler.OturumYonetimi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class OturumYonetimiController : ControllerBase
    {
        private readonly IOturumYonetimi _oturumYonetimi;

        public OturumYonetimiController(IOturumYonetimi oturumYonetimi)
        {
            _oturumYonetimi = oturumYonetimi;
        }


        [HttpPost]
        public async Task<IActionResult> KayitOl([FromBody] KullaniciKayitDto model)
        {
            if (ModelState.IsValid)
            {
                await _oturumYonetimi.KayitOl(model);
                return Ok("Kullanici Kaydi Basarili");
            }
            return BadRequest("Kullanici Kaydi Basarisiz");
        }



        [HttpPost]
        public async Task<IActionResult> GirisYap([FromBody] KullaniciGirisDto model)
        {

            string? token = await _oturumYonetimi.GirisYap(model);

            if (token != null)
            {
                return Ok(token);
            }
            return NotFound("Giriş Bulunamadi");
        }

    }
}