using HoneymoonShop.Controllers;
using HoneymoonShop.Model.DressModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DressSelectedTest.SelectedDress
{
    public class TestDress
    {
        public TestDress()
        {
        }


        [Fact]
        public void ResultIsEenView()
        {
            int id = 111;
            DressController c = MockSetuper.SetMockUpAndGetDressController();
            var result = c.Index(dressid:111);
            
            //result moet een view zijn
            var viewResult = Assert.IsType<ViewResult>(result);

        }


        [Fact]
        public void NameOfDressIsCorrect()
        {
            int id = 111;
            DressController c = MockSetuper.SetMockUpAndGetDressController();
            var result = c.Index(id);

            //result moet een view zijn
            var viewResult = Assert.IsType<ViewResult>(result);

            //Check model
            var model = (Dress)viewResult.Model;

            Assert.Equal("Armani", model.Name);
        }

        [Fact]
        public void ErrorPageWhenInvalidID()
        {
            int id = -1;
            DressController c = MockSetuper.SetMockUpAndGetDressController();
            var result = c.Index(id);

            //result moet een view zijn
            var viewResult = Assert.IsType<ViewResult>(result);

            //View is van type Error
            Assert.Equal("Error", viewResult.ViewName);
        }

    }
}
