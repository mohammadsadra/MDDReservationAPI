namespace MDDReservationAPI.Services;

    public interface IMailService
    {
        void Email(string subject, string htmlString);
    }
