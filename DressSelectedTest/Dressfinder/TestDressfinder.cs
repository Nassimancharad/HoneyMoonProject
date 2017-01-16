using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoneymoonShop.Controllers;
using HoneymoonShop.Model.DressModels;
using HoneymoonShop.Model.DressModels.FilterMenu;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DressSelectedTest.Dressfinder
{
    public class TestDressfinder
    {


        [Fact]
        public void FiltersOnPriceBrandAndStyle()
        {
            //Arrange and Act

            SearchItems filter = new SearchItems();
            filter.Initialized = true;
            filter.SelectedMinPrice = 7;
            filter.SelectedMaxPrice = 8;
            filter.Brands.Add("UniqueBrand1");
            filter.Styles.Add(5);


            var controller = MockSetuper.SetMockUpAndGetDressController();

            var result = controller.Browse(filter);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;

            //Assert
            Assert.Equal(1, dresses.Count);
            Assert.Equal("6th Dress", dresses.First().Name);


        }


        [Fact]
        public void FiltersOnPriceAndBrand()
        {
            //Arrange and Act

            SearchItems filter = new SearchItems();
            filter.Initialized = true;
            filter.SelectedMinPrice = 7;
            filter.SelectedMaxPrice = 10;
            filter.Brands.Add("UniqueBrand3");

            var controller = MockSetuper.SetMockUpAndGetDressController();

            var result = controller.Browse(filter);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;

            //Assert
            Assert.Equal(1, dresses.Count);
            Assert.Equal("UniqueBrand3", dresses.First().BrandName);


        }

        [Fact]
        public void FiltersOnPrice()
        {
            //Arrange and Act

            SearchItems priceItems = new SearchItems();
            priceItems.Initialized = true;
            priceItems.SelectedMinPrice = 7;
            priceItems.SelectedMaxPrice = 10;



            var controller = MockSetuper.SetMockUpAndGetDressController();

            var result = controller.Browse(priceItems);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;

            //Assert
            Assert.Equal(3, dresses.Count);
            

        }

        [Fact]
        public void FiltersOnSilhouette()
        {
            //Arrange and Act

            SearchItems styleItems = new SearchItems();
            styleItems.Initialized = true;
            styleItems.Necklines.Add(24); // Princess model


            var controller = MockSetuper.SetMockUpAndGetDressController();

            var result = controller.Browse(styleItems);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;

            //Assert
            Assert.Equal(1, dresses.Count);
            Assert.Equal("ExpensiveDress", dresses.First().Name);
          
        }


        [Fact]
        public void FiltersOnNecklines()
        {
            //Arrange and Act

            SearchItems styleItems = new SearchItems();
            styleItems.Initialized = true;
            styleItems.Necklines.Add(23); // Lage rug


            var controller = MockSetuper.SetMockUpAndGetDressController();

            var result = controller.Browse(styleItems);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;

            //Assert
            Assert.Equal(2, dresses.Count);
            Assert.Equal("AffordableDress", dresses.First().Name);
            Assert.Equal("CheapDress", dresses.ElementAt(1).Name);
        }

        [Fact]
        public void FiltersOnStyle()
        {
            //Arrange and Act

            SearchItems styleItems = new SearchItems();
            styleItems.Initialized = true;
            styleItems.Styles.Add(12); //verleiderlijk


            var controller = MockSetuper.SetMockUpAndGetDressController();

            var result = controller.Browse(styleItems);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;

            //Assert
            Assert.Equal(2, dresses.Count);
            Assert.Equal("AffordableDressHigh", dresses.First().Name);
            Assert.Equal("AffordableDressMiddle", dresses.ElementAt(1).Name);
        }


        [Fact]
        public void FiltersOnBrand()
        {
            //Arrange and Act

            SearchItems brandItems = new SearchItems();
            brandItems.Initialized = true; 
            brandItems.Brands.Add("UniqueBrand1");


            var controller = MockSetuper.SetMockUpAndGetDressController();

            var result = controller.Browse(brandItems);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;

            //Assert
           Assert.Equal("UniqueBrand1", dresses.ElementAt(0).BrandName);
            Assert.Equal("UniqueBrand1", dresses.ElementAt(1).BrandName);
            Assert.Equal("UniqueBrand1", dresses.ElementAt(2).BrandName);

            Assert.Equal(3, dresses.Count);
        }

        [Fact]
        public void SortsOnPrice()
        {
            //Arrange and Act

            var priceSorter = new List<DressFinderController.Sorter>
            {
                new DressFinderController.Sorter
                {
                    Name = "testSort",
                    Id = 999,
                    Sort = dressList => dressList.OrderBy(dress => dress.PriceIndication)
                }
            };

            var controller = MockSetuper.SetMockUpAndGetDressController(priceSorter);
            DressFinderController.BrowseParameters param = new DressFinderController.BrowseParameters()
            {
                Sort = 999
            };

            var result = controller.Browse(parameters:param);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;

            //Assert
            Assert.True(dresses.ElementAt(0).PriceIndication < dresses.ElementAt(1).PriceIndication);
            Assert.True(dresses.ElementAt(1).PriceIndication < dresses.ElementAt(2).PriceIndication);
        }



        [Fact]
        public void UnSortedOrder()
        {
            //Arrange
            var controller = MockSetuper.SetMockUpAndGetDressController();
            //            controller.SetSorters(priceSorter);
            var result = controller.Browse();
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;


            //Assert
            Assert.Equal("6th Dress", dresses.ElementAt(0).Name);
            Assert.Equal("CheapDress", dresses.ElementAt(dresses.Count-1).Name);
        }


        [Fact]
        public void AllPossibleCategoriesAreRetrievedWhenCatIdNotPassed()
        {
            //Arrange
            var controller = MockSetuper.SetMockUpAndGetDressController();
            //            controller.SetSorters(priceSorter);
            var result = controller.Browse();
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;


            //Assert
            Assert.Equal(3, superModel.Categories.Count);
        }

        [Fact]
        public void AllDressesAreRetrievedWhenCatIdNotPassed()
        {
            //Arrange
            var controller = MockSetuper.SetMockUpAndGetDressController();
            //            controller.SetSorters(priceSorter);
            var result = controller.Browse();
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;


            //Assert
            Assert.Equal(6, dresses.Count);
        }

        [Fact]
        public void CurrentCategoryIsRetrieved()
        {
            //Arrange
            var controller = MockSetuper.SetMockUpAndGetDressController();
            //            controller.SetSorters(priceSorter);
            DressFinderController.BrowseParameters parameters = new DressFinderController.BrowseParameters()
            {
                Category = 2,
            };
            var result = controller.Browse(parameters: parameters);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;


            //Assert
            Assert.Equal(2, superModel.CurrentCategory);
        }

        [Fact]
        public void NonExistentCatIDWillShowAllDresses()
        {
            //Arrange
            var controller = MockSetuper.SetMockUpAndGetDressController();
            //            controller.SetSorters(priceSorter);

            //Act
            int nonExistentCatID = 999; 
            var result = controller.Browse(parameters:null);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;


            //Assert
            Assert.Equal(6, dresses.Count);
        }

        [Fact]
        public void MissingCatIDWillShowAllDresses()
        {
            //Arrange
            var controller = MockSetuper.SetMockUpAndGetDressController();
            //            controller.SetSorters(priceSorter);

            //Act
            int nonExistentCatID = 999;
            var result = controller.Browse();
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;


            //Assert
            Assert.Equal(-1, superModel.CurrentCategory);
            Assert.Equal(6, dresses.Count);
        }


        [Fact]
        public void FilterByCategory()
        {
            //Arrange
            var controller = MockSetuper.SetMockUpAndGetDressController();
            //            controller.SetSorters(priceSorter);
            DressFinderController.BrowseParameters param = new DressFinderController.BrowseParameters()
            {
                Category = 2,
            };
            var result = controller.Browse(parameters:param);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;


            //Assert
          

            Assert.Equal(2, dresses.Count);
            
        }

        [Fact]
        public void FilterByCategoryAndSortByPrice()
        {

            var priceSorter = new List<DressFinderController.Sorter>
            {
                new DressFinderController.Sorter
                {
                    Name = "testSort",
                    Id = 999,
                    Sort = dressList => dressList.OrderBy(dress => dress.PriceIndication)
                }
            };

            //Arrange
            var controller = MockSetuper.SetMockUpAndGetDressController(priceSorter);
            //            controller.SetSorters(priceSorter);
            DressFinderController.BrowseParameters param = new DressFinderController.BrowseParameters()
            {
                Category = 2,
            };
            var result = controller.Browse(parameters:param);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            SuperModel superModel = viewResult.Model as SuperModel;
            var dresses = superModel.Dresses;


            //Assert

            Assert.Equal("ExpensiveDress", dresses.ElementAt(0).Name);
            Assert.Equal("CheapDress", dresses.ElementAt(1).Name);
            Assert.Equal(2, dresses.Count);

        }
    }
}
