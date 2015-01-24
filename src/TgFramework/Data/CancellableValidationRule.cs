using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TgFramework.Data
{
    public class CancellableValidationRule : ValidationRule
    {
        #region Private Members

        private bool isCanceling = false;

        #endregion

        #region Constructors

        public CancellableValidationRule()
        {

        }

        #endregion

        #region Private Methods

        private static void InvokeOnMainThread(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            Application.Current.Dispatcher.BeginInvoke(action);
        }

        #endregion

        #region Overrides

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            return ValidationResult.ValidResult;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo, System.Windows.Data.BindingExpressionBase owner)
        {
            if (!isCanceling)
            {
                var element = owner.Target as UIElement;
                if (element != null)
                {
                    var itemChangingAction = element.GetValidatingAction();
                    if (itemChangingAction != null)
                    {
                        var result = itemChangingAction(element, value);
                        if (result == null)
                        {
                            InvokeOnMainThread(() => element.Focus());

                            return new ValidationResult(false, null);
                        }
                        else if (result == false)
                        {
                            if (owner.Target is Selector)
                            {
                                InvokeOnMainThread(() => owner.UpdateTarget());
                            }
                            else
                            {
                                owner.UpdateTarget();
                            }

                            InvokeOnMainThread(() =>
                            {
                                isCanceling = true;
                                owner.ValidateWithoutUpdate();
                                isCanceling = false;
                            });

                            return new ValidationResult(false, null);
                        }
                        else
                        {
                            var itemChangedAction = element.GetValueChangedAction();
                            if (itemChangedAction != null)
                            {
                                InvokeOnMainThread(() => itemChangedAction.Invoke(element, value));
                            }
                        }
                    }
                }
            }

            return base.Validate(value, cultureInfo, owner);
        }

        #endregion
    }
}
