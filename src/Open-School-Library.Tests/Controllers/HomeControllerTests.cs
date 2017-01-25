using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Open_School_Library.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Open_School_Library.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnsAView()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.NotNull(result);
        }
    }
}
