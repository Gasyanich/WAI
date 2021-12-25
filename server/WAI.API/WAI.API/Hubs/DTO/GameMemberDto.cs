namespace WAI.API.Hubs.DTO;

public class GameMemberDto
{
    public long UserId { get; set; }

    public string GameWord { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string AvatarUrl { get; set; }
}