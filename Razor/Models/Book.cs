using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Razor.Models
{
    [Table("Book")]
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "书名")]
        [Required(ErrorMessage = "书名不能为空")]
        [StringLength(50, ErrorMessage = "书名最大长度为50")]
        public string Name { get; set; }

        [Display(Name = "单价")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 10000, ErrorMessage = "单价范围只能在（0.01 ~ 10000）"), DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "出版日期")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "创建时间")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "最后更新时间")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? LastUpdateTime { get; set; }
    }
}