//-----------------------------------------------------------------------
// <copyright file="Data.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.Framework.Core.DTO
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    public interface IData
    {
        bool HasError { get; }
        string ErrorMessage { get; set; }
        object GetValue();
        void SetValue(object value);
    }

    public class Data<T> : IData, INotifyPropertyChanged
    {
        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                var handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(Value)));
                }
            }
        }
        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                var handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(HasError)));
                    handler(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public object GetValue()
        {
            return Value;
        }

        public void SetValue(object value)
        {
            Value = (T)value;
        }

        public Data()
        {

        }
        public Data(T value)
        {
            Value = value;
        }
        public void AddError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
