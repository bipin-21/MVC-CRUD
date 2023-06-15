namespace MVCRUD.Models
{
    public class AddEmployeeViewModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public DateTime DOB { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public string Java { get; set; }
        public string Python { get; set; }
        public string CPlusPlus { get; set; }
        public required string Gender { get; set; }
        public char RecStatus { get; set; } = 'A';
    }
}
