using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace L.Datas
{
    //[AddINotifyPropertyChangedInterface]
    public abstract class NotifyBeanBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName, object before, object after)
        {
            OnPropertyChanged(propertyName);
        }
    }
    //public abstract class CompareNotifyBeanBase : NotifyBeanBase
    //{
    //    protected override void OnPropertyChanged(string propertyName, object before, object after)
    //    {
    //        if (before!=after)OnPropertyChanged(propertyName);
    //    }
    //}
}
