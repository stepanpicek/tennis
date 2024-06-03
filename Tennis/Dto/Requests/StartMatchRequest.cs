namespace Tennis.Dto.Requests;

public class StartMatchRequest
{
    public string Name { get; set; }
    public int FirstPlayerExperience { get; set; }
    public int SecondPlayerExperience { get; set; }
}