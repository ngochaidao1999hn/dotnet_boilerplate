namespace Infrastructure.Services
{
    public interface IEmailService
    {
        /// <summary>
        ///KHÔNG HOẠT ĐỘNG !!! ĐỪNG DÙNG
        ///ĐỂ ĐÂY CHO ĐẸP
        /// </summary>
        void SendByMailKit(string to, string subject, string html, string from = null);

        /// <summary>
        /// Send email to user by using Sendgrid API
        /// </summary>
        /// <param name="to">Received Email</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="data">Dynamic data for template</param>
        /// <param name="templateId">Template Id</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task SendBySendgrid(string to, string subject, object data, string templateId);
    }
}