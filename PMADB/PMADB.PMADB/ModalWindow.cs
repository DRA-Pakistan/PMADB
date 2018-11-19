using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Threading;
using Microsoft.LightSwitch.Presentation.Extensions;
using System;

namespace LightSwitchApplication
{
    public class ModalWindow
    {
        private IVisualCollection _collection;
        private string _dialogName;
        private string _entityName;
        private IScreenObject _screen;
        private IContentItemProxy _window;
        private IEntityObject _entity;

        public ModalWindow(IVisualCollection visualCollection, string dialogName, string entityName = "")
        {
            _collection = visualCollection;
            _dialogName = dialogName;
            _entityName = ((entityName != "") ? entityName : _collection.Details.GetModel().ElementType.Name);
            _screen = _collection.Screen;
        }

        public void Initialise()
        {
            _window = _screen.FindControl(_dialogName);

            _window.ControlAvailable += (object s, ControlAvailableEventArgs e) => {
                var window = (ChildWindow)e.Control;

                window.Closed += (object s1, EventArgs e1) => {
                    DialogClosed(s1);
                };
            };
        }

        public bool CanAdd()
        {
            return (_collection.CanAddNew == true);
        }

        public bool CanView()
        {
            return (_collection.SelectedItem != null);
        }

        public void AddEntity()
        {
            IEntityObject result = null;

            _window.DisplayName = string.Format("Add {0}", _entityName);
            _collection.AddNew();

            OpenModalWindow();
        }

        public void ViewEntity()
        {
            _window.DisplayName = string.Format("View {0}", _entityName);

            OpenModalWindow();
        }

        private void OpenModalWindow()
        {
            _entity = _collection.SelectedItem as IEntityObject;
            _screen.OpenModalWindow(_dialogName);
        }

        public void DialogOk()
        {
            if (_entity != null)
            {
                _screen.CloseModalWindow(_dialogName);
            }
        }

        public void DialogCancel()
        {
            if (_entity != null)
            {

                _screen.CloseModalWindow(_dialogName);

                DiscardChanges();

            }
        }

        public void DialogClosed(object sender)
        {
            var window = (ChildWindow)sender;

            if (window.DialogResult.HasValue == false)
            {
                DiscardChanges();
            }
        }

        private void DiscardChanges()
        {
            if (_entity != null)
            {
                _entity.Details.DiscardChanges();
            }
        }
    }


}