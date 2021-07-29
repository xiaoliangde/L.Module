using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L.Datas
{
    public interface IBean<ID> //where ID : IConvertible
    {
        ID Id { get; }
    }
    public interface IBean:IBean<object>
    { 
        public const string __KEY_NAME = nameof(Id);
    }

    /// <summary>
    /// 主键默认名称，其次Key标识
    /// </summary>
    /// <typeparam name="ID"></typeparam>
    public abstract class BeanBase<ID> :NotifyBeanBase,IBean//, IBean<ID> //where ID : IConvertible
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Description("标识码")]
        public virtual ID Id { get; set; }

        object IBean<object>.Id { get; }
    }

    public class BeanInt32 : BeanBase<int>
    {
        public override int Id { get; set; } = -1;
    }
    public class BeanInt64 : BeanBase<long>
    {
        public override long Id { get; set; } = -1;
    }

}
