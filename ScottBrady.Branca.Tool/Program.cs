using System;
using CommandLine;
using ScottBrady.IdentityModel.Tokens;

namespace ScottBrady.Branca.Tool
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<EncryptOptions, DecryptOptions>(args)
                .MapResult(
                    (EncryptOptions options) =>
                    {
                        var timestamp = options.Timestamp ?? BrancaToken.GetBrancaTimestamp(DateTimeOffset.UtcNow);

                        var result = new BrancaService().Encrypt(options.Key, options.KeyType, timestamp, options.Payload);
                        if (result.IsSuccess)
                        {
                            Console.WriteLine(result.Value);
                            return 0;
                        }

                        Console.WriteLine(result.Error);
                        return 1;
                    },
                    (DecryptOptions options) =>
                    {
                        var result = new BrancaService().Decrypt(options.Key, options.KeyType, options.Token);
                        if (result.IsSuccess)
                        {
                            Console.WriteLine($"Payload: {result.Value.Payload}");
                            Console.WriteLine($"Timestamp: {result.Value.BrancaFormatTimestamp} ({result.Value.Timestamp})");
                            return 0;
                        }

                        Console.WriteLine(result.Error);
                        return 1;
                    },
                    errors => 1);
        }
    }
}