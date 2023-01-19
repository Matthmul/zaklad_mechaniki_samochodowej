namespace ZakladMechanikiSamochodowej.Database.DatabaseModels
{
    public partial class Users
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; } = null;
        public string? EmialAddress { get; set; } = null;
        public bool IsAdmin { get; set; } = false;
    }
}
