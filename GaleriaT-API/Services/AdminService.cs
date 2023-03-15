using AutoMapper;
using GaleriaT_API.Entities;
using GaleriaT_API.Models;
using GaleriaT_API.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace GaleriaT_API.Services
{
    public interface IAdminService
    {
        void Update(AdminPasswordDto dto);
        string GenerateJwt(AdminPasswordDto dto);
    }

    public class AdminService : IAdminService
    {
        private readonly GalleryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GalleryService> _logger;
        private readonly IPasswordHasher<Admin> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AdminService(GalleryDbContext dbContext, IMapper mapper, ILogger<GalleryService>logger, IPasswordHasher<Admin> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }


        public void Update(AdminPasswordDto dto)
        {
            var admin = _dbContext
                 .Admin
                 .FirstOrDefault(g => g.Id == 1);
                             
            if (admin is null) throw new NotFoundException("Admin not found");


            var hashedPassword = _passwordHasher.HashPassword(admin, dto.Password);


            admin.PasswordHash = hashedPassword;



            _dbContext.SaveChanges();


        }
        public string GenerateJwt(AdminPasswordDto dto)
        {
            var admin = _dbContext
                 .Admin
                 .FirstOrDefault(g => g.Id == 1);

            if (admin is null) throw new NotFoundException("Admin not found");

            var result = _passwordHasher.VerifyHashedPassword(admin, admin.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestExcepion("Invalid password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString())
             };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }


    }
}
