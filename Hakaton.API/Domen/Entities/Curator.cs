namespace Hakaton.API.Domen.Entities;
public class Curator {
    public int Id {get; set;}
    public int TeacherId {get; set;}
    public Teacher Teacher {get; set;}
}