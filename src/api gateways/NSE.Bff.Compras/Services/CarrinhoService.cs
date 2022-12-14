using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Models;
using NSE.Core.Communication;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.Bff.Compras.Services
{
    public interface ICarrinhoService
    {
        Task<CarrinhoDTO> ObterCarrinho();
        //Task<int> ObterQuantidadeCarrinho();
        Task<ResponseResult> AdicionarItemCarrinho( ItemCarrinhoDTO produto );
        Task<ResponseResult> AtualizarItemCarrinho( Guid produtoId, ItemCarrinhoDTO carrinho );
        Task<ResponseResult> RemoverItemCarrinho( Guid produtoId );
    }

    public class CarrinhoService : Service, ICarrinhoService
    {
        private readonly HttpClient _httpClient;

        public CarrinhoService( HttpClient httpClient, IOptions<AppServicesSettings> settings )
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }

        public async Task<ResponseResult> AdicionarItemCarrinho( ItemCarrinhoDTO produto )
        {
            var itemContent = ObterConteudo(produto);

            var response = await _httpClient.PostAsync("/carrinho/", itemContent);

            if(!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> AtualizarItemCarrinho( Guid produtoId, ItemCarrinhoDTO carrinho )
        {
            var itemContent = ObterConteudo(carrinho);

            var response = await _httpClient.PutAsync("/carrinho/", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<CarrinhoDTO> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("/carrinho/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CarrinhoDTO>(response);
        }

        //public async Task<int> ObterQuantidadeCarrinho()
        //{
        //    var response = await _httpClient.GetAsync("/carrinho-quantidade/");

        //    TratarErrosResponse(response);

        //    return await DeserializarObjetoResponse<int>(response);
        //}

        public async Task<ResponseResult> RemoverItemCarrinho( Guid produtoId )
        {
            var response = await _httpClient.DeleteAsync($"/carrinho/{produtoId}");

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}
