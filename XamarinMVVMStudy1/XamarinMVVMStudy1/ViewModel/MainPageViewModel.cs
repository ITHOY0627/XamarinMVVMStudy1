using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XamarinMVVMStudy1.Model;

namespace XamarinMVVMStudy1.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableRangeCollection<UserModel> _users = new ObservableRangeCollection<UserModel>();  // List와 비슷하다고

        //BaseViewModel에서 만든 모델을 view와 연결해주는 viewModel인곳.

        //변수
        private string _userID;
        private string _userName;
        private string _email;
        private string _telephone;
        private DateTime? _registDate;

        //Property`
        public string UserID { get => this._userID; set => SetProperty(ref this._userID, value); }
        public string UserName { get => this._userName; set => SetProperty(ref this._userName, value); }
        public string Email { get => this._email; set => SetProperty(ref this._email, value); }
        public string Telephone { get => this._telephone; set => SetProperty(ref this._telephone, value); }
        public DateTime? RegistDate { get => this._registDate; set => SetProperty(ref this._registDate, value); }
        public ObservableRangeCollection<UserModel> Users { get => this._users; set => SetProperty(ref this._users, value); }

        //Command
        public ICommand RegistCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand SelectUserCommand { get; }

        public MainPageViewModel()
        {
            this.UserID = "hkkim";
            this.UserName = "카리나";
            this.Email = "jonye@smenter.co.kr";
            this.Telephone = "010-2452-8392";
            this.RegistDate = DateTime.Now;

            RegistCommand = new Command(() => Regist(), () => IsControlEnable);
            ModifyCommand = new Command(() => Modify(), () => IsControlEnable);
            DeleteCommand = new Command(() => Delete(), () => IsControlEnable);
            ResetCommand = new Command(() => Reset(), () => IsControlEnable);
            SelectUserCommand = new Command<Model.UserModel>((obj) => SelectUser(obj), (obj) => IsControlEnable);
        }
        
        private void Regist()
        { //async
            IsControlEnable = false;
            IsBusy = true;
            (RegistCommand as Command).ChangeCanExecute(); // 체인지를 해줘야 값이 변경된다.

            //엔티티 그거랑 똑같
            UserModel model = new UserModel()
            {
                UserID = this.UserID,
                UserName = this.UserName,
                Email = this.Email,
                Telephone = this.Telephone,
                RegistDate = DateTime.Now
            };

            Users.Add(model);

            IsControlEnable = true;
            IsBusy = false;
            (RegistCommand as Command).ChangeCanExecute();
        }

        private void Modify()
        {
            IsControlEnable = false;
            IsBusy = true;
            (ModifyCommand as Command).ChangeCanExecute();

            var user = Users.Where(i => i.UserID == this.UserID).FirstOrDefault(); // 같은 것 중 맨 위 처음거 가져옴.

            if(user != null)
            {
                user.UserName = this.UserName;
                user.Email = this.Email;
                user.Telephone = this.Telephone;
                user.RegistDate = this.RegistDate;
            }

            IsControlEnable = true;
            IsBusy = false;
            (ModifyCommand as Command).ChangeCanExecute();
        }

        private void Delete()
        {
            IsControlEnable = false;
            IsBusy = true;
            (DeleteCommand as Command).ChangeCanExecute();

            var user = Users.Where(i => i.UserID != this.UserID).FirstOrDefault();

            if(user != null)
            {
                Users.Remove(user);
            }

            IsControlEnable = true;
            IsBusy = false;
            (DeleteCommand as Command).ChangeCanExecute();

        }

        private void Reset()
        {
            IsControlEnable = false;
            IsBusy = true;
            (ResetCommand as Command).ChangeCanExecute();

            this.UserName = string.Empty;
            this.Email = string.Empty;
            this.Telephone = string.Empty;
            this.UserID = string.Empty;
            this.RegistDate = null;

            IsControlEnable = true;
            IsBusy = false;
            (RegistCommand as Command).ChangeCanExecute();  

        }

        private void SelectUser(Model.UserModel user)
        {
            //연속클릭 막아줌.
            IsControlEnable = false;
            IsBusy = true;
            (SelectUserCommand as Command).ChangeCanExecute();

            if( user != null )
            {
                this.UserID = user.UserID;
                this.UserName = user.UserName;
                this.Email = user.Email;    
                this.Telephone = user.Telephone;
                this.RegistDate = user.RegistDate;
            }

            IsControlEnable = true;
            IsBusy = false;
            (SelectUserCommand as Command).ChangeCanExecute();
        }


    }
}
