using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
using QutebaApp_Data.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QutebaApp_Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        public UserVM Register(AuthenticateUserVM authenticateUser, string role, string createdAccountWith)
        {
            try
            {
                var roles = unitOfWork.RoleRepository.GetAll();
                int roleId = roles.First(r => r.RoleName == role).Id;

                authenticateUser.Password = Encrypt(authenticateUser.Password);

                User userAccount = new User()
                {
                    Email = authenticateUser.Email,
                    Password = authenticateUser.Password,
                    Fullname = authenticateUser.FullName,
                    RoleId = roleId,
                    CreatedAccountWith = createdAccountWith,
                    UserCreationTime = DateTime.Now
                };


                unitOfWork.UserRepository.Insert(userAccount);

                unitOfWork.Save();

                var savedUser = unitOfWork.UserRepository.FindBy(a => a.Email == userAccount.Email);

                return new UserVM
                {
                    ID = savedUser.Id,
                    FullName = savedUser.Fullname,
                    Email = savedUser.Email,
                    Role = savedUser.Role.RoleName
                };

            }
            catch (Exception e) { throw e; }
        }

        public UserVM Login(AuthenticateUserVM authenticateUser)
        {
            try
            {
                authenticateUser.Password = Encrypt(authenticateUser.Password);

                var queryUser = unitOfWork.UserRepository.FindBy(a => a.Email == authenticateUser.Email && a.Password == authenticateUser.Password);

                queryUser.Role = unitOfWork.RoleRepository.GetById(queryUser.RoleId);

                return new UserVM
                {
                    ID = queryUser.Id,
                    FullName = queryUser.Fullname,
                    Email = queryUser.Email,
                    Role = queryUser.Role.RoleName
                };
            }
            catch (Exception e) { throw e; }
        }

        public IEnumerable<Claim> SetCustomClaims(int id, string role)
        {
            var user = unitOfWork.UserRepository.GetById(id);

            var claims = new List<Claim>
               {
                  new Claim(ClaimTypes.Name, user.Fullname),
                  new Claim(ClaimTypes.Email, user.Email),
                  new Claim(ClaimTypes.Role, role)

               };

            return claims;
        }

        public AuthenticatedUserVM GetToken(UserVM user, IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
             issuer: configuration["Authentication:Jwt:Issuer"],
             audience: configuration["Authentication:Jwt:Audience"],
             claims: claims,
             notBefore: null,
             expires: DateTime.Now.AddMinutes(double.Parse(configuration["Authentication:Jwt:ExpiryInMinutes"])),
             signingCredentials: creds);

            return new AuthenticatedUserVM
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                ID = user.ID,
                FullName = user.FullName,
                Email = user.Email,
                Claims = token.Claims
            };
        }

        public string Encrypt(string stringToEncrypt)
        {
            byte[] bufferPassword = Encoding.ASCII.GetBytes(stringToEncrypt);
            byte[] hashedPassword = HashAlgorithm.Create(configuration["Authentication:Hash"]).ComputeHash(bufferPassword);
            string encryptedPassword = Convert.ToBase64String(hashedPassword);

            return encryptedPassword;
        }

        public bool DoesUserExist(string email)
        {
            var user = unitOfWork.UserRepository.FindBy(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            return true; 
        }
    }
}
