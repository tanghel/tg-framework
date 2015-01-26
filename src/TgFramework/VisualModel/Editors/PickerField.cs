using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgFramework.VisualModel.Editors
{
    public class PickerField : EditFieldBase
    {
        #region Properties

        public ObservableCollection<PickerItem> Items { get; set; }

        #endregion

        #region Constructors

        public PickerField()
        {
            this.Items = new ObservableCollection<PickerItem>();
        }

        #endregion
    }
}
