using AutoMapper;
using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model.BasketModule;
using ECommerce.Service.Abstraction;
using ECommerce.Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class BasketService : IBasketService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;

        public BasketService(IMapper mapper,IBasketRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var BasketEnity=_mapper.Map<CustomerBasket>(basket);
            var createdOrUpdatedBasket=await _repository.CreateOrUpdateBasketAsync(BasketEnity);
           
             return _mapper.Map<BasketDto>(createdOrUpdatedBasket);
            

        }

        public async Task<bool> DeleteBasketById(string basketId)=>  await  _repository.DeleteBasketById(basketId);
    
        

        public async Task<BasketDto?> GetBasketAsync(string basketId)
        {
           var basket= await _repository.GetBasketAsync(basketId);
            if(basket==null)
                return null;
             return    _mapper.Map<BasketDto>(basket);
        }
    }
}
