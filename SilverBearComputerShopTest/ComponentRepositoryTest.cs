using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SilverBearComputerShop.Data;
using SilverBearComputerShop.Models;
using SilverBearComputerShop.Repositories;
using System.Collections.Generic;

namespace SilverBearComputerShopTest
{
	public class ComponentRepositoryTest
	{

        private ComputerRepository computerRepository;
        public static DbContextOptions<ComputerShopContext> dbContextOptions { get; }
		public static string connectionString = "Server=BINHNQ2022\\BINHNQ8;Database=SilverBearComputerShop1;Trusted_Connection=True;MultipleActiveResultSets=true";
        static ComponentRepositoryTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ComputerShopContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
            
        public void GetComputerRepository()
        {
            var context = new ComputerShopContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);
            computerRepository = new ComputerRepository(context);
        }

        [Test]
        public void GetById()
        {
            //Arrange  
            GetComputerRepository();
            int id = 1;

            //Act  
            var computer = computerRepository.GetById(id).Result;

            //Assert  
            Assert.AreEqual(1, computer.ID);
        }

        [Test]
        public void GetAll()
        {
            //Arrange  
            GetComputerRepository();

            //Act  
            var computer = (List<Computer>)computerRepository.GetAll().Result;

            //Assert  
            Assert.AreEqual(3, computer.Count);
        }
    }
}
