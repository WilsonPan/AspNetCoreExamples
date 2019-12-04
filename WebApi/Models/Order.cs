using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        /// <value></value>
        [DisplayName("订单号")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        /// <value></value>
        public int Quantity { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        /// <value></value>
        public decimal Amount { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        /// <value></value>
        public bool IsComplete { get; set; }
    }
}