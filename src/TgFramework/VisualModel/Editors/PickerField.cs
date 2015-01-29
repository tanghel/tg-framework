using System.Collections.ObjectModel;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class PickerField : FieldBase
    {
        #region Constructors

        public PickerField()
        {
            Items = new ObservableCollection<PickerItem>();
        }

        #endregion

        #region Properties

        public ObservableCollection<PickerItem> Items { get; set; }

        #endregion
    }
}