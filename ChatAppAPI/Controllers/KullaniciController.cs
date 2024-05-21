using ChatAppAPI.Servisler.Kullanicilar;
using ChatAppAPI.Servisler.Kullanicilar.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class KullaniciController(IKullaniciServisi kullaniciServisi) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> MevcutKullaniciGetir()
        {
            KullaniciGetirDTO? kullanici = await kullaniciServisi.MevcutKullaniciGetir();
            return Ok(kullanici);
        }


        [HttpGet]
        public async Task<IActionResult> TumDigerKullanicilariGetir()
        {
            IEnumerable<KullaniciGetirDTO> kullanicilar = await kullaniciServisi.TumDigerKullanicilariGetir();
            return Ok(kullanicilar);
        }
    }
}
