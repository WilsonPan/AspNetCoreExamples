using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "学生姓名")]
        [Required(ErrorMessage = "学生姓名不能为空")]
        [StringLength(50, ErrorMessage = "学生姓名最大长度为50")]
        public string Name { get; set; }
        
        [Display(Name = "年龄")]
        [Required(ErrorMessage = "年龄不能为空")]
        [Range(minimum: 10, maximum: 100, ErrorMessage = "学生年龄必须在（10 ~ 100）之间")]
        [ConcurrencyCheck]
        public int Age { get; set; }

        public int SchoolId { get; set; }

        public School School { get; set; }

        [Display(Name = "创建时间")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "最后更新时间")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? LastUpdateTime { get; set; }
    }
}