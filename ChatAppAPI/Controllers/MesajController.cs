using ChatAppAPI.Servisler.Mesajlar;
using ChatAppAPI.Servisler.Mesajlar.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MesajController : ControllerBase
    {
        private readonly IMesajServisi _mesajServisi;

        public MesajController(IMesajServisi mesajServisi)
        {
            _mesajServisi = mesajServisi;
        }

        [HttpGet]
        public async Task<IActionResult> MesajlarıGetir(string gonderenAdi, string aliciAdi)
        {
            if (ModelState.IsValid)
            {
                var messages = await _mesajServisi.MesajlarıGetir(new KullaniciMesajlariGetirDTO { GonderenKullaniciAdi = gonderenAdi, AliciKullaniciAdi = aliciAdi });
                if (!messages.Any()) return NotFound();
                return Ok(messages);
            }
            return BadRequest();
        }


        [HttpPatch]
        public async Task<IActionResult> MesajlariGorulduYap([FromBody] MesajlariGorulduYapDTO setUserMessages)
        {
            if (ModelState.IsValid)
            {
                if (await _mesajServisi.MesajlariGorulduYap(setUserMessages)) return Ok();
                return NotFound();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> MesajlasilanKullanicilariGetir()
        {
            string? mevcutKullaniciId = User.Identity?.Name;

            if (mevcutKullaniciId == null) return Unauthorized();

            var messagedUsers = await _mesajServisi.MesajlasilanKullanicilariGetir(mevcutKullaniciId);
            if (messagedUsers == null) return NotFound();

            return Ok(messagedUsers);
        }
    }
}
