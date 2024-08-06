using CryptoMarketApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMarketApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoController : Controller
    {
        private readonly CryptoServices _cryptoService;

        public CryptoController(CryptoServices cryptoService)
        {
            _cryptoService = cryptoService;
        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var crypto = await _cryptoService.GetCryptoDataAsync();
            var top10Crypto = crypto.OrderBy(c => c.Rank).Take(10).ToList();
            ViewBag.AllCrypto = crypto;
            return View(top10Crypto);
        }

        [HttpGet]
        [Route("GetCryptoData")]
        public async Task<IActionResult> GetCryptoData(string currency = "usd")
        {
            var crypto = await _cryptoService.GetCryptoDataAsync(currency);
            return Json(crypto);
        }
        [HttpGet]
        [Route("Details/{sId}")]
        public async Task<IActionResult> Details(string sId, string currency = "usd")
        {
            var crypto = await _cryptoService.GetCryptoDataAsync(currency);
            var selectedCrypto = crypto.FirstOrDefault(c => string.Equals(c.Id, sId, StringComparison.OrdinalIgnoreCase));
            if (selectedCrypto == null)
            {
                return NotFound();
            }
            var historicalData = await _cryptoService.GetCryptoHistoricalDataAsync(sId, currency);

            ViewBag.HistoricalData = historicalData;
            ViewBag.Currency = currency;
            return View(selectedCrypto);
        }
    }
}

