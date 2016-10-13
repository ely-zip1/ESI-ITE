using ESI_ITE.Data_Access;
using ESI_ITE.Model;
using ESI_ITE.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ESI_ITE.ViewModel
{
    public class LoginViewModel: ViewModelBase
    {
        public LoginViewModel( )
        {
            loginCommand = new DelegateCommand(validateLogin);
            Load();
        }

        #region Properties

        DataAccess db = new DataAccess();

        private string txtUsername;
        public string TxtUsername
        {
            get { return txtUsername; }
            set
            {
                txtUsername = value;
                OnPropertyChanged();
            }
        }

        private string txtPassword;
        public string TxtPassword
        {
            get { return txtPassword; }
            set
            {
                txtPassword = value;
                OnPropertyChanged();
            }
        }

        private string txtErrorMessage;
        public string TxtErrorMessage
        {
            get { return txtErrorMessage; }
            set
            {
                txtErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private bool isLoginEnabled = false;
        public bool IsLoginEnabled
        {
            get { return isLoginEnabled; }
            set
            {
                isLoginEnabled = value;
                OnPropertyChanged();
            }
        }


        #region Commands

        private DelegateCommand loginCommand;
        public ICommand LoginCommand
        {
            get { return loginCommand; }
        }

        private DelegateCommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }

        #endregion


        private bool isFirstLoad = true;

        #endregion


        private void Load( )
        {
            //    var program = new ProgramsModel();
            //    var programList = program.FetchAll();

            //    var user = new UserModel();
            //    var userList = user.FetchAll();

            //    StringBuilder sb = new StringBuilder();

            //    foreach ( var u in userList )
            //    {
            //        var _user = (UserModel)u;
            //        foreach ( var p in programList )
            //        {
            //            sb.Clear();
            //            var _prg = (ProgramsModel)p;
            //            sb.Append("insert into user_permissions values(null,");
            //            sb.Append("'" + _user.Id + "',");
            //            sb.Append("'" + _prg.Id + "'");
            //            sb.Append(")");

            //            db.Insert(sb.ToString());
            //        }
            //    }
        }

        private void validateLogin( )
        {
            if ( IsLoginEnabled )
            {
                var user = new UserModel();

                user = (UserModel)user.Fetch(TxtUsername, "code");

                if ( user.ComputeHash(user.Salt + TxtPassword) == user.Password )
                {
                    MyGlobals.MainWindow.IsLoginVisible = false;
                    MyGlobals.MainWindow.BlurIntensity = 0;
                    MyGlobals.LoggedUser = user;
                }
                else
                {
                    TxtErrorMessage = "Invalid Username or Password!";
                }
            }
        }

        #region Validation

        private string GetValidationError( string propertyName )
        {
            string error = null;

            switch ( propertyName )
            {

            }

            return error;
        }

        #endregion
    }
}
