using MDDReservationAPI.Enums;

namespace MDDReservationAPI.Models;

public class Admin : User
{
    public Admin()
    {
        this.Role = (int) RoleEnum.Admin;;
        this.CreatedAt = DateTime.UtcNow;
    }
}