using CommandLine;

namespace ScottBrady.Branca.Tool
{
    [Verb("encrypt", HelpText = "Create a Branca token.")]
    public class EncryptOptions : Options
    {
        
        [Option('t', "timestamp", HelpText = "When your token was created (unsigned big endian 4 byte UNIX timestamp). Defaults to current time.")]
        public uint? Timestamp { get; set; }
        
        [Option('p', "payload", HelpText = "The contents of the token you want to create.")]
        public string Payload { get; set; }
    }

    [Verb("decrypt", HelpText = "Decrypt a Branca token.")]
    public class DecryptOptions : Options
    {
        [Option('t', "token", HelpText = "The token you want to decrypt.")]
        public string Token { get; set; }
    }

    public abstract class Options
    {
        [Option('k', "key", HelpText = "Your 32-byte symmetric key. Default format is UTF8.")]
        public string Key { get; set; }
        
        [Option("type", HelpText = "Format of the key: 'utf8', 'base64', or 'base64url'.", Default = "utf8")]
        public string KeyType { get; set; }
    }
}