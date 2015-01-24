using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgFramework.Data
{
    public delegate void ValidatingEventHandler(object sender, ValidatingEventArgs args);

    public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs args);

    public class ValidatingEventArgs : EventArgs
    {
        public bool? Result { get; set; }

        public object Value { get; private set; }

        public ValidatingEventArgs(object value)
        {
            this.Value = value;
        }
    }

    public class ValueChangedEventArgs : EventArgs
    {
        public object Value { get; set; }

        public ValueChangedEventArgs(object value)
        {
            this.Value = value;
        }
    }
}
