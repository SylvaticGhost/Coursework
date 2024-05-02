using GlobalModels.Messages.CompanyResponse;
using MongoDB.Entities;

namespace MessageSvc.Models;

public class UserMessageBox : Entity
{
    public Guid UserId { get; set; }
    public List<AnswerOnApplication> CompanyResponses { get; set; } 
    
    public UserMessageBox (Guid userId)
    {
        UserId = userId;
        CompanyResponses = new List<AnswerOnApplication>();
    }
}