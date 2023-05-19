using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace XamarinMVVMStudy1.Model
{
    public class UserModel : ObservableObject
    {
        public string UserID { get; set; } //변경되지 않음 (pk값으로 잡는다고)

        // 아래 그리드(컬렉션 뷰쪽에 값을 안바꾸는 방법)
        //public string UserName { get; set; }
        //public string Email { get; set; }
        //public string Telephone{ get; set; }
        //public DateTime? RegistDate { get; set; }

        ///////////////////////////////////////////////
        public string _userName;
        public string _email;
        public string _telephone;
        public DateTime? _registDate;

        //SetProperty 버튼 아래 그리드로 뿌려주는?
        public string UserName { get => this._userName; set => SetProperty(ref this._userName, value); }
        public string Email { get => this._email; set => SetProperty(ref this._email, value); }
        public string Telephone { get => this._telephone; set => SetProperty(ref this._telephone, value); }
        public DateTime? RegistDate { get => this._registDate; set => SetProperty(ref this._registDate, value); }





    }
}
