using Microsoft.Extensions.Options;
using NSE.Core.Communication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public interface IComprasBffServices
    {
        Task<CarrinhoViewModel> ObterCarrinho();
        Task<int> ObterQuantidadeCarrinho();
        Task<ResponseResult> AdicionarItemCarrinho( ItemCarrinhoViewModel produto );
        Task<ResponseResult> AtualizarItemCarrinho( Guid produtoId, ItemCarrinhoViewModel produto );
        Task<ResponseResult> RemoverItemCarrinho( Guid produtoId );
    }
    public class ComprasBffService : Service, IComprasBffServices
    {
        private readonly HttpClient _httpClient;

        public ComprasBffService( HttpClient httpClient, IOptions<AppSettings> settings )
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ComprasBffUrl);
        }

        public async Task<CarrinhoViewModel> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync( "/compras/carrinho" );

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CarrinhoViewModel>(response);  
        }

        public async Task<int> ObterQuantidadeCarrinho()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho-quantidade/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<int>(response);
        }

        public async Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel produto)
        {
            var itemContent = ObterConteudo(produto);
            var response = await _httpClient.PostAsync("/compras/carrinho/items/", itemContent);
            if(!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);
            return RetornoOk();
        }

        public async Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel produto)
        {
            var itemContent = ObterConteudo(produto);
            var response = await _httpClient.PostAsync($"/compras/carrinho/items/{produto.ProdutoId}", itemContent);
            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);
            return RetornoOk();
        }

        public async Task<ResponseResult> RemoverItemCarrinho(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"/compras/carrinho/items/{produtoId}");
            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);
            return RetornoOk();
        }
    }
}
