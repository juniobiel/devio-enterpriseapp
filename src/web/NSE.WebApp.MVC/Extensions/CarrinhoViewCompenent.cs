using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class CarrinhoViewCompenent : ViewComponent
    {
        private readonly IComprasBffServices _comprasBffService;

        public CarrinhoViewCompenent( IComprasBffServices comprasBffService )
        {
            _comprasBffService = comprasBffService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _comprasBffService.ObterQuantidadeCarrinho());
        }
    }
}
