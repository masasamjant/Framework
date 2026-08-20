namespace Masasamjant.Security
{
    public abstract class CertificateUnitTest : UnitTest
    {
        private const string CertificatesFile = "Certificates.txt";
        private static string? testCertificatesFolder;
        private static string? testCertificateFile;
        private static string? testExpiredCertificateFile;
        private static string? testInvalidCertificateFile;
        private static string? testNotFoundCertificateFile;
        private static string? thumbprint;
        private static string? subjectName;
        private static string? expiredThumbprint;
        private static string? expiredSubjectName;

        protected static string GetThumbprint()
        {
            if (thumbprint == null)
                thumbprint = ReadCertificatesFileValue("Thumbprint");
            return thumbprint;
        }

        protected static string GetSubjectName()
        {
            if (subjectName == null)
                subjectName = ReadCertificatesFileValue("Subject");
            return subjectName;
        }

        protected static string GetExpiredThumbprint()
        {
            if (expiredThumbprint == null)
                expiredThumbprint = ReadCertificatesFileValue("ThumbprintExpired");
            return expiredThumbprint;
        }

        protected static string GetExpiredSubjectName()
        {
            if (expiredSubjectName == null)
                expiredSubjectName = ReadCertificatesFileValue("SubjectExpired");
            return expiredSubjectName;
        }

        private static string GetTestCertificatesFolder()
        {
            if (testCertificatesFolder == null)
                testCertificatesFolder = ReadCertificatesFileValue("Folder");

            return testCertificatesFolder;
        }

        protected static string GetTestCertificateFilePath()
        {
            if (testCertificateFile == null)
                testCertificateFile = Path.Combine(GetTestCertificatesFolder(), ReadCertificatesFileValue("Valid"));

            return testCertificateFile;
        }

        protected static string GetExpiredTestCertificateFilePath()
        {
            if (testExpiredCertificateFile == null)
                testExpiredCertificateFile = Path.Combine(GetTestCertificatesFolder(), ReadCertificatesFileValue("Expired"));

            return testExpiredCertificateFile;
        }

        protected static string GetInvalidTestCertificateFilePath()
        {
            if (testInvalidCertificateFile == null)
                testInvalidCertificateFile = Path.Combine(GetTestCertificatesFolder(), ReadCertificatesFileValue("Invalid"));
            return testInvalidCertificateFile;
        }

        protected static string GetNotFoundTestCertificateFilePath()
        {
            if (testNotFoundCertificateFile == null)
                testNotFoundCertificateFile = Path.Combine(GetTestCertificatesFolder(), ReadCertificatesFileValue("NotFound"));
            return testNotFoundCertificateFile;
        }

        private static string ReadCertificatesFileValue(string label)
        {
            using (var reader = File.OpenText(CertificatesFile))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(label))
                        return line.Split('=').Last();
                }
            }

            return string.Empty;
        }
    }
}
