using static System.Runtime.InteropServices.JavaScript.JSType;
using ProfileLib;
using Newtonsoft.Json.Bson;

namespace ProfileTest
{
    [TestClass]
    public sealed class ProfileTest
    {
        private readonly Profile validProfile = new() { ID = 1, Name = "Bertil", Score = 1000 };

        [TestMethod]
        public void ValidProfileTest()
        {
            Assert.AreEqual(1, validProfile.ID);
            Assert.AreEqual("Bertil", validProfile.Name);
            Assert.AreEqual(1000, validProfile.Score);
        }

        [TestMethod]
        public void ProfileNameCanBeSet()
        {
            var profile = new Profile();
            profile.Name = "Anna";
            Assert.AreEqual("Anna", profile.Name);
        }

        [TestMethod]
        public void ProfileScoreCanBeSet()
        {
            var profile = new Profile();
            profile.Score = 500;
            Assert.AreEqual(500, profile.Score);
        }

        [TestMethod]
        public void ProfileIDCanBeSet()
        {
            var profile = new Profile();
            profile.ID = 42;
            Assert.AreEqual(42, profile.ID);
        }

        [TestMethod]
        public void ProfileNameCanBeNull()
        {
            var profile = new Profile();
            Assert.ThrowsException<ArgumentNullException>(() => profile.Name = null);
        }

        [TestMethod]
        public void ProfileNameThreeChar()
        {
            var profile = new Profile();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => profile.Name = "mar");
        }

        [TestMethod]
        public void ProfileScoreDefaultIsZero()
        {
            var profile = new Profile();
            Assert.AreEqual(0, profile.Score);
        }
    }
}
