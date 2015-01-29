﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TgFramework.VisualModel.Editors
{
    public class ButtonField : EditFieldBase
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