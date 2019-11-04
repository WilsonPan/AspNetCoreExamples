using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("School")]
    public class School
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "学校名称")]
        [Required(ErrorMessage = "学校名称不能为空")]
        [StringLength(100, ErrorMessage = "学校名称最大长度为100")]
        public string Name { get; set; }

        [Display(Name = "学校地址")]
        [Required(ErrorMessage = "学校地址不能为空")]
        [StringLength(200, ErrorMessage = "学校地址最大长度为200")]
        public string Address { get; set; }
        public List<Student> Students { get; set; }

        [Display(Name = "创建时间")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "最后更新时间")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? LastUpdateTime { get; set; }
    }
}