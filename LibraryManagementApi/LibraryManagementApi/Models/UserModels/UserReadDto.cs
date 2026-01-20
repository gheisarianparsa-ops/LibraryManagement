using LibraryManagementApi.Models.OrderModels;

namespace LibraryManagementApi.Models.UserModels
{
    public class UserReadDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<OrderInUser> Orders { get; set; }
    }
}
