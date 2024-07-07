namespace BlogApp.Payloads.Requests;

public record UserLoginRequest(string email, string password);