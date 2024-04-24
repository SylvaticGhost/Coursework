using GlobalModels.Messages.CompanyResponse;
using MongoDB.Entities;

namespace MessageSvc.Models;

public class UserMessageBox : Entity
{
    public Guid UserId { get; private set; }
    public List<AnswerOnApplication> CompanyResponses { get; private set; } 
    
    public UserMessageBox (Guid userId)
    {
        UserId = userId;
        CompanyResponses = new List<AnswerOnApplication>();
    }
}