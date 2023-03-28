using Disturbed.Caching.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private IDistributedCache _distributedCache; //DisturbedCache kullanabilmemiz için bu interface i inject etmemiz gerekmektedir.

        public ValuesController(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }

        [HttpGet("set")]
        public async Task<IActionResult> Set(string name, string surname)
        {
            //cache yaparken bizim kullandığımız ayar yapılarıda mevcut. Bunlardan absolute ve sliding expiration inceleyelim.
            /*
             Absolute : Cache' deki datanın ne kadar tutulacağına dair net ömrünün belirtilmesidir. Belirlenen ömür sonra erdiğinde cache direkt olarak temizlenir.

            Sliding : Cache'lenmiş datanın memory de belirtilen süre periyodu zarfında tutulmasını belirtir. Belirtilen süre periyodu içerisinde cache'e yapılan erişim neticesinde datanın ömrü bir o kadar uzatılacaktır. Aksi taktirde belirtilen süre zarfında bir erişim söz konusu olmazsa cache temizlenecektir.             
             */


            //SetString metodu ile metinsel verileri Redis üzerinde cacheleyebiliyoruz.

            await _distributedCache.SetStringAsync("name", name, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });

            //Set metodu ile ise verileri binary formatta redis üzerine cacheleyebiliyoruz.
            await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname), options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
            return Ok();
        }

        [HttpPost("setObject")]
        public async Task<IActionResult> SetObject(User user)
        {
            //Bu controller metodunda biz bir classı veyahut bir objeyi nasıl redis te saklayabiliyoruz. Buna değiniyor olacağız.

            // JSON serileştirme ayarları
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            // Nesneyi JSON'a dönüştürme
            string userJson = JsonConvert.SerializeObject(user, settings);

            //Dönüştürülmüş user objesini redis e gönderiyoruz.
            await _distributedCache.SetStringAsync("user", userJson, options: new());

            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            //GetString metodu ile metinsel verileri Redis üzerinden okuyabiliyoruz elde ediyoruz.
            var name = await _distributedCache.GetStringAsync("name");

            //Get metodu ile metinsel verileri Redis üzerinden binary formatta okuyabiliyoruz elde ediyoruz.
            var surnameBinary = await _distributedCache.GetAsync("surname");
            var surname = Encoding.UTF8.GetString(surnameBinary);
            return Ok(new
            {
                name,
                surname
            });
        }

        [HttpGet("getObject")]
        public async Task<IActionResult> GetObject()
        {
            //GetString metodu ile metinsel verileri Redis üzerinden okuyabiliyoruz elde ediyoruz.
            string? user = await _distributedCache.GetStringAsync("user");

            // JSON verisini C# nesnesine dönüştürme
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            User? userObject = JsonConvert.DeserializeObject<User>(user, settings);

            return Ok(userObject);

        }
    }
}



