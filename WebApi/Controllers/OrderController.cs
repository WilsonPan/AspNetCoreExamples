using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// 订单模块
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [FormatFilter]
    public class OrderController : ControllerBase
    {

        readonly Models.OrderStore _orderStore = null;
        public OrderController(Models.OrderStore orderStore)
        {
            _orderStore = orderStore;
        }

        /// <summary>
        /// 查询所有订单
        /// </summary>
        [HttpGet]
        public ActionResult<List<Models.Order>> GetAll() => _orderStore.Orders;

        /// <summary>
        /// 获取订单    
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}.{format?}")]
        public ActionResult<Models.Order> GetById(int id)
        {
            var order = _orderStore.Orders.FirstOrDefault(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns>成功返回订单Id，失败返回-1</returns>
        [HttpPost]
        public ActionResult<int> Create(Models.Order order)
        {
            if (_orderStore.Orders.Any(m => m.OrderNo == order.OrderNo))
            {
                return -1;
            }

            order.Id = _orderStore.Orders.Max(m => m.Id) + 1;
            _orderStore.Orders.Add(order);

            return order.Id;
        }

        /// <summary>
        /// 更新订单
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<bool> Update(Models.Order model)
        {
            Console.WriteLine($"OrderNo:{model.OrderNo}");
            var order = _orderStore.Orders.FirstOrDefault(m => m.OrderNo == model.OrderNo);

            if (order == null)
            {
                return NotFound();
            }

            order.Amount = model.Amount;
            order.Quantity = model.Quantity;

            return true;
        }

        /// <summary>
        /// 更新订单指定信息
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH  /Order/{orderNo} 
        ///     [
        ///         {
        ///             "op": "test",
        ///             "path": "/quantity",
        ///             "value": "2"
        ///         },
        ///         {
        ///             "op": "test",
        ///             "path": "/amount",
        ///             "value": "38.28"
        ///         },
        ///         {
        ///             "op": "add",
        ///             "path": "/isComplete",
        ///             "value": "true"
        ///         },
        ///     ]
        /// </remarks>
        /// <returns>返回是否成功</returns>
        /// <response code="200">提交成功</response>
        /// <response code="400">提交参数异常</response>    
        /// <response code="404">订单号不存在</response>
        [HttpPatch("{orderNo:length(18)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> Update([FromBody] JsonPatchDocument<Models.Order> patchDoc, [FromRoute] string orderNo)
        {
            var order = _orderStore.Orders.FirstOrDefault(m => m.OrderNo == orderNo);

            if (order == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(order, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(true);
        }
    }
}