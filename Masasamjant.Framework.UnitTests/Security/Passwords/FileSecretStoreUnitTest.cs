namespace Masasamjant.Security.Passwords
{
    [TestClass]
    public class FileSecretStoreUnitTest : UnitTest
    {
        private string _testDirectory = null!;

        [TestInitialize]
        public void Setup()
        {
            _testDirectory = Path.Combine(Path.GetTempPath(), $"FileSecretStoreTests_{Guid.NewGuid()}");
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
        public void Constructor_WithValidDirectory_ShouldSetStoreDirectory()
        {
            var store = new TestFileSecretStore(_testDirectory);

            Assert.AreEqual(_testDirectory, store.GetStoreDirectory());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullDirectory_ShouldThrowArgumentNullException()
        {
            _ = new TestFileSecretStore(null!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithEmptyDirectory_ShouldThrowArgumentNullException()
        {
            _ = new TestFileSecretStore(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithWhitespaceDirectory_ShouldThrowArgumentNullException()
        {
            _ = new TestFileSecretStore("   ");
        }

        [TestMethod]
        public void ValidateApplicationEnvironment_WithValidArguments_ShouldNotThrow()
        {
            var store = new TestFileSecretStore(_testDirectory);

            store.CallValidateApplicationEnvironment("TestApp", PasswordEnvironment.Development);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateApplicationEnvironment_WithNullApplication_ShouldThrowArgumentNullException()
        {
            var store = new TestFileSecretStore(_testDirectory);

            store.CallValidateApplicationEnvironment(null!, PasswordEnvironment.Development);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateApplicationEnvironment_WithEmptyApplication_ShouldThrowArgumentNullException()
        {
            var store = new TestFileSecretStore(_testDirectory);

            store.CallValidateApplicationEnvironment(string.Empty, PasswordEnvironment.Development);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateApplicationEnvironment_WithWhitespaceApplication_ShouldThrowArgumentNullException()
        {
            var store = new TestFileSecretStore(_testDirectory);

            store.CallValidateApplicationEnvironment("   ", PasswordEnvironment.Development);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateApplicationEnvironment_WithUndefinedEnvironment_ShouldThrowArgumentException()
        {
            var store = new TestFileSecretStore(_testDirectory);

            store.CallValidateApplicationEnvironment("TestApp", (PasswordEnvironment)999);
        }

        [TestMethod]
        public void GetSecretFilePath_WithDevelopmentEnvironment_ShouldReturnCorrectPath()
        {
            var store = new TestFileSecretStore(_testDirectory);
            var application = "MyApp";
            var environment = PasswordEnvironment.Development;

            var result = store.CallGetSecretFilePath(application, environment);

            var expectedFileName = "MYAPP-DEVELOPMENT-SCRT.sec";
            Assert.AreEqual(Path.Combine(_testDirectory, expectedFileName), result);
        }

        [TestMethod]
        public void GetSecretFilePath_WithProductionEnvironment_ShouldReturnCorrectPath()
        {
            var store = new TestFileSecretStore(_testDirectory);
            var application = "MyApp";
            var environment = PasswordEnvironment.Production;

            var result = store.CallGetSecretFilePath(application, environment);

            var expectedFileName = "MYAPP-PRODUCTION-SCRT.sec";
            Assert.AreEqual(Path.Combine(_testDirectory, expectedFileName), result);
        }

        [TestMethod]
        public void GetSecretFilePath_WithMixedCaseApplication_ShouldReturnUppercasePath()
        {
            var store = new TestFileSecretStore(_testDirectory);
            var application = "MyTestApp";
            var environment = PasswordEnvironment.Development;

            var result = store.CallGetSecretFilePath(application, environment);

            Assert.IsTrue(result.Contains("MYTESTAPP-DEVELOPMENT-SCRT.sec"));
        }

        [TestMethod]
        public void EnsureStoreDirectoryExist_WhenDirectoryDoesNotExist_ShouldCreateDirectory()
        {
            var store = new TestFileSecretStore(_testDirectory);

            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }

            store.CallEnsureStoreDirectoryExist();

            Assert.IsTrue(Directory.Exists(_testDirectory));
        }

        [TestMethod]
        public void EnsureStoreDirectoryExist_WhenDirectoryExists_ShouldNotThrow()
        {
            Directory.CreateDirectory(_testDirectory);
            var store = new TestFileSecretStore(_testDirectory);

            store.CallEnsureStoreDirectoryExist();
            Assert.IsTrue(Directory.Exists(_testDirectory));
        }

        [TestMethod]
        public void CheckStoreFile_WhenFileDoesNotExist_ShouldNotThrow()
        {
            var nonExistentFile = Path.Combine(_testDirectory, "nonexistent.sec");

            TestFileSecretStore.CallCheckStoreFile(nonExistentFile, false, "TestApp", PasswordEnvironment.Development);
        }

        [TestMethod]
        public void CheckStoreFile_WhenFileExistsAndOverwriteIsTrue_ShouldDeleteFile()
        {
            Directory.CreateDirectory(_testDirectory);
            var testFile = Path.Combine(_testDirectory, "test.sec");
            File.WriteAllText(testFile, "test content");

            TestFileSecretStore.CallCheckStoreFile(testFile, true, "TestApp", PasswordEnvironment.Development);

            Assert.IsFalse(File.Exists(testFile));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CheckStoreFile_WhenFileExistsAndOverwriteIsFalse_ShouldThrowInvalidOperationException()
        {
            Directory.CreateDirectory(_testDirectory);
            var testFile = Path.Combine(_testDirectory, "test.sec");
            File.WriteAllText(testFile, "test content");

            TestFileSecretStore.CallCheckStoreFile(testFile, false, "TestApp", PasswordEnvironment.Development);
        }

        [TestMethod]
        public void CheckStoreFile_ExceptionMessage_ShouldContainApplicationAndEnvironment()
        {
            Directory.CreateDirectory(_testDirectory);
            var testFile = Path.Combine(_testDirectory, "test.sec");
            File.WriteAllText(testFile, "test content");
            var application = "MyTestApp";
            var environment = PasswordEnvironment.Production;

            var exception = Assert.ThrowsException<InvalidOperationException>(() =>
                TestFileSecretStore.CallCheckStoreFile(testFile, false, application, environment));

            Assert.IsTrue(exception.Message.Contains(application));
            Assert.IsTrue(exception.Message.Contains(environment.ToString()));
        }

        [TestMethod]
        public async Task GetSecretAsync_WhenSecretExists_ShouldReturnSecret()
        {
            var store = new TestFileSecretStore(_testDirectory);
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
            var store = new TestFileSecretStore(_testDirectory);

            var result = await store.GetSecretAsync("NonExistent", PasswordEnvironment.Development);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetSecretAsync_WithDifferentEnvironments_ShouldReturnCorrectSecret()
        {
            var store = new TestFileSecretStore(_testDirectory);
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
        public async Task StoreSecretAsync_WithNullSecret_ShouldThrowArgumentNullException()
        {
            var store = new TestFileSecretStore(_testDirectory);

            await store.StoreSecretAsync("TestApp", PasswordEnvironment.Development, null!, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task StoreSecretAsync_WithEmptySecret_ShouldThrowArgumentNullException()
        {
            var store = new TestFileSecretStore(_testDirectory);

            await store.StoreSecretAsync("TestApp", PasswordEnvironment.Development, string.Empty, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task StoreSecretAsync_WithWhitespaceSecret_ShouldThrowArgumentNullException()
        {
            var store = new TestFileSecretStore(_testDirectory);

            await store.StoreSecretAsync("TestApp", PasswordEnvironment.Development, "   ", false);
        }

        [TestMethod]
        public async Task StoreSecretAsync_WithValidSecret_ShouldCreateFile()
        {
            var store = new TestFileSecretStore(_testDirectory);
            var secret = "MySecret123";

            await store.StoreSecretAsync("TestApp", PasswordEnvironment.Development, secret, false);

            var filePath = store.CallGetSecretFilePath("TestApp", PasswordEnvironment.Development);
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public async Task StoreSecretAsync_WithValidSecret_ShouldStoreCorrectContent()
        {
            var store = new TestFileSecretStore(_testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;
            var secret = "MySecret123";

            await store.StoreSecretAsync(application, environment, secret, false);

            var filePath = store.CallGetSecretFilePath(application, environment);
            var fileContent = File.ReadAllText(filePath);
            Assert.AreEqual(secret, fileContent);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task StoreSecretAsync_WhenFileExistsAndOverwriteIsFalse_ShouldThrowInvalidOperationException()
        {
            var store = new TestFileSecretStore(_testDirectory);
            var application = "TestApp";
            var environment = PasswordEnvironment.Development;

            await store.StoreSecretAsync(application, environment, "FirstSecret", false);
            await store.StoreSecretAsync(application, environment, "SecondSecret", false);
        }

        [TestMethod]
        public async Task StoreSecretAsync_WhenFileExistsAndOverwriteIsTrue_ShouldReplaceSecret()
        {
            var store = new TestFileSecretStore(_testDirectory);
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
            var store = new TestFileSecretStore(_testDirectory);
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

        private class TestFileSecretStore : FileSecretStore
        {
            public TestFileSecretStore(string storeDirectory) : base(storeDirectory)
            {
            }

            public override Task<string?> GetSecretAsync(string application, PasswordEnvironment environment)
            {
                ValidateApplicationEnvironment(application, environment);
                var filePath = GetSecretFilePath(application, environment);

                if (File.Exists(filePath))
                {
                    return Task.FromResult<string?>(File.ReadAllText(filePath));
                }

                return Task.FromResult<string?>(null);
            }

            public override Task StoreSecretAsync(string application, PasswordEnvironment environment, string secret, bool overwrite)
            {
                if (string.IsNullOrWhiteSpace(secret))
                    throw new ArgumentNullException(nameof(secret), "The value cannot be null, empty or whitespace.");

                ValidateApplicationEnvironment(application, environment);
                EnsureStoreDirectoryExist();

                var filePath = GetSecretFilePath(application, environment);
                CheckStoreFile(filePath, overwrite, application, environment);

                File.WriteAllText(filePath, secret);
                return Task.CompletedTask;
            }

            public string GetStoreDirectory() => StoreDirectory;
            public void CallValidateApplicationEnvironment(string application, PasswordEnvironment environment)
                => ValidateApplicationEnvironment(application, environment);
            public string CallGetSecretFilePath(string application, PasswordEnvironment environment)
                => GetSecretFilePath(application, environment);
            public void CallEnsureStoreDirectoryExist()
                => EnsureStoreDirectoryExist();
            public static void CallCheckStoreFile(string secretFilePath, bool overwrite, string application, PasswordEnvironment environment)
                => CheckStoreFile(secretFilePath, overwrite, application, environment);
        }
    }
}