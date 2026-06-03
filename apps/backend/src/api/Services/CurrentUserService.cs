using System.Security.Claims;

namespace api.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor)
{
    private ClaimsPrincipal User =>
        httpContextAccessor.HttpContext?.User
        ?? throw new UnauthorizedAccessException("User not authenticated.");

    public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;

    public string FirebaseUid =>
        User.FindFirst("user_id")?.Value
        ?? throw new UnauthorizedAccessException("Firebase uid not found.");

    public string Email =>
        User.FindFirst(ClaimTypes.Email)?.Value
        ?? throw new UnauthorizedAccessException("User email not found.");

    public bool EmailVerified =>
        bool.TryParse(User.FindFirst("email_verified")?.Value, out var verified) && verified;
}
