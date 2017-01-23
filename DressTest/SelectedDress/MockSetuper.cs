using System.Collections.Generic;
using System.Linq;
using HoneymoonShop.Controllers;
using HoneymoonShop.Model;
using HoneymoonShop.Model.DressModels;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DressSelectedTest.SelectedDress
{
    public class MockSetuper
    {

        public MockSetuper()
        {
           
        }



        public static DressController SetMockUpAndGetDressController()
        {
            //maak alle mockobjecten aan
            var mockDbContext = new Mock<DBContext>();
            var mockDbSetDress = new Mock<DbSet<Dress>>();
//            var mockDbSetBrand = new Mock<DbSet<Brand>>();
//            var mockDbSetProperty = new Mock<DbSet<Property>>();
//            var mockDbSetDressProperty = new Mock<DbSet<DressProperty>>();
//            var mockDbAppointment = new Mock<DbSet<Appointment>>();


            var dummyData = new List<Dress>()
            {
                new Dress()
                {
                    Name = "Armani",
                    Brand =  new Brand() {Name = "BrandnameDummy", Dresses = null},
                    DressId = 111,
                    BrandName = "ArmaniName",
                    Colors = Color.Ivory,
                    Description = "TestDescription",
                    PriceIndication = 10,
                    Images = new List<Image>()
                    {
                        new Image() {Dress = null, DressId = 111, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>()
                    {
                        new DressProperty()
                        {
                            Dress = null,
                            DressId = 111,
                            PropertyId = 1,
                            Property = new Property()
                            {
                                Name = "propName",
                                PropertyId = 1,
                                Type = Property.PropertyType.Neckline,
                                Dresses = null
                            }
                        }
                    }
                },
                new Dress() {
                    Name = "Gucci",
                    Brand =  new Brand() {Name = "secondDress", Dresses = null},
                    DressId = 222,
                    BrandName = "secondName",
                    Colors = Color.Ivory,
                    Description = "TestDescription2",
                    PriceIndication = 10,
                    Images = new List<Image>()
                    {
                        new Image() {Dress = null, DressId = 111, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>()
                    {
                        new DressProperty()
                        {
                            Dress = null,
                            DressId = 111,
                            PropertyId = 1,
                            Property = new Property()
                            {
                                Name = "propName",
                                PropertyId = 1,
                                Type = Property.PropertyType.Neckline,
                                Dresses = null
                            }
                        }
                    }
                }
            }.AsQueryable();


       


            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.Provider).Returns(dummyData.Provider);
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.Expression).Returns(dummyData.Expression);
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.ElementType).Returns(dummyData.ElementType);
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.GetEnumerator()).Returns(dummyData.GetEnumerator());


            //als Dress uit de DbContext opgevraagd ook een mock object teruggeven
            mockDbContext.Setup(x => x.Dresses).Returns(mockDbSetDress.Object);
          
            DressController c = new DressController(mockDbContext.Object);

            return c;
        }

       
    }
}
