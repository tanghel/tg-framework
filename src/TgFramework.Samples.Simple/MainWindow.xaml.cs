using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TgFramework.VisualModel;
using TgFramework.VisualModel.API;

namespace TgFramework.Samples.Simple
{
    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public class LabelTextEditFactory : IEditorFactory
    {
        public DependencyProperty EditProperty
        {
            get { return Label.ContentProperty; }
        }

        public UIElement CreateElement(FieldBase field)
        {
            return new Label()
            {
                Background = Brushes.Red
            };
        }
    }

    public class MyEntity
    {
        [Display(Description = "First Name")]
        public string FirstName { get; set; }

        [Display(Description = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [StringLength(32)]
        public string City { get; set; }

        public string Country { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Display(Description = "Birth date")]
        public DateTime Birthdate { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface Implementation

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when a property has been changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private MyEntity _editValue;

        public MyEntity EditValue
        {
            get
            {
                return _editValue;
            }
            set
            {
                _editValue = value;
                OnPropertyChanged("EditValue");
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            EditValue = new MyEntity()
            {
                FirstName = "Andreea",
                LastName = "Patatu",
                Address = "Strada Zoreilor",
                City = "Sibiu",
                Country = "Romania",
                Gender = Gender.Female,
                Birthdate = new DateTime(1989, 2, 16)
            };

            DataContext = this;
        }
    }
}
