using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.ViewModel
{
    public class UserPageViewModel: ViewModelBase
    {
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private bool isLoginValid;
        public bool IsLoginValid
        {
            get { return isLoginValid; }
            set
            {
                isLoginValid = value;
                OnPropertyChanged("IsLoginValid");
            }
        }


    }
}
