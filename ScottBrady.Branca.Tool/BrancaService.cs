using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ScottBrady.IdentityModel.Tokens;

namespace ScottBrady.Branca.Tool
{
    public interface IBrancaService
    {
        Result<string> Encrypt(string key, string keyType, uint timestamp, string payload);
        Result<BrancaToken> Decrypt(string key, string keyType, string token);
    }

    public class BrancaService : IBrancaService
    {
        public Result<string> Encrypt(string key, string keyType, uint timestamp, string payload)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrWhiteSpace(keyType)) throw new ArgumentNullException(nameof(keyType));
            if (string.IsNullOrWhiteSpace(payload)) throw new ArgumentNullException(nameof(payload));

            byte[] keyBytes;
            try
            {
                keyBytes = ParseKey(key, keyType);
            }
            catch
            {
                return Result<string>.Failure("Unable to parse key");
            }

            if (keyBytes.Length != 32) return Result<string>.Failure("Invalid key length");

            var handler = new BrancaTokenHandler();

            try
            {
                return Result<string>.Success(handler.CreateToken(payload, timestamp, keyBytes));
            }
            catch
            {
                return Result<string>.Failure("Unable to create token");
            }
        }

        public Result<BrancaToken> Decrypt(string key, string keyType, string token)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrWhiteSpace(keyType)) throw new ArgumentNullException(nameof(keyType));
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));

            byte[] keyBytes;
            try
            {
                keyBytes = ParseKey(key, keyType);
            }
            catch
            {
                return Result<BrancaToken>.Failure("Unable to parse key");
            }

            if (keyBytes.Length != 32) return Result<BrancaToken>.Failure("Invalid key length");

            var handler = new BrancaTokenHandler();

            try
            {
                return Result<BrancaToken>.Success(handler.DecryptToken(token, keyBytes));
            }
            catch
            {
                return Result<BrancaToken>.Failure("Unable to decrypt token");
            }
        }

        private byte[] ParseKey(string key, string keyType)
        {
            return keyType switch
            {
                "base64" => Convert.FromBase64String(key),
                "utf8" => Encoding.UTF8.GetBytes(key),
                "base64url" => Base64UrlEncoder.DecodeBytes(key),
                _ => throw new NotSupportedException("Invalid key type")
            };
        }
    }
}