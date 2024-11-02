﻿using DenemeTakipAPI.Application.Abstraction.Token;
using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Infrastructure.Services
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;

        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        //public Token CreateAccessToken(int day, AppUser user)
        //{
        //    Application.DTOs.Token token = new();
        //    SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        //    SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        //    token.Expiration = DateTime.UtcNow.AddDays(day);
        //    JwtSecurityToken securityToken = new(
        //        audience: _configuration["Token:Audience"],
        //        issuer: _configuration["Token:Issuer"],
        //        expires: token.Expiration,
        //        notBefore: DateTime.UtcNow,
        //        signingCredentials: signingCredentials,
        //        claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) }
        //        );

        //    JwtSecurityTokenHandler tokenHandler = new();
        //    token.AccessToken = tokenHandler.WriteToken(securityToken);
        //    token.RefreshToken=CreateRefreshToken();
        //    return token;

        //}

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }

        public async Task<Token> CreateAccessToken(int day, AppUser user)
        {
            Application.DTOs.Token token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            };
            token.Expiration = DateTime.UtcNow.AddDays(day);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }
    }
}
