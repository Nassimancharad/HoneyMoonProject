using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using HoneymoonShop.Model;
using HoneymoonShop.Model.DressModels;
using HoneymoonShop.Controllers;

namespace Test1
{
    public class MockSetup
    {
        public static DressFinderController SetMockUpAndGetDressController(List<DressFinderController.Sorter> sorter = null) {

            #region Declare mockObjects

            //maak alle mockobjecten aan
            var mockDbContext = new Mock<DBContext>();
            var mockDbSetDress = new Mock<DbSet<Dress>>();
            var mockDbSetBrand = new Mock<DbSet<Brand>>();
            var mockDbSetProperty = new Mock<DbSet<Property>>();
            var mockDbSetDressProperty = new Mock<DbSet<DressProperty>>();
            var mockDbSetDressCategory = new Mock<DbSet<DressCategory>>();
   
            var mockSuperModel = new Mock<SuperModel>();

            #endregion

            #region DmmyDressData
            // Maak dummydata aan


            var dmmyData = new List<Dress>
            {
                 new Dress
                {

                    Name = "frst Dress",
                    Brand = new Brand {Name = "Brand1", Dresses = null},
                    DressId = 1,
                    BrandName = "Brand1",
                    Colors = Color.Color,
                    Description = "TestDescription",
                    PriceIndication = 8,
                    Images = new List<Image>
                    {
                        new Image {Dress = null, DressId = 1, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>
                    {
                        new DressProperty
                        {
                            Dress = null,
                            DressId = 1,
                            PropertyId = 5,  
                            Property = new Property
                            {
                                Name = "Gatar",
                                PropertyId = 5,   
                                Type = Property.PropertyType.Style, 
                                Dresses = null
                            }
                        }
                    },
               
                },


                 new Dress
                {

                    Name = "SecondDress",
                    Brand = new Brand {Name = "Brand1", Dresses = null},
                    DressId = 2,
                    BrandName = "Brand1",
                    Colors = Color.Color,
                    Description = "TestDescription",
                    PriceIndication = 7,
                    Images = new List<Image>
                    {
                        new Image {Dress = null, DressId = 2, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>
                    {
                        new DressProperty
                        {
                            Dress = null,
                            DressId = 2,
                            PropertyId = 12,  
                            Property = new Property
                            {
                                Name = "Mooi",
                                PropertyId = 12,  
                                Type = Property.PropertyType.Style,  // int= 1
                                Dresses = null
                            }
                        }
                    },
                
                },

                 new Dress
                {

                    Name = "ThirdDress",
                    Brand = new Brand {Name = "Brand1", Dresses = null},
                    DressId = 3,
                    BrandName = "Brand1",
                    Colors = Color.Color,
                    Description = "TestDescription",
                    PriceIndication = 6,
                    Images = new List<Image>
                    {
                        new Image {Dress = null, DressId = 3, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>
                    {
                        new DressProperty
                        {
                            Dress = null,
                            DressId = 3,
                            PropertyId = 12, 
                            Property = new Property
                            {
                                Name = "mooi",
                                PropertyId = 12, 
                                Type = Property.PropertyType.Style,
                                Dresses = null
                            }
                        }
                    },
                 
                },



                new Dress
                {

                    Name = "Dress4",
                    Brand = new Brand {Name = "Brand2", Dresses = null},
                    DressId = 4,
                    BrandName = "",
                    Colors = Color.Color,
                    Description = "Brand2",
                    PriceIndication = 5,
                    Images = new List<Image>
                    {
                        new Image {Dress = null, DressId = 111, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>
                    {
                        new DressProperty
                        {
                            Dress = null,
                            DressId = 4,
                            PropertyId = 23,  
                            Property = new Property
                            {
                                Name = "propName",
                                PropertyId =23,  
                                Type = Property.PropertyType.Neckline,
                                Dresses = null
                            }
                        }
                    },
                  
                },
              
            }.AsQueryable();


            #endregion

            #region DummyPropertyData

            var dummyproperties = new List<Property>()
            {
                new Property()
                {
                    Name = "Dure property",
                    Dresses = null,
                    PropertyId = 1,
                    Type = Property.PropertyType.Neckline
                },

                new Property()
                {
                    Name = "Affordable property",
                    Dresses = null,
                    PropertyId = 2,
                    Type = Property.PropertyType.Silhouette
                },

                new Property()
                {
                    Name = "Cheap property",
                    Dresses = null,
                    PropertyId = 3,
                    Type = Property.PropertyType.Style
                },
            }.AsQueryable();

            #endregion

            #region DummyBrandData

            var dummyBrands = new List<Brand>()
            {
                new Brand()
                {
                    Name = "Brand 1",
                    Dresses = null
                }
            }
           

            

           
          



            #endregion

            #region DummyDressCategoriesData

          .AsQueryable();

            #endregion

            #region Setup mockDressList
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.Provider).Returns(dmmyData.Provider);
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.Expression).Returns(dmmyData.Expression);
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.ElementType).Returns(dmmyData.ElementType);
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.GetEnumerator()).Returns(dmmyData.GetEnumerator());


            #endregion

            #region Setup mockPropertiesList
            mockDbSetProperty.As<IQueryable<Property>>().Setup(m => m.Provider).Returns(dummyproperties.Provider);
            mockDbSetProperty.As<IQueryable<Property>>().Setup(m => m.Expression).Returns(dummyproperties.Expression);
            mockDbSetProperty.As<IQueryable<Property>>().Setup(m => m.ElementType).Returns(dummyproperties.ElementType);
            mockDbSetProperty.As<IQueryable<Property>>().Setup(m => m.GetEnumerator()).Returns(dummyproperties.GetEnumerator());


         

         


            #endregion

            #region Setup mockBrandProperties
            mockDbSetBrand.As<IQueryable<Brand>>().Setup(m => m.Provider).Returns(dummyBrands.Provider);
            mockDbSetBrand.As<IQueryable<Brand>>().Setup(m => m.Expression).Returns(dummyBrands.Expression);
            mockDbSetBrand.As<IQueryable<Brand>>().Setup(m => m.ElementType).Returns(dummyBrands.ElementType);
            mockDbSetBrand.As<IQueryable<Brand>>().Setup(m => m.GetEnumerator()).Returns(dummyBrands.GetEnumerator());


           


            #endregion

            //als Dress uit de DbContext opgevraagd ook een mock object teruggeven
            mockDbContext.Setup(x => x.Dresses).Returns(mockDbSetDress.Object);
            mockDbContext.Setup(x => x.Properties).Returns(mockDbSetProperty.Object);
            mockDbContext.Setup(x => x.Brands).Returns(mockDbSetBrand.Object);
            mockDbContext.Setup(x => x.DressCategories).Returns(mockDbSetDressCategory.Object);

            var c = new DressFinderController(mockDbContext.Object);
            c.SetSorters(sorter);

            return c;
        }
    }
}