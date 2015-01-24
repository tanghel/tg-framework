using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgFramework.VisualModel.Editors
{
    public class PickerEditSettings : EditSettingsBase
    {
        public ObservableCollection<PickerItem> Items { get; set; }

        public PickerEditSettings()
        {
            Items = new ObservableCollection<PickerItem>();
        }
    }
}
