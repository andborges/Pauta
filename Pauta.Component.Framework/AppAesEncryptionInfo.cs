using MobileItFramework.Encryption;

namespace Pauta.Component.Framework
{
    public class AppAesEncryptionInfo : AesEncryptionInfo
    {
        public override string Password
        {
            get { return "ZskU37awLVsxgMqyS"; }
        }

        public override string Salt
        {
            get { return "PautaComponentFramework"; }
        }

        public override int PasswordIterations
        {
            get { return 2; }
        }

        public override string InitialVector
        {
            get { return "QSLbm97h@mls87wI"; }
        }

        public override int KeySize
        {
            get { return 256; }
        }
    }
}