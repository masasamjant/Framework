using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace Masasamjant.Security.Passwords
{
    [TestClass]
    [SupportedOSPlatform("windows")]
    public class WindowsFileSecretStoreUnitTest : UnitTest
    {
        private string _testDirectory = null!;

        [TestInitialize]
        public void Setup()
        {
            _testDirectory = Path.Combine(Path.GetTempPath(), $"WindowsFileSecretStoreTests_{Guid.NewGuid()}");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }

        [TestMethod]
        public void Constructor_WithValidScopeAndDefaultDirectory_ShouldCreateInstance()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser);

            Assert.IsNotNull(store);
        }

        [TestMethod]
        public void Constructor_WithValidScopeAndCustomDirectory_ShouldCreateInstance()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            Assert.IsNotNull(store);
        }

        [TestMethod]
        public void Constructor_WithLocalMachineScope_ShouldCreateInstance()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.LocalMachine, _testDirectory);

            Assert.IsNotNull(store);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithUndefinedScope_ShouldThrowArgumentException()
        {
            _ = new WindowsFileSecretStore((DataProtectionScope)999, _testDirectory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullDirectory_ShouldThrowArgumentNullException()
        {
            _ = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, null!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithEmptyDirectory_ShouldThrowArgumentNullException()
        {
            _ = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithWhitespaceDirectory_ShouldThrowArgumentNullException()
        {
            _ = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, "   ");
        }

        [TestMethod]
        public async Task GetSecretAsync_WhenSecretExists_ShouldReturnDecryptedSecret()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var expectedSecret = "MySecretPassword123";

            await store.StoreSecretAsync(application, environment, expectedSecret, false);

            var result = await store.GetSecretAsync(application, environment);

            Assert.AreEqual(expectedSecret, result);
        }

        [TestMethod]
        public async Task GetSecretAsync_WhenSecretDoesNotExist_ShouldReturnNull()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            var result = await store.GetSecretAsync("NonExistent", PasswordEnvironment.Development);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetSecretAsync_WithDifferentEnvironments_ShouldReturnCorrectSecret()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var devSecret = "DevSecret123";
            var prodSecret = "ProdSecret456";

            await store.StoreSecretAsync(application, PasswordEnvironment.Development, devSecret, false);
            await store.StoreSecretAsync(application, PasswordEnvironment.Production, prodSecret, false);

            var devResult = await store.GetSecretAsync(application, PasswordEnvironment.Development);
            var prodResult = await store.GetSecretAsync(application, PasswordEnvironment.Production);

            Assert.AreEqual(devSecret, devResult);
            Assert.AreEqual(prodSecret, prodResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetSecretAsync_WithNullApplication_ShouldThrowArgumentNullException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.GetSecretAsync(null!, PasswordEnvironment.Development);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetSecretAsync_WithEmptyApplication_ShouldThrowArgumentNullException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.GetSecretAsync(string.Empty, PasswordEnvironment.Development);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetSecretAsync_WithWhitespaceApplication_ShouldThrowArgumentNullException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.GetSecretAsync("   ", PasswordEnvironment.Development);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetSecretAsync_WithUndefinedEnvironment_ShouldThrowArgumentException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.GetSecretAsync("TestApp", (PasswordEnvironment)999);
        }

        [TestMethod]
        public async Task GetSecretAsync_WithCorruptedFile_ShouldThrowInvalidOperationException()
        {
            Directory.CreateDirectory(_testDirectory);
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;

            var secretFilePath = Path.Combine(_testDirectory, $"{application.ToUpperInvariant()}-{environment.ToString().ToUpperInvariant()}-SCRT.sec");
            await File.WriteAllTextAsync(secretFilePath, "corrupted data");

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
                await store.GetSecretAsync(application, environment));
        }

        [TestMethod]
        public async Task StoreSecretAsync_WithValidSecret_ShouldCreateEncryptedFile()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var secret = "MySecret123";

            await store.StoreSecretAsync(application, environment, secret, false);

            var secretFilePath = Path.Combine(_testDirectory, $"{application.ToUpperInvariant()}-{environment.ToString().ToUpperInvariant()}-SCRT.sec");
            Assert.IsTrue(File.Exists(secretFilePath));
        }

        [TestMethod]
        public async Task StoreSecretAsync_WithValidSecret_ShouldEncryptContent()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var secret = "MySecret123";

            await store.StoreSecretAsync(application, environment, secret, false);

            var secretFilePath = Path.Combine(_testDirectory, $"{application.ToUpperInvariant()}-{environment.ToString().ToUpperInvariant()}-SCRT.sec");
            var fileContent = await File.ReadAllTextAsync(secretFilePath);
            
            Assert.IsFalse(fileContent.Contains(secret));
        }

        [TestMethod]
        public async Task StoreSecretAsync_ThenGetSecret_ShouldReturnOriginalSecret()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var expectedSecret = "MySecretPassword123!@#$%^&*()";

            await store.StoreSecretAsync(application, environment, expectedSecret, false);
            var result = await store.GetSecretAsync(application, environment);

            Assert.AreEqual(expectedSecret, result);
        }

        [TestMethod]
        public async Task StoreSecretAsync_WithLocalMachineScope_ShouldStoreAndRetrieve()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.LocalMachine, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var expectedSecret = "LocalMachineSecret123";

            await store.StoreSecretAsync(application, environment, expectedSecret, false);
            var result = await store.GetSecretAsync(application, environment);

            Assert.AreEqual(expectedSecret, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task StoreSecretAsync_WithNullSecret_ShouldThrowArgumentException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.StoreSecretAsync("TestApp", PasswordEnvironment.Development, null!, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task StoreSecretAsync_WithEmptySecret_ShouldThrowArgumentException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.StoreSecretAsync("TestApp", PasswordEnvironment.Development, string.Empty, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task StoreSecretAsync_WithWhitespaceSecret_ShouldThrowArgumentException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.StoreSecretAsync("TestApp", PasswordEnvironment.Development, "   ", false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task StoreSecretAsync_WithNullApplication_ShouldThrowArgumentNullException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.StoreSecretAsync(null!, PasswordEnvironment.Development, "secret", false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task StoreSecretAsync_WithEmptyApplication_ShouldThrowArgumentNullException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.StoreSecretAsync(string.Empty, PasswordEnvironment.Development, "secret", false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task StoreSecretAsync_WithWhitespaceApplication_ShouldThrowArgumentNullException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.StoreSecretAsync("   ", PasswordEnvironment.Development, "secret", false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task StoreSecretAsync_WithUndefinedEnvironment_ShouldThrowArgumentException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.StoreSecretAsync("TestApp", (PasswordEnvironment)999, "secret", false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task StoreSecretAsync_WhenFileExistsAndOverwriteIsFalse_ShouldThrowInvalidOperationException()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;

            await store.StoreSecretAsync(application, environment, "FirstSecret", false);
            await store.StoreSecretAsync(application, environment, "SecondSecret", false);
        }

        [TestMethod]
        public async Task StoreSecretAsync_WhenFileExistsAndOverwriteIsTrue_ShouldReplaceSecret()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var firstSecret = "FirstSecret";
            var secondSecret = "SecondSecret";

            await store.StoreSecretAsync(application, environment, firstSecret, false);
            await store.StoreSecretAsync(application, environment, secondSecret, true);

            var result = await store.GetSecretAsync(application, environment);
            Assert.AreEqual(secondSecret, result);
        }

        [TestMethod]
        public async Task StoreSecretAsync_WithMultipleApplications_ShouldStoreIndependently()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var app1 = "App1";
            var app2 = "App2";
            var environment = PasswordEnvironment.Development;
            var secret1 = "Secret1";
            var secret2 = "Secret2";

            await store.StoreSecretAsync(app1, environment, secret1, false);
            await store.StoreSecretAsync(app2, environment, secret2, false);

            var result1 = await store.GetSecretAsync(app1, environment);
            var result2 = await store.GetSecretAsync(app2, environment);

            Assert.AreEqual(secret1, result1);
            Assert.AreEqual(secret2, result2);
        }

        [TestMethod]
        public async Task StoreSecretAsync_WithSpecialCharacters_ShouldStoreAndRetrieveCorrectly()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var secretWithSpecialChars = "P@ssw0rd!#$%^&*()_+-=[]{}|;':\",./<>?`~";

            await store.StoreSecretAsync(application, environment, secretWithSpecialChars, false);
            var result = await store.GetSecretAsync(application, environment);

            Assert.AreEqual(secretWithSpecialChars, result);
        }

        [TestMethod]
        public async Task StoreSecretAsync_WithUnicodeCharacters_ShouldStoreAndRetrieveCorrectly()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var secretWithUnicode = "Pässwörd_日本語_中文_Русский_🔐🔑";

            await store.StoreSecretAsync(application, environment, secretWithUnicode, false);
            var result = await store.GetSecretAsync(application, environment);

            Assert.AreEqual(secretWithUnicode, result);
        }

        [TestMethod]
        public async Task StoreSecretAsync_WithLongSecret_ShouldStoreAndRetrieveCorrectly()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var longSecret = new string('A', 10000);

            await store.StoreSecretAsync(application, environment, longSecret, false);
            var result = await store.GetSecretAsync(application, environment);

            Assert.AreEqual(longSecret, result);
        }

        [TestMethod]
        public async Task Integration_MultipleSecretsInSameDirectory_ShouldWorkIndependently()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);

            await store.StoreSecretAsync("App1", PasswordEnvironment.Development, "Dev1Secret", false);
            await store.StoreSecretAsync("App1", PasswordEnvironment.Production, "Prod1Secret", false);
            await store.StoreSecretAsync("App2", PasswordEnvironment.Development, "Dev2Secret", false);
            await store.StoreSecretAsync("App2", PasswordEnvironment.Production, "Prod2Secret", false);

            Assert.AreEqual("Dev1Secret", await store.GetSecretAsync("App1", PasswordEnvironment.Development));
            Assert.AreEqual("Prod1Secret", await store.GetSecretAsync("App1", PasswordEnvironment.Production));
            Assert.AreEqual("Dev2Secret", await store.GetSecretAsync("App2", PasswordEnvironment.Development));
            Assert.AreEqual("Prod2Secret", await store.GetSecretAsync("App2", PasswordEnvironment.Production));
        }

        [TestMethod]
        public async Task Integration_StoreRetrieveUpdateRetrieve_ShouldWorkCorrectly()
        {
            var store = new WindowsFileSecretStore(DataProtectionScope.CurrentUser, _testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;

            await store.StoreSecretAsync(application, environment, "InitialSecret", false);
            var initial = await store.GetSecretAsync(application, environment);
            Assert.AreEqual("InitialSecret", initial);

            await store.StoreSecretAsync(application, environment, "UpdatedSecret", true);
            var updated = await store.GetSecretAsync(application, environment);
            Assert.AreEqual("UpdatedSecret", updated);
        }
    }
}
