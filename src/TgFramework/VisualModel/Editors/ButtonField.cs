using System.Windows;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class ButtonField : FieldBase
    {
        #region Dependency Properties

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ButtonField), new PropertyMetadata(null));

        #endregion

        #region Properties

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        #endregion
    }
}
