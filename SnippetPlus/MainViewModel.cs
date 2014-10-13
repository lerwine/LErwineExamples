using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SnippetPlus
{
    public class MainViewModel : DependencyObject
    {
        #region HostWindow Property Members

        public const string PropertyName_HostWindow = "HostWindow";

        public static readonly DependencyProperty HostWindowProperty =
            DependencyProperty.Register(MainViewModel.PropertyName_HostWindow, typeof(Window), typeof(MainViewModel),
                new PropertyMetadata(null));

        public Window HostWindow
        {
            get { return (Window)(this.GetValue(MainViewModel.HostWindowProperty)); }
            set { this.SetValue(MainViewModel.HostWindowProperty, value); }
        }

        #endregion

        #region ClassMenuClick Command Property Members

        public event EventHandler ClassMenuClick;

        private Events.RelayCommand _classMenuClickCommand = null;

        public Events.RelayCommand ClassMenuClickCommand
        {
            get
            {
                if (this._classMenuClickCommand == null)
                    this._classMenuClickCommand = new Events.RelayCommand(this.OnClassMenuClick);

                return this._classMenuClickCommand;
            }
        }

        protected virtual void OnClassMenuClick(object parameter)
        {
            if (this.ClassMenuClick != null)
                this.ClassMenuClick(this, EventArgs.Empty);
        }

        #endregion

        #region PropertyMenuClick Command Property Members

        public event EventHandler PropertyMenuClick;

        private Events.RelayCommand _propertyMenuClickCommand = null;

        public Events.RelayCommand PropertyMenuClickCommand
        {
            get
            {
                if (this._propertyMenuClickCommand == null)
                    this._propertyMenuClickCommand = new Events.RelayCommand(this.OnPropertyMenuClick);

                return this._propertyMenuClickCommand;
            }
        }

        protected virtual void OnPropertyMenuClick(object parameter)
        {
            if (this.PropertyMenuClick != null)
                this.PropertyMenuClick(this, EventArgs.Empty);
        }

        #endregion
    }
}
