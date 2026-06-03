using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class UsersService(AppDbContext context, CurrentUserService currentUserService)
{
    public async Task<User> GetOrCreateAsync()
    {
        var firebaseUid = currentUserService.FirebaseUid;

        var user = await context.Users.FirstOrDefaultAsync(user => user.FirebaseUid == firebaseUid);

        if (user is not null)
        {
            var updated = false;

            if (user.Email != currentUserService.Email)
            {
                user.Email = currentUserService.Email;
                updated = true;
            }

            if (user.EmailVerified != currentUserService.EmailVerified)
            {
                user.EmailVerified = currentUserService.EmailVerified;
                updated = true;
            }

            if (updated)
            {
                user.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();
            }

            return user;
        }

        user = new User
        {
            FirebaseUid = currentUserService.FirebaseUid,
            Email = currentUserService.Email,
            EmailVerified = currentUserService.EmailVerified,
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow,
        };

        context.Users.Add(user);

        await context.SaveChangesAsync();

        return user;
    }
}
