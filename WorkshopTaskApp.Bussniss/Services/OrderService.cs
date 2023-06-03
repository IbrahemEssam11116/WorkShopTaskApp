using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Bussniss.Intrfaces;
using WorkshopTaskApp.Entity.Models;
using WorkshopTaskApp.Repository.Interfaces;

namespace WorkshopTaskApp.Bussniss.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<UserOrder> _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IGenericRepository<UserOrder> orderRepository,
                              IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> AddOrder(UserOrder userProduct)
        {
            await _orderRepository.Add(userProduct);
            var isSaved = await _unitOfWork.Save();
            return isSaved;
        }
    }
}
