using System;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{   
  // AuthRepository Implmentation
  public class AuthRepository : IAuthRepository
  {
    // responsible for querying DB via EF
    // inject data context here
    private readonly DataContext _context;

    public AuthRepository(DataContext context)
    {
      _context = context;
    }

    // login to API
    public async Task<User> Login(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync( 
            // will return username that matches or NULL
            result => result.Username == username
        );

        // if user not found send status
        if (user == null)
            return null; // 401 unauthorized

        // compute hash password generates
        if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            return null; // 401 unauthorized

        // compare hash to what is stored in DB
        return user;
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        // disposes of everything inside once code is executed
        using(
            // Initializes a new instance of the System.Security.Cryptography.HMACSHA512 class with the specified key data.
            // pass in the key to get the computed hash for comparison with what is in the DB

            var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)
        ) {
            // ComputeHash takes in byte array. Must Convert password to byte array using UTF8.GetBytes funct
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            // loop over computedHash and passwordHash and compare
            for (int i = 0; i < computedHash.Length; i++) 
            {
                // if any char doesn't match, incorrect pwd
                if (computedHash[i] != passwordHash[i]) 
                    return false;
            }
        }

        // otherwise password verified
        return true;
    }

    public async Task<User> Register(User user, string password)
    {
       byte[] passwordHash, passwordSalt;

    // use keyword OUT to pass vars by ref not value
       CreatePasswordHash(password, out passwordHash, out passwordSalt);

    // set user password and salt using byte array vars above
       user.PasswordHash = passwordHash;
       user.PasswordSalt = passwordSalt;

    // add to DB. update user object then save changes to DB
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        // disposes of everything inside once code is executed
        using(
            // Initializes a new instance of the System.Security.Cryptography.HMACSHA512 class with a randomly generated key.

            var hmac = new System.Security.Cryptography.HMACSHA512()
        ) {
            passwordSalt = hmac.Key;

            // ComputeHash takes in byte array. Must Convert password to byte array using UTF8.GetBytes funct
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
      

    }

    public async Task<bool> UserExists(string username)
    {
        // found matching username in DB. can't be used
      if (await _context.Users.AnyAsync(result => result.Username == username))
        return true;

        // username doesn't exist and can be used
        return false;
    }
  }
}