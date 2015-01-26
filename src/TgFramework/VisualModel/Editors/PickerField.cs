using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgFramework.VisualModel.Editors
{
    public class PickerField : EditField<PickerEditSettings>
    {
        #region Properties

        public ObservableCollection<PickerItem> Items
        {
            get
            {
                return this.EditSettings.Items;
            }
            set
            {
                this.EditSettings.Items = value;
            }
        }

        #endregion
    }
}
