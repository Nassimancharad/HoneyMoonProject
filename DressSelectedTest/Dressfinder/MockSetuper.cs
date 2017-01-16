using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using HoneymoonShop.Controllers;
using HoneymoonShop.Model;
using HoneymoonShop.Model.AppointmentModels;
using HoneymoonShop.Model.DressModels;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DressSelectedTest.Dressfinder
{
    public class MockSetuper
    {
        public static DressFinderController SetMockUpAndGetDressController(List<DressFinderController.Sorter> sorter=null)
        {

            #region Declare mockObjects

            //maak alle mockobjecten aan
            var mockDbContext = new Mock<DBContext>();
            var mockDbSetDress = new Mock<DbSet<Dress>>();
            var mockDbSetCategory = new Mock<DbSet<Category>>();
            var mockDbSetBrand = new Mock<DbSet<Brand>>();
            var mockDbSetProperty = new Mock<DbSet<Property>>();
            var mockDbSetDressProperty = new Mock<DbSet<DressProperty>>();
            var mockDbSetDressCategory = new Mock<DbSet<DressCategory>>();
            var mockDbSetAppointment = new Mock<DbSet<Appointment>>();
            var mockDbSetJewelry = new Mock<DbSet<Jewelry>>();

            var mockSuperModel = new Mock<SuperModel>();

            #endregion

            #region DummyDressData
            // Maak dummydata aan


            var dummyData = new List<Dress>
            {
                 new Dress
                {

                    Name = "6th Dress",
                    Brand = new Brand {Name = "UniqueBrand1", Dresses = null},
                    DressId = 666,
                    BrandName = "UniqueBrand1",
                    Colors = Color.Color,
                    Description = "TestDescription",
                    PriceIndication = 8,
                    Images = new List<Image>
                    {
                        new Image {Dress = null, DressId = 666, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>
                    {
                        new DressProperty
                        {
                            Dress = null,
                            DressId = 666,
                            PropertyId = 5,   //Modieuze bruid
                            Property = new Property
                            {
                                Name = "Verleiderlijk",
                                PropertyId = 5,   //Modieuze bruid
                                Type = Property.PropertyType.Style,  // int= 0
                                Dresses = null
                            }
                        }
                    },
                    Jewelry = null,
                    Categories = new List<DressCategory>()
                    {
                        new DressCategory()
                        {
                                DressId = 111, Category = new Category() {Name = "CategoryA", Dresses = null, CategoryId = 4}, Dress = null, CategoryId = 4
                        }
                    }
                },


                 new Dress
                {

                    Name = "AffordableDressHigh",
                    Brand = new Brand {Name = "UniqueBrand1", Dresses = null},
                    DressId = 111,
                    BrandName = "UniqueBrand1",
                    Colors = Color.Color,
                    Description = "TestDescription",
                    PriceIndication = 7,
                    Images = new List<Image>
                    {
                        new Image {Dress = null, DressId = 111, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>
                    {
                        new DressProperty
                        {
                            Dress = null,
                            DressId = 111,
                            PropertyId = 12,  //Verleiderlijk
                            Property = new Property
                            {
                                Name = "Verleiderlijk",
                                PropertyId = 12,   //Verleiderlijk
                                Type = Property.PropertyType.Style,  // int= 1
                                Dresses = null
                            }
                        }
                    },
                    Jewelry = null,
                    Categories = new List<DressCategory>()
                    {
                        new DressCategory()
                        {
                                DressId = 111, Category = new Category() {Name = "CategoryA", Dresses = null, CategoryId = 1}, Dress = null, CategoryId = 1
                        }
                    }
                },

                 new Dress
                {

                    Name = "AffordableDressMiddle",
                    Brand = new Brand {Name = "UniqueBrand1", Dresses = null},
                    DressId = 1111,
                    BrandName = "UniqueBrand1",
                    Colors = Color.Color,
                    Description = "TestDescription",
                    PriceIndication = 6,
                    Images = new List<Image>
                    {
                        new Image {Dress = null, DressId = 111, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>
                    {
                        new DressProperty
                        {
                            Dress = null,
                            DressId = 1111,
                            PropertyId = 12, //Verleiderlijk
                            Property = new Property
                            {
                                Name = "Verleiderlijk",
                                PropertyId = 12, //Verleiderlijk
                                Type = Property.PropertyType.Style,
                                Dresses = null
                            }
                        }
                    },
                    Jewelry = null,
                    Categories = new List<DressCategory>()
                    {
                        new DressCategory()
                        {
                                DressId = 1111, Category = new Category() {Name = "CategoryA", Dresses = null, CategoryId = 1}, Dress = null, CategoryId = 1
                        }
                    }
                },



                new Dress
                {
                    
                    Name = "AffordableDress",
                    Brand = new Brand {Name = "UniqueBrand2", Dresses = null},
                    DressId = 111,
                    BrandName = "",
                    Colors = Color.Color,
                    Description = "UniqueBrand2",
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
                            DressId = 111,
                            PropertyId = 23,  //Lage rug
                            Property = new Property
                            {
                                Name = "propName",
                                PropertyId =23,  //Lage rug
                                Type = Property.PropertyType.Neckline,
                                Dresses = null
                            }
                        }
                    },
                    Jewelry = null,
                    Categories = new List<DressCategory>()
                    {
                        new DressCategory()
                        {
                                DressId = 111, Category = new Category() {Name = "CategoryA", Dresses = null, CategoryId = 1}, Dress = null, CategoryId = 1
                        }
                    }
                },
                new Dress
                {
                    Name = "ExpensiveDress",
                    Brand = new Brand {Name = "UniqueBrand3", Dresses = null},
                    DressId = 222,
                    BrandName = "UniqueBrand3",
                    Colors = Color.Ivory,
                    Description = "TestDescription2",
                    PriceIndication = 10,
                    Images = new List<Image>
                    {
                        new Image {Dress = null, DressId = 222, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>
                    {
                        new DressProperty
                        {
                            Dress = null,
                            DressId = 222,
                            PropertyId = 24, // Princess model
                            Property = new Property
                            {
                                Name = "propName2",
                                PropertyId = 24, // Princess model
                                Type = Property.PropertyType.Silhouette,
                                Dresses = null
                            }
                        }
                    },
                    Categories = new List<DressCategory>()
                    {
                        new DressCategory()
                        {
                                DressId = 222, Category = new Category() {Name = "CategoryB", Dresses = null, CategoryId = 2}, Dress = null, CategoryId = 2
                        }
                    }
                },
                new Dress
                {
                    Name = "CheapDress",
                    Brand = new Brand {Name = "UniqueBrand4", Dresses = null},
                    DressId = 333,
                    BrandName = "UniqueBrand4",
                    Colors = Color.Ivory,
                    Description = "TestDescription3",
                    PriceIndication = 1,
                    Images = new List<Image>
                    {
                        new Image {Dress = null, DressId = 333, DressURL = "testurl"}
                    },
                    Properties = new List<DressProperty>
                    {
                        new DressProperty
                        {
                            Dress = null,
                            DressId = 333,
                            PropertyId = 23,  //Lage rug
                            Property = new Property
                            {
                                Name = "propName3",
                                PropertyId = 23,  //Lage rug
                                Type = Property.PropertyType.Neckline,
                                Dresses = null
                            }
                        }
                    },
                    Categories = new List<DressCategory>()
                    {
                        new DressCategory()
                        {
                                DressId = 333, Category = new Category() {Name = "CategoryC", Dresses = null, CategoryId = 2}, Dress = null, CategoryId = 2
                        }
                    }
                }
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
                    Sieraden = null,
                    Dresses = null
                }
            }.AsQueryable();

            #endregion

            #region DummyCategoriesData

            var DummyCategoriesData = new List<Category>()
            {
                new Category()
                {
                    Name = "CategoryA",
                    Dresses = null,
                    CategoryId = 1
                },

                new Category()
                {
                    Name = "CategoryB",
                    Dresses = null,
                    CategoryId = 2
                },

                new Category()
                {
                    Name = "CategoryC",
                    Dresses = null,
                    CategoryId = 3
                },
            }.AsQueryable();



            #endregion

            #region DummyDressCategoriesData

            var dummyDressCategories = new List<DressCategory>()
            {
                new DressCategory()
                {
                    Dress = null,
                    DressId = 111,
                    CategoryId = 1,
                    Category = new Category()
                    {
                        Name = "CategoryA",
                        Dresses = null,
                        CategoryId = 1
                    }
                },

                new DressCategory()
                {
                    Dress = null,
                    DressId = 222,
                    CategoryId = 2,
                    Category = new Category()
                    {
                        Name = "CategoryB",
                        Dresses = null,
                        CategoryId = 2
                    }
                },
                new DressCategory()
                {
                    Dress = null,
                    DressId = 333,
                    CategoryId = 3,
                    Category = new Category()
                    {
                        Name = "CategoryC",
                        Dresses = null,
                        CategoryId = 3
                    }
                }
            }.AsQueryable();

            #endregion

            #region Setup mockDressList
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.Provider).Returns(dummyData.Provider);
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.Expression).Returns(dummyData.Expression);
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.ElementType).Returns(dummyData.ElementType);
            mockDbSetDress.As<IQueryable<Dress>>().Setup(m => m.GetEnumerator()).Returns(dummyData.GetEnumerator());


            #endregion

            #region Setup mockPropertiesList
            mockDbSetProperty.As<IQueryable<Property>>().Setup(m => m.Provider).Returns(dummyproperties.Provider);
            mockDbSetProperty.As<IQueryable<Property>>().Setup(m => m.Expression).Returns(dummyproperties.Expression);
            mockDbSetProperty.As<IQueryable<Property>>().Setup(m => m.ElementType).Returns(dummyproperties.ElementType);
            mockDbSetProperty.As<IQueryable<Property>>().Setup(m => m.GetEnumerator()).Returns(dummyproperties.GetEnumerator());


            #endregion

            #region Setup mockCategoriesList
            mockDbSetCategory.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(DummyCategoriesData.Provider);
            mockDbSetCategory.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(DummyCategoriesData.Expression);
            mockDbSetCategory.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(DummyCategoriesData.ElementType);
            mockDbSetCategory.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(DummyCategoriesData.GetEnumerator());


            #endregion

            #region Setup mockBrandProperties
            mockDbSetBrand.As<IQueryable<Brand>>().Setup(m => m.Provider).Returns(dummyBrands.Provider);
            mockDbSetBrand.As<IQueryable<Brand>>().Setup(m => m.Expression).Returns(dummyBrands.Expression);
            mockDbSetBrand.As<IQueryable<Brand>>().Setup(m => m.ElementType).Returns(dummyBrands.ElementType);
            mockDbSetBrand.As<IQueryable<Brand>>().Setup(m => m.GetEnumerator()).Returns(dummyBrands.GetEnumerator());


            #endregion

            #region Setup mockDressCategories
            mockDbSetDressCategory.As<IQueryable<DressCategory>>().Setup(m => m.Provider).Returns(dummyDressCategories.Provider);
            mockDbSetDressCategory.As<IQueryable<DressCategory>>().Setup(m => m.Expression).Returns(dummyDressCategories.Expression);
            mockDbSetDressCategory.As<IQueryable<DressCategory>>().Setup(m => m.ElementType).Returns(dummyDressCategories.ElementType);
            mockDbSetDressCategory.As<IQueryable<DressCategory>>().Setup(m => m.GetEnumerator()).Returns(dummyDressCategories.GetEnumerator());


            #endregion

            //als Dress uit de DbContext opgevraagd ook een mock object teruggeven
            mockDbContext.Setup(x => x.Dresses).Returns(mockDbSetDress.Object);
            mockDbContext.Setup(x => x.Properties).Returns(mockDbSetProperty.Object);
            mockDbContext.Setup(x => x.Brands).Returns(mockDbSetBrand.Object);
            mockDbContext.Setup(x => x.Categories).Returns(mockDbSetCategory.Object);
            mockDbContext.Setup(x => x.DressCategories).Returns(mockDbSetDressCategory.Object);

            var c = new DressFinderController(mockDbContext.Object);
            c.SetSorters(sorter);
           
            return c;
        }
    }
}