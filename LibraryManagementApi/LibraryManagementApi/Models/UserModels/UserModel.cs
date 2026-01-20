using LibraryManagementApi.Models.OrderModels;

namespace LibraryManagementApi.Models.UserModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
    }
}
