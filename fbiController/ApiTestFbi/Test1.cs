using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fbiController;
using fbiController.Controllers;
using fbiController.Models;

namespace FbiControllerTests
{
    [TestClass]
    public class FbiControllerTests
    {
        private WantedDbContext CreateDb()
        {
            var options = new DbContextOptionsBuilder<WantedDbContext>()
                .UseInMemoryDatabase(databaseName: $"wantedDb_{System.Guid.NewGuid()}")
                .Options;

            return new WantedDbContext(options);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOk()
        {
            var db = CreateDb();

            db.WantedPeople.Add(new WantedPerson
            {
                FbiUid = "123",
                Title = "Test person"
            });
            await db.SaveChangesAsync();

            var controller = new FbiController(db, httpClientFactory: null);

            var result = await controller.GetAll(page: 1, pageSize: 10, CancellationToken.None);

            var ok = result as OkObjectResult;
            Assert.IsNotNull(ok, "Result should be OkObjectResult");
            Assert.IsNotNull(ok!.Value, "OkObjectResult.Value should not be null");
        }


        [TestMethod]
        public async Task GetById_ReturnsOk_WhenEntityExists()
        {
            var db = CreateDb();

            var person = new WantedPerson
            {
                FbiUid = "abc",
                Title = "Existing person"
            };
            db.WantedPeople.Add(person);
            await db.SaveChangesAsync();

            var controller = new FbiController(db, httpClientFactory: null);

            var id = person.Id;

            var result = await controller.GetById(id, CancellationToken.None);

            var ok = result as OkObjectResult;
            Assert.IsNotNull(ok, "Result should be OkObjectResult");

            var returned = ok!.Value as WantedPerson;
            Assert.IsNotNull(returned, "Returned value should be a WantedPerson");

            Assert.AreEqual(id, returned!.Id, "Returned person Id should match");
            Assert.AreEqual("Existing person", returned.Title);
        }

        [TestMethod]
        public async Task GetById_ReturnsNotFound_WhenEntityMissing()
        {
            var db = CreateDb();
            var controller = new FbiController(db, httpClientFactory: null);

            var result = await controller.GetById(id: 999, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult),
                "Result should be NotFoundResult when entity does not exist");
        }
    }
}
