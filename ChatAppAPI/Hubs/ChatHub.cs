using ChatAppAPI.Servisler.Mesajlar;
using ChatAppAPI.Servisler.Mesajlar.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppAPI.Hubs
{
    public class ChatHub : Hub
    {

        private readonly IMesajServisi _mesajServisi;

        public static List<string> BagliKullaniciIdler { get; } = new List<string>();

        public ChatHub(IMesajServisi mesajServisi)
        {
            _mesajServisi = mesajServisi;
        }
        public async Task SendMessageToUser(string gonderenId, string aliciId, string mesaj)
        {
            MesajGonderDTO messageDto = new()
            {
                GonderilmeZamani = DateTime.Now,
                AliciId = aliciId,
                GonderenId = gonderenId,
                Text = mesaj
            };
            if (BagliKullaniciIdler.Contains(aliciId))
            {
                messageDto.GonderilmeDurumu = true;
                await Clients.Group(aliciId).SendAsync("messageToUserReceived", gonderenId, aliciId, mesaj);
            }
            await _mesajServisi.MesajEkle(messageDto);
        }

        public override async Task OnConnectedAsync()
        {
            var kullaniciId = Context.User?.Identity?.Name;

            if (kullaniciId == null)
                throw new Exception("Kullanıcı bulunamadı.");

            var outboxMessages = await _mesajServisi.GonderilemeyenMesajlariGetir(kullaniciId);
            foreach (var outboxMessage in outboxMessages)
            {
                await Clients.Group(outboxMessage.AliciId).SendAsync("messageToUserReceived", outboxMessage.GonderenId, outboxMessage.AliciId, outboxMessage.Text);
                await _mesajServisi.MesajiGonderildiYap(outboxMessage.MesajId);
            }

            lock (BagliKullaniciIdler)
            {
                if (!BagliKullaniciIdler.Contains(kullaniciId))
                    BagliKullaniciIdler.Add(kullaniciId);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, kullaniciId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var kullaniciId = Context.User?.Identity?.Name;

            if (kullaniciId == null)
            {
                var mesaj = $"kullanici bulunamadı. ex.Message: {exception?.Message}";
                throw new Exception(mesaj);
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, kullaniciId);

            lock (BagliKullaniciIdler)
            {
                BagliKullaniciIdler.Remove(
                    kullaniciId
                );
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
