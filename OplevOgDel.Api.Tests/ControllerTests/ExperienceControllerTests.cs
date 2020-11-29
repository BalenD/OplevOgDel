using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OplevOgDel.Api.Controllers;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Dto.ExperienceDto;
using OplevOgDel.Api.Models.Dto.RequestDto;
using OplevOgDel.Api.Services;
using Xunit;

namespace OplevOgDel.Api.Tests.ControllerTests
{
    public class ExperienceControllerTests
    {
        private readonly Mock<IExperienceRepository> mockRepo;
        private readonly Mock<ILogger> mockLogger;
        private readonly Mapper _autoMapperConfig;
        private ExperienceController controller;

        public ExperienceControllerTests()
        {
            // arrange
            mockRepo = new Mock<IExperienceRepository>();
            mockLogger = new Mock<ILogger>();
            
            

            _autoMapperConfig = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new Helpers.AutoMapper())));
            controller = new ExperienceController(mockLogger.Object, mockRepo.Object, _autoMapperConfig);
        }

        

        [Fact]
        public async void GetAllExperiences()
        {
            // arrange
            mockRepo.Setup(x => x.GetAllAsync(It.IsAny<ExperienceRequestParametersDto>())).ReturnsAsync(GetAllAsyncTest);

            // act
            var req = new ExperienceRequestParametersDto();
            var result = await controller.GetAllExperiences(req);

            // assert
            var resultValue = Assert.IsAssignableFrom<OkObjectResult>(result);
            var returnedItems = Assert.IsType<List<ViewExperienceDto>>(resultValue.Value);
            Assert.Equal(2, returnedItems.Count);
            
        }

        [Fact]
        public async void GetOneExperience_Found()
        {
            // arrange
            mockRepo.Setup(x => x.GetAnExperienceAsync(It.IsAny<Guid>())).ReturnsAsync(GetOneAsyncTest);

            // act
            var id = Guid.NewGuid();
            var result = await controller.GetOneExperience(id);

            // assert
            var resultValue = Assert.IsAssignableFrom<OkObjectResult>(result);
            var returnedExperience = Assert.IsType<ViewOneExperienceDto>(resultValue.Value);
            Assert.Matches("testing2", returnedExperience.Name);
        }

        [Fact]
        public async void GetOneExperince_NotFound()
        {
            // arrange
            mockRepo.Setup(x => x.GetAnExperienceAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            // act
            var id = Guid.NewGuid();
            var result = await controller.GetOneExperience(id);

            // assert
            var resultValue = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
            var returnedObj = Assert.IsType<ErrorObject>(resultValue.Value);
            Assert.Equal("Could not find experience", returnedObj.Error);
            Assert.Equal("GET", returnedObj.Method);
            Assert.Equal($"/api/Experiences/{id}", returnedObj.At);
            Assert.Equal(404, returnedObj.StatusCode);
        }

        [Fact]
        public async void UpdateOneExperience_NotFound()
        {
            // arrange
            mockRepo.Setup(x => x.GetFirstByExpressionAsync(It.IsAny<Expression<Func<Experience, bool>>>())).ReturnsAsync(() => null);
            var id = Guid.NewGuid();
            var newObj = new EditExperienceDto() { Address = "hejhej" };
            
            // act
            var result = await controller.UpdateOneExperience(id, newObj);

            // assert
            var resultValue = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
            var returnedObj = Assert.IsType<ErrorObject>(resultValue.Value);
            Assert.Equal("Could not find experience to edit", returnedObj.Error);
            Assert.Equal("PUT", returnedObj.Method);
            Assert.Equal($"/api/experiences/{id}", returnedObj.At);
            Assert.Equal(404, returnedObj.StatusCode);
        }

        [Fact]
        public async void UpdateOneExperience_UserDoesNotOwn()
        {
            // arrange
            mockRepo.Setup(x => x.GetFirstByExpressionAsync(It.IsAny<Expression<Func<Experience, bool>>>())).ReturnsAsync(GetOneAsyncTest);
            mockRepo.Setup(x => x.GetCategoryByNameAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            var id = Guid.NewGuid();
            var newObj = new EditExperienceDto() { Address = "hejhej"  };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.Role, "User"),
                 new Claim("profileId", Guid.NewGuid().ToString())
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            
            // act
            var result = await controller.UpdateOneExperience(id, newObj);

            // assert
            var resultValue = Assert.IsAssignableFrom<UnauthorizedObjectResult>(result);
            var returnedObj = Assert.IsType<ErrorObject>(resultValue.Value);
            Assert.Equal("Unauthorized to perform this action", returnedObj.Error);
            Assert.Equal("PUT", returnedObj.Method);
            Assert.Equal($"/api/experiences/{id}", returnedObj.At);
            Assert.Equal(401, returnedObj.StatusCode);
        }

        [Fact]
        public async void UpdateOneExperience_Problem()
        {
            // arrange
            mockRepo.Setup(x => x.GetFirstByExpressionAsync(It.IsAny<Expression<Func<Experience, bool>>>())).ReturnsAsync(GetOneAsyncTest);
            mockRepo.Setup(x => x.GetCategoryByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new Category() { Id = Guid.NewGuid() });
            mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(() => false);
            var id = Guid.NewGuid();
            var newObj = new EditExperienceDto() { Address = "hejhej"};
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.Role, "Admin"),
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // act
            var result = await controller.UpdateOneExperience(id, newObj);

            // assert
            var resultValue = Assert.IsAssignableFrom<ObjectResult>(result);
            var returnedObj = Assert.IsType<ErrorObject>(resultValue.Value);
            Assert.Equal("Error updating an experience", returnedObj.Error);
            Assert.Equal("PUT", returnedObj.Method);
            Assert.Equal($"/api/experiences/{id}", returnedObj.At);
            Assert.Equal(500, returnedObj.StatusCode);
        }

        [Fact]
        public async void UpdateOneExperience_NoContent()
        {
            // arrange
            mockRepo.Setup(x => x.GetFirstByExpressionAsync(It.IsAny<Expression<Func<Experience, bool>>>())).ReturnsAsync(GetOneAsyncTest);
            mockRepo.Setup(x => x.GetCategoryByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new Category() { Id = Guid.NewGuid() });
            mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(() => true);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.Role, "Admin"),
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var id = Guid.NewGuid();
            var newObj = new EditExperienceDto() { Address = "hejhej" };

            // act
            var result = await controller.UpdateOneExperience(id, newObj);

            // assert
            var resultValue = Assert.IsAssignableFrom<NoContentResult>(result);

        }

        [Fact]
        public async void DeleteOneExperience_NotFound()
        {
            // arrange
            mockRepo.Setup(x => x.GetFirstByExpressionAsync(It.IsAny<Expression<Func<Experience, bool>>>())).ReturnsAsync(() => null);
            var id = Guid.NewGuid();

            // act
            var result = await controller.DeleteOneExperience(id);

            // assert
            var resultValue = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
            var returnedObj = Assert.IsType<ErrorObject>(resultValue.Value);
            Assert.Equal("Could not find experience to delete", returnedObj.Error);
            Assert.Equal("DELETE", returnedObj.Method);
            Assert.Equal($"/api/experiences/{id}", returnedObj.At);
            Assert.Equal(404, returnedObj.StatusCode);
        }

        [Fact]
        public async void DeleteOneExperience_Problem()
        {
            // arrange
            mockRepo.Setup(x => x.GetFirstByExpressionAsync(It.IsAny<Expression<Func<Experience, bool>>>())).ReturnsAsync(GetOneAsyncTest);
            mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(() => false);
            var id = Guid.NewGuid();

            // act
            var result = await controller.DeleteOneExperience(id);

            // assert
            var resultValue = Assert.IsAssignableFrom<ObjectResult>(result);
            var returnedObj = Assert.IsType<ErrorObject>(resultValue.Value);
            Assert.Equal("Error deleting an experience", returnedObj.Error);
            Assert.Equal("DELETE", returnedObj.Method);
            Assert.Equal($"/api/experiences/{id}", returnedObj.At);
            Assert.Equal(500, returnedObj.StatusCode);
        }

        [Fact]
        public async void DeleteOneExperience_Ok()
        {
            // arrange
            mockRepo.Setup(x => x.GetFirstByExpressionAsync(It.IsAny<Expression<Func<Experience, bool>>>())).ReturnsAsync(GetOneAsyncTest);
            mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(() => true);
            var id = Guid.NewGuid();

            // act
            var result = await controller.DeleteOneExperience(id);
            
            // assert
            Assert.IsAssignableFrom<NoContentResult>(result);
        }

        [Fact]
        public async void CreateOneExperience_BadRequest()
        {
            // arrange
            mockRepo.Setup(x => x.GetCategoryByNameAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            var exprToCreate = new NewExperienceDto()
            {
                Address = "testing"
            };

            // act
            var result = await controller.CreateOneExperience(exprToCreate);

            // assert
            var resultValue = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            var returnedObj = Assert.IsType<ErrorObject>(resultValue.Value);
            Assert.Equal("Category is invalid", returnedObj.Error);
            Assert.Equal("POST", returnedObj.Method);
            Assert.Equal("/api/experiences", returnedObj.At);
            Assert.Equal(400, returnedObj.StatusCode);
        }

        [Fact]
        public async void CreateOneExperience_Problem()
        {
            // arrange
            mockRepo.Setup(x => x.GetCategoryByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new Category());
            mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(() => false);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("profileId", Guid.NewGuid().ToString())
            }, "mock"));

            var exprToCreate = new NewExperienceDto()
            {
                Address = "testing"
            };

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // act
            var result = await controller.CreateOneExperience(exprToCreate);

            // assert
            var resultValue = Assert.IsAssignableFrom<ObjectResult>(result);
            var returnedObj = Assert.IsType<ErrorObject>(resultValue.Value);
            Assert.Equal("Error creating an experience", returnedObj.Error);
            Assert.Equal("POST", returnedObj.Method);
            Assert.Equal("/api/experiences", returnedObj.At);
            Assert.Equal(500, returnedObj.StatusCode);
        }

        [Fact]
        public async void CreateOneExperience_CreatedAtAction()
        {
            // arrange
            mockRepo.Setup(x => x.GetCategoryByNameAsync(It.IsAny<string>())).ReturnsAsync(() => new Category());
            mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(() => true);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("profileId", Guid.NewGuid().ToString())
            }, "mock"));

            var exprToCreate = new NewExperienceDto()
            {
                Address = "testing"
            };

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // act
            var result = await controller.CreateOneExperience(exprToCreate);

            // assert
            var resultValue = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            var returnedObj = Assert.IsType<ViewOneExperienceDto>(resultValue.Value);
            Assert.Equal("testing", returnedObj.Address);
        }


        private List<Experience> GetAllAsyncTest()
        {
            var experiences = new List<Experience>();
            experiences.Add(new Experience()
            {
                Id = Guid.NewGuid(),
                Name = "testing",
                Description = "purely for testing",
                City = "Helsingør",
                Address = "Gadenavn 1",
                Category = new Category() { Id = Guid.NewGuid(), Name = "Musik"},
                Pictures = new List<Picture>() { new Picture() { Path = "test1.jpg" } }
            });
            experiences.Add(new Experience()
            {
                Id = Guid.NewGuid(),
                Name = "testing2",
                Description = "purely for testing2",
                City = "Køvenhavn",
                Address = "Gadenavn 2",
                Category = new Category() { Id = Guid.NewGuid(), Name = "Musik" },
                Pictures = new List<Picture>() { new Picture() { Path = "test1.jpg" } }
            });
            return experiences;
        }

        private Experience GetOneAsyncTest()
        {
            return new Experience()
            {
                Id = Guid.Parse("506599C8-EF5A-4D3F-8AE2-47B44D04A792"),
                Name = "testing2",
                Description = "purely for testing2",
                City = "Køvenhavn",
                Address = "Gadenavn 2",
                Category = new Category() { Id = Guid.NewGuid(), Name = "Musik" },
                Pictures = new List<Picture>() { new Picture() { Path = "test1.jpg" } }
            };
        }

    }
}
