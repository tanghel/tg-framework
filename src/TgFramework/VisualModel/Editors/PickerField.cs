using System.Collections.ObjectModel;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class PickerField : FieldBase
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
