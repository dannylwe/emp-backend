using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using intEmp.Entity;
using Microsoft.IdentityModel.Tokens;

namespace intEmp.Services;

public class AuthService
{
    public static class AuthSettings
    {
    public static string PrivateKey { get; set; } = "strong_key_1";
    }
    public string GenerateToken(Employee user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = credentials,
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(Employee user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));

        return claims;
    }

    public static byte[] HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPassword(string password, byte[] hashedPassword)
    {
        using var hmac = new HMACSHA512();
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hashedPassword);
    }
}