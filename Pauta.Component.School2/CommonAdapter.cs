using MobileItFramework.ComponentModel;
using MobileItFramework.Encryption;
using MobileItFramework.Security;
using MobileItFramework.WebApi.Security;
using Pauta.Component.Framework;

namespace Pauta.Component.School2
{
    public class CommonAdapter : ICommonAdapter
    {
        public Token Authenticate(Credential credential)
        {
            var decryptedInfo = credential.EncryptedInfo.AesDecrypt(new AppAesEncryptionInfo());
            var credentialParts = decryptedInfo.Split('|');

            var username = credentialParts[0];
            var password = credentialParts[1];

            if (username == "alberto" && password == "1q2w3e4r")
            {
                var sessionManagement = new MemorySessionManagement();
                var token = sessionManagement.New(username, "Alberto da Silveira");

                return token;
            }

            return new Token();
        }
    }
}