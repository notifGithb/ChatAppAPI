using ChatAppAPI.Servisler.Kullanicilar;
using ChatAppAPI.Servisler.Kullanicilar.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciServisi _kullaniciServisi;

        public KullaniciController(IKullaniciServisi kullaniciServisi)
        {
            _kullaniciServisi = kullaniciServisi;
        }

        [HttpGet]
        public async Task<IActionResult> MevcutKullaniciGetir()
        {
            string? mevcutKullaniciId = User.Identity?.Name;

            if (mevcutKullaniciId == null) return Unauthorized();

            KullaniciGetirDTO? kullanici = await _kullaniciServisi.MevcutKullaniciGetir(mevcutKullaniciId);

            if (kullanici != null)
            {
                return Ok(kullanici);
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> TumDigerKullanicilariGetir()
        {
            string? mevcutKullaniciId = User.Identity?.Name;

            if (mevcutKullaniciId == null) return Unauthorized();

            IEnumerable<KullaniciGetirDTO> kullanicilar = await _kullaniciServisi.TumDigerKullanicilariGetir(mevcutKullaniciId);

            if (kullanicilar.Any())
            {
                return Ok(kullanicilar);
            }
            return NotFound();
        }
    }
}
