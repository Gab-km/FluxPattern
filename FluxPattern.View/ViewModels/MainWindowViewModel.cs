using System;
using System.Diagnostics;
using System.Windows;
using FluxPattern.Logic;

namespace FluxPattern.View.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, ISubscriber
    {
        #region StateValue変更通知プロパティ
        private int _StateValue;
        public int StateValue
        {
            get { return _StateValue; }
            set
            {
                if (_StateValue == value)
                    return;
                _StateValue = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region AddValue変更通知プロパティ
        private int _AddValue;
        public int AddValue
        {
            get { return _AddValue; }
            set
            {
                if (_AddValue == value)
                    return;
                _AddValue = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SubtractValue変更通知プロパティ
        private int _SubtractValue;
        public int SubtractValue
        {
            get { return _SubtractValue; }
            set
            {
                if (_SubtractValue == value)
                    return;
                _SubtractValue = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            // StateValue = 0;
            Store.Subscribe(this);
            var action = ActionCreator.CreateInitial(0);
            Dispatcher.Dispatch(action);
        }

        public void Add()
        {
            // StateValue += AddValue;
            var action = ActionCreator.CreateAdd(AddValue);
            Dispatcher.Dispatch(action);
        }

        public void Subtract()
        {
            // StateValue -= SubtractValue;
            var action = ActionCreator.CreateSub(SubtractValue);
            Dispatcher.Dispatch(action);
        }

        public void Update(string context)
        {
            if (context == "State")
            {
                StateValue = Store.GetState();
            }
        }

        protected override void Dispose(bool disposing)
        {
            Store.Unsubscribe(this);
            base.Dispose(disposing);
        }
    }
}