using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TaskManagement.Web.Areas.Manager.Controllers;
using TaskManagement.Web.Models.Data;
using System.Linq;

namespace TaskManagement.Web.Test.Area.Manager.Controllers
{
    [TestFixture]
    public class AreaControllerTest
    {
        private static readonly List<Models.Area> AreaList = new List<Models.Area> {
                                                                                new Models.Area {
                                                                                                    Name = "First"
                                                                                                },
                                                                                new Models.Area {
                                                                                                    Name = "Second"
                                                                                                },
                                                                                new Models.Area {
                                                                                                    Name = "Third"
                                                                                                },
                                                                            };

        [Test]
        public void Test()
        {
            // Arrange
            var controller = GetAreaController();

            // Act
            ViewResult result = (ViewResult)controller.Index();

            // Assert
            var areas = result.Model as IEnumerable<Models.Area>;
            Assert.That(areas, Has.Count.EqualTo(AreaList.Count));
        }

        private static AreaController GetAreaController()
        {
            var areaRepositoryMock = new Mock<IAreaRepository>();
            areaRepositoryMock.Setup(a => a.FindAll()).Returns(AreaList.AsQueryable);

            return new AreaController(areaRepositoryMock.Object);
        }
    }
}