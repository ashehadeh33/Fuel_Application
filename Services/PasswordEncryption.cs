namespace qenergy.Services
{
    public class PasswordEncryption
    {
        public static string EncryptPasswordBase64(string pass)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(pass); 
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string DecryptPasswordBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
