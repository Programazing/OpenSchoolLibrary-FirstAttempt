using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Open_School_Library.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Open_School_Library.Tests.Controllers
{
    public class HomeControllerTests: IDisposable
    {
        private HomeController _homeController;

        public HomeControllerTests()
        {
            _homeController = new HomeController();
        }

        [Fact]
        public void IndexReturnsAView()
        {
            ViewResult result = _homeController.Index() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void AboutReturnsAView()
        {
            ViewResult result = _homeController.About() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ContactReturnsAView()
        {
            ViewResult result = _homeController.Contact() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ErrorReturnsAView()
        {
            ViewResult result = _homeController.Error() as ViewResult;

            Assert.NotNull(result);
        }

        public void Dispose()
        {
            _homeController.Dispose();
        }
    }
}
