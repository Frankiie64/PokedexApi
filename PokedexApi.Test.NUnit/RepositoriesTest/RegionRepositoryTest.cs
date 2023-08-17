using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Pokedex.Core.Application.Interfaces.Repositories;
using Pokedex.Core.Domain.Entities;
using Pokedex.Infrastructure.Persistence.Context;
using Pokedex.Infrastructure.Persistence.Repositories;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexApi.Test.NUnit.RepositoriesTest
{
    public class RegionRepositoryTest
    {
        protected TestServer server;
        public IGenericRepository<Region> _repo { get; set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            this.server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _repo = server.Host.Services.GetRequiredService<IGenericRepository<Region>>();
        }

        [Order(0)]
        [Test]
        public async Task GetAll_ReturnsValidList()
        {
            // Act
            var result = await _repo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<IEnumerable<Region>>(result);
        }

        [Order(1)]
        [Test]
        public async Task GetAll_ReturnsEmptyList()
        {
            // Act
            var result = await _repo.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }

        [Order(2)]
        [Test]
        public async Task GetAll_ReturnsList()
        {
            //arrage
            var response = await _repo.Add(new Region()
            {
                CreateBy = "Jdoe",
                Created = DateTime.Now,
                Description = "Description",
                Id = Guid.NewGuid(),             
                Name = "Prueba",
                
            });

            // Act           
            var result = await _repo.GetAll();

            // Assert
            Assert.True(response);
            Assert.IsTrue(result.Count() >= 0);
        }

        [Order(3)]
        [Test]
        public async Task GetAll_ReturnsElementsOfTypeRegion()
        {

            //arrage

            var responseDelete = await _repo.DeleteAll();

            var response = await _repo.Add(new Region()
            {
                CreateBy = "Jdoe",
                Created = DateTime.Now,
                Description = "Description",
                Id = Guid.NewGuid(),
                Name = "Prueba",

            });

            // Act
            var result = await _repo.GetAll();

            // Assert
            Assert.True(responseDelete);
            Assert.True(response);
            Assert.True(result.All(item => item is Region));
        }

        [Order(4)]
        [Test]
        public async Task GetAll_DoesNotThrowException()
        {
            //arrage

            var responseDelete = await _repo.DeleteAll();

            var response = await _repo.Add(new Region()
            {
                CreateBy = "Jdoe",
                Created = DateTime.Now,
                Description = "Description",
                Id = Guid.NewGuid(),
                Name = "Prueba",

            });

            // Act
            async Task Action() => await _repo.GetAll();

            // Assert
            Assert.True(responseDelete);
            Assert.True(response);
            Assert.DoesNotThrowAsync(Action);
        }

        [Test]
        [Order(5)]
        public async Task GetById_ReturnsValidItem()
        {
            // Arrange

            var responseDelete = await _repo.DeleteAll();

            var addedItem = new Region
            {
                CreateBy = "Jdoe",
                Created = DateTime.Now,
                Description = "Description",
                Id = Guid.NewGuid(),
                Name = "TestRegion",
            };

            await _repo.Add(addedItem);

            // Act

            var result = await _repo.GetById(addedItem.Id);

            // Assert
            Assert.IsTrue(responseDelete);
            Assert.NotNull(result);
            Assert.AreEqual(addedItem.Id, result.Id);
        }

        [Test]
        [Order(6)]
        public async Task GetById_ReturnsNullForNonExistentItem()
        {
            // Arrange: Generar un Id no existente
            var responseDelete = await _repo.DeleteAll();
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await _repo.GetById(nonExistentId);

            // Assert
            Assert.IsTrue(responseDelete);
            Assert.Null(result);
        }

        [Test]
        [Order(7)]
        public async Task GetById_DoesNotThrowException()
        {
            // Arrange

            var responseDelete = await _repo.DeleteAll();

            var addedItem = new Region
            {
                CreateBy = "Jdoe",
                Created = DateTime.Now,
                Description = "Description",
                Id = Guid.NewGuid(),
                Name = "TestRegion",
            };

            // Act

            var response = await _repo.Add(addedItem);
            async Task Action() => await _repo.GetById(addedItem.Id);

            // Assert
            Assert.IsTrue(responseDelete);
            Assert.IsTrue(response);
            Assert.DoesNotThrowAsync(Action);
        }
      
    }
}
