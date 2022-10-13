using JWTToken.Authentication.Configurations;
using JWTToken.Authentication.Models;
using JWTToken.Authentication.Models.Enums;
using JWTToken.Authentication.Models.Interfaces;
using JWTToken.Authentication.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTToken.Authentication
{
    public class AuthenticateManager
    {
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private IDistributedCache _cache;

        public AuthenticateManager(
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IDistributedCache cache)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _cache = cache;
        }

        public ObjectToken Authenticate(IUserAuthenticateDto userDto, IUserServiceAuthenticate userService)
        {
            bool credenciaisValidas = false;
            if (userDto != null)
            {
                if (userDto.TypeLogin == TypeLogin.Login)
                {
                    var usuarioBase = userService.GetForLogin(userDto);
                    credenciaisValidas = (usuarioBase != null);
                }
                else if (userDto.TypeLogin == TypeLogin.RefreshToken && !String.IsNullOrWhiteSpace(userDto.RefreshToken))
                {
                    RefreshTokenData refreshTokenBase = null;

                    string strTokenArmazenado =
                        _cache.GetString(userDto.RefreshToken);
                    if (!String.IsNullOrWhiteSpace(strTokenArmazenado))
                    {
                        refreshTokenBase = JsonConvert
                            .DeserializeObject<RefreshTokenData>(strTokenArmazenado);
                    }

                    credenciaisValidas = (refreshTokenBase != null &&
                        userDto.Access == refreshTokenBase.Access &&
                        userDto.RefreshToken == refreshTokenBase.RefreshToken);

                    // Elimina o token de refresh já que um novo será gerado
                    if (credenciaisValidas)
                        _cache.Remove(userDto.RefreshToken);
                }
            }

            if (credenciaisValidas)
            {
                return GenerateToken(userService.GetByAcess(userDto.Access));
            }
            else
            {
                return new ObjectToken()
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }

        private ObjectToken GenerateToken(IUserAuthenticate user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim("AccessLogged", user.Access),
                        new Claim("UserProfileLogged", user.UserProfile.GetHashCode().ToString())
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            // Calcula o tempo máximo de validade do refresh token
            // (o mesmo será invalidado automaticamente pelo Redis)
            TimeSpan finalExpiration =
                TimeSpan.FromSeconds(_tokenConfigurations.FinalExpiration);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            var resultado = new ObjectToken()
            {
                authenticated = true,
                created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                refreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
                message = "OK"
            };

            // Armazena o refresh token em cache através do Redis 
            var refreshTokenData = new RefreshTokenData();
            refreshTokenData.RefreshToken = resultado.refreshToken;
            refreshTokenData.Access = user.Access;

            DistributedCacheEntryOptions opcoesCache =
                new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(finalExpiration);
            opcoesCache.AbsoluteExpiration = DateTime.Now.Add(finalExpiration);
            opcoesCache.SlidingExpiration = finalExpiration;
            _cache.SetString(resultado.refreshToken,
                JsonConvert.SerializeObject(refreshTokenData),
                opcoesCache);

            return resultado;
        }
    }
}
