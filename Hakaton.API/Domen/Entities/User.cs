namespace Hakaton.API.Domen.Entities;

public class User
{
    public int Id {get; set;}
    public string Login {get; set;}
    public required string PasswordHash {get; set;}
    public string Token {get; set;}
    public bool Subscription {get; set;}
    public int RoleId {get; set;}
    public Role Role {get; set;}
}