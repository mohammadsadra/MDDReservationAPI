namespace MDDReservationAPI.DTO
{
    public class AdminCreationDTO
    {
        public string Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? NationalId { get; set; }

        public bool? IsVerify { get; set; }
    }
}
