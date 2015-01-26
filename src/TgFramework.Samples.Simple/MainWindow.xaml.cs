using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TgFramework.VisualModel;
using TgFramework.VisualModel.Editors;
using TgFramework.Core;

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

        public UIElement CreateElement(EditFieldBase field)
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

        private MyEntity _EditValue;

        public MyEntity EditValue
        {
            get
            {
                return _EditValue;
            }
            set
            {
                _EditValue = value;
                this.OnPropertyChanged("EditValue");
            }
        }

        public MainWindow()
        {
            //EditorFactory.Instance.RegisterDefaultLayoutManager<StackPanelLayoutManager>();
            //EditorFactory.Instance.RegisterEditor<TextEditSettings, LabelTextEditFactory>();

            InitializeComponent();

            this.EditValue = new MyEntity()
            {
                FirstName = "Andreea",
                LastName = "Patatu",
                Address = "Strada Zoreilor",
                City = "Sibiu",
                Country = "Romania",
                Gender = Gender.Female,
                Birthdate = new DateTime(1989, 2, 16)
            };

            this.DataContext = this;

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var field in propertyContainer.Fields)
            {
                field.Validating += (s, args) => args.Result = true;
                field.ValueChanged += (s, args) => MessageBox.Show("Value changed to: ", args.Value.ToStringNN());
            }
        }
    }
}
