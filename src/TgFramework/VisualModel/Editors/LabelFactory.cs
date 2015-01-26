using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TgFramework.VisualModel.Editors
{
    public class LabelFactory : IEditorFactory<EditFieldBase>
    {
        public System.Windows.DependencyProperty EditProperty
        {
            get { return Label.ContentProperty; }
        }

        public System.Windows.UIElement CreateElement(EditFieldBase field)
        {
            return new Label();
        }
    }
}
