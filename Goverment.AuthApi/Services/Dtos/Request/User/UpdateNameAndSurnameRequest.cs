﻿using System.Net.Sockets;

namespace Goverment.AuthApi.Business.Dtos.Request.User
{

    public class UpdateNameAndSurnameRequest
    {

        private string _fullname = string.Empty;
        public string FullName { get { return _fullname; } set { _fullname = value.Trim(); } }
    }
}
