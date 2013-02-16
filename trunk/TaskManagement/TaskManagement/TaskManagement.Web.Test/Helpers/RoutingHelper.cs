using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace TaskManagement.Web.Test.Helpers
{
    public static class RoutingHelper
    {
        public static RouteData TestRoute(string url, object expectedValues)
        {
            var routeConfig = new RouteCollection();
            MvcApplication.RegisterRoutes(routeConfig);

            var mockHttpContext = MakeMockHttpContext(url);

            RouteData routeData = routeConfig.GetRouteData(mockHttpContext.Object);

            Assert.That(routeData.Route, Is.Not.Null, "No route was matched.");

            var expectedDict = new RouteValueDictionary(expectedValues);

            foreach (var expectedValue in expectedDict)
            {
                if (expectedValue.Value == null)
                {
                    Assert.That(routeData.Values[expectedValue.Key], Is.Null);
                }
                else
                {
                    Assert.That(expectedValue.Value.ToString(), 
                        Is.EqualTo(routeData.Values[expectedValue.Key].ToString()));
                }
            }

            return routeData;
        }

        private static Mock<HttpContextBase> MakeMockHttpContext(string url)
        {
            var mockHttpContext = new Mock<HttpContextBase>();
            
            // Mock the request
            var mockRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);
            
            // Mock the response
            var mockResponse = new Mock<HttpResponseBase>();
            mockHttpContext.Setup(x => x.Response).Returns(mockResponse.Object);
            mockResponse.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(x => x);

            return mockHttpContext;
        }
    }

    public class RoutesTestClassBase<TAreaRegistration>
    {
        protected void TestRoute(string url, object expectations)
        {
            var routes = new RouteCollection();
            var areaRegistration = (AreaRegistration)Activator.CreateInstance(typeof(TAreaRegistration));

            // Get an AreaRegistrationContext for my class. Give it an empty RouteCollection
            var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, routes);
            areaRegistration.RegisterArea(areaRegistrationContext);

            url = "~/" + areaRegistration.AreaName + "/" + url;

            // Mock up an HttpContext object with my test path (using Moq)
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns(url);

            // Get the RouteData based on the HttpContext
            var routeData = routes.GetRouteData(context.Object);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual(areaRegistration.AreaName, routeData.DataTokens["area"]);

            foreach (PropertyValue property in GetProperties(expectations))
            {
                Assert.IsTrue(string.Equals(property.Value.ToString(),
                    routeData.Values[property.Name].ToString(),
                    StringComparison.OrdinalIgnoreCase)
                    , string.Format("Expected '{0}', not '{1}' for '{2}'.",
                    property.Value, routeData.Values[property.Name], property.Name));
            }
        }

        private static IEnumerable<PropertyValue> GetProperties(object o)
        {
            if (o != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(o);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(o);
                    if (val != null)
                    {
                        yield return new PropertyValue { Name = prop.Name, Value = val };
                    }
                }
            }
        }

        private sealed class PropertyValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}