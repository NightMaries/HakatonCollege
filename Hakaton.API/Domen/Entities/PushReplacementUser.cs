using Hakaton.API.Domen.Entities;

public class PushReplacementUser
{
    public int Id {get; set;}
    public int UserId{get;set;}
    public User user {get; set;}
    public int ReplacementId {get; set;}
    public Replacement Replacement {get; set;}

}