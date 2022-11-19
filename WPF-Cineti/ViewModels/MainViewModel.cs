using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private HomeViewModel _homeViewModel;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;






        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView; set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get => _caption; set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get => _icon; set
            {

                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowUsersViewCommand { get; }

        public ICommand ShowClientsViewCommand { get; }

        public HomeViewModel HomeViewModel { get => _homeViewModel; set => _homeViewModel = value; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowUsersViewCommand = new ViewModelCommand(ExecuteShowUsersViewCommand);
            ShowClientsViewCommand = new ViewModelCommand(ExecuteShowClientsViewCommand);


            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }



        private void ExecuteShowClientsViewCommand(object obj)
        {
            CurrentChildView = new ClientViewModel();
            Caption = "Clients";
            Icon = IconChar.Aviato;
        }

        private void ExecuteShowUsersViewCommand(object obj)
        {
            CurrentChildView = new UsersViewModel();
            Caption = "Users";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Home";
            Icon = IconChar.Home;
        }





        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName} ;)";
                CurrentUserAccount.ProfilePicture = null;               
            }
            else
            {
                CurrentUserAccount.DisplayName="Invalid user, not logged in";
                //Hide child views.
            }
        }
    }
}
