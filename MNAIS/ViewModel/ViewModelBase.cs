using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MNAIS
{
    [Serializable]
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public virtual string DisplayName { get; set; }

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected ViewModelBase() { }       
        
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {            
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        
        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {

        }

        #if DEBUG
        
        ~ViewModelBase()
        {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }

        #endif        
    }
}
