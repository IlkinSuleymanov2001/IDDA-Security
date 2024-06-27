namespace Goverment.AuthApi.Services.Constants;

    public static  class Messages
    {
        public const string EmailIsUnique = "Bu e - poçt ünvanı üzrə sistemdə istifadəçi mövcuddur";
        public const string InvalidOtp = "Kod yanlışdır";
        public const string UserNotExists = "Bu e-poçt ünvanı mövcud deyil";
        public const string ResendOtpError = "Həddən artıq uğursuz cəhd, bir müddət sonra yenidən yoxlayın";
        public const string UserNameAndPasswordError = "E-poçt və ya şifrə yanlışdır";
        public const string PassordFromatValidationError = "Şifrədə ən azı bir böyük və kiçik latın hərfi, rəqəm və xüsusi simvol istifadə olunmalıdır.";
        public const string PasswordLengthvalidationError = "Şifrə ən azı 8 simvoldan ibarət olmalıdır"  ;
        public const string  IDTokenExpired = "Sessiyanizin vaxdi bitdi..";
}
