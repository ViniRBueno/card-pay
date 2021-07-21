using Microsoft.AspNetCore.Mvc;

namespace CardPay.Controllers
{
    [Route("/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IActionResult Index()
        {
            return null;
        }
    }
}