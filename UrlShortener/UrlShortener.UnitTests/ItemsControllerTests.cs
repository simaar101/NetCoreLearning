using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Dtos;
using UrlShortener.Api.Models;
using UrlShortener.Api.Repository;
using UrlShortener.Api.Logic;
using UrlShortener.Api.Controllers;
using Xunit;
using Moq;
using FluentAssertions;


namespace UrlShortener.UnitTests
{
    public class ItemsControllerTests
    {
        private readonly  Mock<IUrlRepo> repositoryStub = new();
        private readonly  Mock<IMapper> mapperStub = new();
        private readonly  Mock<IUrlGenerator> UrlGeneratorStub = new();

        //UnitOfWork_StateUnderTest_ExpectedBehavior
        [Fact]
        public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetUrlAsync(It.IsAny<Guid>())).ReturnsAsync((Url)null);
            var controller = new UrlController(repositoryStub.Object, mapperStub.Object, UrlGeneratorStub.Object);

            //Act
            var result = await controller.GetUrlAsync(Guid.NewGuid());
        
            //Assert
            //Assert.IsType<NotFoundResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        //UnitOfWork_StateUnderTest_ExpectedBehavior    
        [Fact]
        public async Task GetItemAsync_ExistingItem_ReturnsExpectedItem()
        {
            // Arrange

            var expectedUrl = CreateRandomUrl();
            UrlDto dtoURL = new UrlDto
            {
                CreatedDate = expectedUrl.CreatedDate,
                HashFunction = expectedUrl.HashFunction,
                ShortNameUrl = expectedUrl.ShortNameUrl,
                LongNameUrl = expectedUrl.LongNameUrl,
                Id = expectedUrl.Id
            };

            mapperStub.Setup(mapper => mapper.Map<UrlDto>(It.IsAny<Url>())).Returns(dtoURL);


            repositoryStub.Setup(repo => repo.GetUrlAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUrl);
 
            var controller = new UrlController(repositoryStub.Object, mapperStub.Object, UrlGeneratorStub.Object);
  
            //Act
            var result = await controller.GetUrlAsync(Guid.NewGuid());
       
            //Assert
            result.Value.Should().BeEquivalentTo(expectedUrl);
            result.Value.Should().BeEquivalentTo(dtoURL);
            // UrlDto naderino = (UrlDto) result.Value;
            // naderino.ShortNameUrl.Should().BeEquivalentTo(expectedUrl.ShortNameUrl);
            Assert.IsType<UrlDto>(result.Value);
            Assert.Equal(result.Value.ShortNameUrl, expectedUrl.ShortNameUrl);
        }

        //UnitOfWork_StateUnderTest_ExpectedBehavior
        [Fact]
        public async Task GetItemsAsync_WithUnexistingItem_ReturnsNotFound()
        {
            //Arrange
            repositoryStub.Setup(repo => repo.GetUrlsAsync()).ReturnsAsync((IEnumerable < Url > )null);
            // repositoryStub.Setup(repo => repo.GetUrlAsync(It.IsAny<Guid>())).ReturnsAsync((Url)null);
            var controller = new UrlController(repositoryStub.Object, mapperStub.Object, UrlGeneratorStub.Object);

            //Act
            var result = await controller.GetUrlsAsync();

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundResult>();

        }
        [Fact]
        //UnitOfWork_StateUnderTest_ExpectedBehavior
        public async Task GetItemsAsync_WithItemsInRepo_ReturnOkResult()
        {
            //Arrange
            var expectectedListOfUrl = new List<Url>();
            var expectectedListOfUrlDto = new List<UrlDto>();
            for (int i = 0; i < 2; i++)
            {
                var expectedUrl = CreateRandomUrl();
                expectectedListOfUrl.Add(expectedUrl);
                UrlDto dtoURL = new UrlDto
                {
                    CreatedDate = expectedUrl.CreatedDate,
                    HashFunction = expectedUrl.HashFunction,
                    ShortNameUrl = expectedUrl.ShortNameUrl,
                    LongNameUrl = expectedUrl.LongNameUrl,
                    Id = expectedUrl.Id
                };
                expectectedListOfUrlDto.Add(dtoURL);
            }

            mapperStub.Setup(mapper => mapper.Map<IEnumerable<UrlDto>>(It.IsAny<IEnumerable<Url>>())).Returns(expectectedListOfUrlDto);

            repositoryStub.Setup(repo => repo.GetUrlsAsync()).ReturnsAsync(expectectedListOfUrl);

            var controller = new UrlController(repositoryStub.Object, mapperStub.Object, UrlGeneratorStub.Object);

            //Act
            var result = await controller.GetUrlsAsync();
      
            //Assert
            //result.Value.Should().BeEquivalentTo(expectectedListOfUrl);
            //result.Value.Should().BeEquivalentTo(expectectedListOfUrlDto);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        //UnitOfWork_StateUnderTest_ExpectedBehavior
        [Fact]
        public async Task DeleteUrlAsync_WithUnexistingItem_ReturnsNoContent()
        {
            // Arrange
            var expectedUrl = CreateRandomUrl();
      
            repositoryStub.Setup(repo => repo.GetUrlAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUrl);

            var controller = new UrlController(repositoryStub.Object, mapperStub.Object, UrlGeneratorStub.Object);

            //Act
            var result = await controller.DeleteUrlAsync(expectedUrl.Id);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }
        //UnitOfWork_StateUnderTest_ExpectedBehavior
        [Fact]
        public async Task UpdateUrlAsync_WithUnexistingItem_ReturnsNoContent()
        {
            // Arrange
            var expectedUrl = CreateRandomUrl();

            repositoryStub.Setup(repo => repo.GetUrlAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUrl);

            var controller = new UrlController(repositoryStub.Object, mapperStub.Object, UrlGeneratorStub.Object);

            UpdateUrlDto updatedUrl = new()
            {
                ShortNameUrl = Guid.NewGuid().ToString(),
                LongNameUrl = Guid.NewGuid().ToString(),
            };
            //Act
            var result = await controller.UpdateUrlAsync(expectedUrl.Id, updatedUrl);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }
        //UnitOfWork_StateUnderTest_ExpectedBehavior    
        [Fact]
        public async Task CreatedUrlAsync_WithUrlToCreate_ReturnsCreatedUrl()
        {
            // Arrange
            var expectedUrl = CreateRandomUrl();
            UrlDto dtoURL = new UrlDto
            {
                CreatedDate = expectedUrl.CreatedDate,
                HashFunction = expectedUrl.HashFunction,
                ShortNameUrl = expectedUrl.ShortNameUrl,
                LongNameUrl = expectedUrl.LongNameUrl,
                Id = expectedUrl.Id
            };
            mapperStub.Setup(mapper => mapper.Map<UrlDto>(It.IsAny<Url>())).Returns(dtoURL);
            UrlGeneratorStub.Setup(urlGen => urlGen.GetHashFunction(It.IsAny<string>())).Returns(dtoURL.HashFunction);

            var controller = new UrlController(repositoryStub.Object, mapperStub.Object, UrlGeneratorStub.Object);
           
            CreateUrlDto newUrl = new()
            {
                LongNameUrl = Guid.NewGuid().ToString(),
            };
            //Act
            var result = await controller.CreateUrlAsync(newUrl);

            var test = (result.Result as CreatedAtActionResult).Value;

            //Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            test.Should().BeEquivalentTo(dtoURL);
        }
        private Url CreateRandomUrl()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ShortNameUrl = Guid.NewGuid().ToString(),
                LongNameUrl = Guid.NewGuid().ToString(),
                HashFunction = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
