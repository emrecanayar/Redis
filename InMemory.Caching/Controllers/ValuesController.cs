using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IMemoryCache _memoryCache; //InMemoryCache kullanabilmemiz için bu interface i inject etmemiz gerekmektedir.

        public ValuesController(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        [HttpGet("create/{value}")]
        public IActionResult CreateCache(string value)
        {
            //InMemoryCache mekanızmasında bir veriyi cache lemek için .Set metodu kullanılır. Bu metot bizden temelde iki parametre alır bunlardan ilki cache leyecek olduğumuz verinin key' i dir. İkinci parametremiz ise direkt olarak cache leyecek olduğumuz veririn ta kendisidir. (Bu  en temel hali)

            _memoryCache.Set("name", value);


            return Ok();
        }

        [HttpGet]
        public string GetCacheValue()
        {
            //InMemoryCache mekanızmasında bir veriyi elde etmek için .Get metodu kullanılır. Bu metot bizden veriyi cachelerden oluşturduğumuz olduğumuz key i bekler. O key i verdikten sonra eğer generic olarak bir durum belirtmezsek bize object türünde belirlediğiz key neticesinde data yı object tipinde verir.

            //var data = _memoryCache.Get("name");

            //.Get metodunu bu şekilde generic tip olarak da belirltebiliriz. Belirlediğimi generic tipe göre cache lenen veriyi geriye döndürür. İki türlü kullanımıda mevcuttur. İhtiyaçlarımıza göre dilediğimizi şekillendirip kullanabiliriz.

            //string result = _memoryCache.Get<string>("name");


            //Peki bunu korunaklı olarak nasıl yapabiliriz. İşte bunun içinde TryGetValue metodunu kullanabiliriz. Bu metot içerisine aldığı key ile ilgili eğer bir data varsa geriye true yoksa da geriye false değerini dönecektir. Bunuda yukarıda bahsettiğimiz gibi istersek geriye object dönecek şekilde ayarlayabiliriz. İstersek te generic olarak istediğimiz veri tipini belirtebiliriz.

            //TryGetValue metodu iki parametre alır ilk parametre cache leme yaparken kullandığımız key 
            // İkinci parametre ise out olarak tanımlanan eğer key de veri varsa geriye veri döndürecek olan datayı temsil eder.

            if (_memoryCache.TryGetValue<string>("name", out string name)) return name;
            else return "Not Found";


            //NOT : Remove metodu ile cachelenmiş veriyi bellekten silebiliriz.



        }

        [HttpGet("createDate")]
        public IActionResult CreateCacheDate()
        {
            //InMemory cache yaparken bizim kullandığımız ayar yapılarıda mevcut. Bunlardan absolute ve sliding expiration inceleyelim.
            /*
             Absolute : Cache' deki datanın ne kadar tutulacağına dair net ömrünün belirtilmesidir. Belirlenen ömür sonra erdiğinde cache direkt olarak temizlenir.

            Sliding : Cache'lenmiş datanın memory de belirtilen süre periyodu zarfında tutulmasını belirtir. Belirtilen süre periyodu içerisinde cache'e yapılan erişim neticesinde datanın ömrü bir o kadar uzatılacaktır. Aksi taktirde belirtilen süre zarfında bir erişim söz konusu olmazsa cache temizlenecektir.             
             */

            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });


            return Ok();
        }


        [HttpGet("getDate")]
        public DateTime GetCacheDate()
        {

            return _memoryCache.Get<DateTime>("date");
        }

    }
}



