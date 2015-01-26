using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TgFramework.VisualModel.Editors
{
    public class ButtonSettings : EditSettingsBase
    {
        //#region Dependency Properties

        //public static readonly DependencyProperty ContentProperty =
        //    DependencyProperty.Register("Content", typeof(object), typeof(ButtonSettings), new PropertyMetadata(null));

        //#endregion

        //#region Properties

        //public object Content
        //{
        //    get { return (object)GetValue(ContentProperty); }
        //    set { SetValue(ContentProperty, value); }
        //}

        //#endregion

        #region Constructors

        public ButtonSettings()
        {
            this.EditFieldChanged += (sender, args) =>
                {
                    BindingOperations.SetBinding(this, Button.ContentProperty, new Binding("Content") { Source = EditField });
                };
        }

        #endregion
    }
}
