﻿namespace Goverment.AuthApi.Business.Dtos.Request
{
	public class UserEmailRequest
	{

        private string _email = string.Empty;
        public string Email { get => _email;
            set => _email = value.Trim().ToLower();
        }

    }
}
