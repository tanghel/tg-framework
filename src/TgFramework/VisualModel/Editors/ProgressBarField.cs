using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class ProgressBarField : FieldBase
    {
        #region Dependency Properties

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(ProgressBarField), new PropertyMetadata(0d));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(ProgressBarField), new PropertyMetadata(0d));

        #endregion

        #region Properties

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        #endregion
    }
}
