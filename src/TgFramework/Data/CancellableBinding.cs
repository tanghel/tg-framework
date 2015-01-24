using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TgFramework.Data
{
    public class CancellableBinding : Binding
    {
        public CancellableBinding()
        {
            this.ValidationRules.Add(new CancellableValidationRule());
        }
    }
}
