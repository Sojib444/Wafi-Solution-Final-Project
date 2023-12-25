using Kidoo.Learn.Students.Dtos;
using System.Threading.Tasks;

namespace Kidoo.Learn.EmailSenders
{
    public interface IMailJetEmailSender
    {
        Task SendStudentRegistraionEmailAsync(CreateUpdateStudentDto student);
    }
}
