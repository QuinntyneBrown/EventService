namespace EventService.Features.Accounts
{
    public class RegisterAccountApiModel
    {
        public string Name { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }        
    }
}
