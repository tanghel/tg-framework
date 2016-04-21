using System;
using System.Collections.ObjectModel;
using System.Windows;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class PickerField : FieldBase
    {
        #region Constructors

        public PickerField()
        {
            Items = new ObservableCollection<PickerItem>();

            if (this.Editor != null)
            {
                this.Editor.AddValueChanged(this.EditProperty, Editor_ValueChanged);
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<PickerItem> Items { get; set; }

        #endregion

        #region Event Handlers

        private void Editor_ValueChanged(object sender, EventArgs args)
        {
            if (this.Items == null)
            {
                this.Items = new ObservableCollection<PickerItem>();
            }

            if (this.Items.Count == 0)
            {
                var dependencyObject = sender as DependencyObject;
                if (dependencyObject != null)
                {
                    var editValue = dependencyObject.GetValue(EditProperty);
                    if (editValue != null && editValue is Enum)
                    {
                        foreach (Enum enumValue in Enum.GetValues(editValue.GetType()))
                        {
                            var item = new PickerItem()
                            {
                                Id = enumValue.ToString(),
                                Title = enumValue.ToString()
                            };

                            this.Items.Add(item);
                        }
                    }
                }
            }
        }

        #endregion
    }
}