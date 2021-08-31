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
            // UrlDto naderino = (UrlDto) result.Value;
            // naderino.ShortNameUrl.Should().BeEquivalentTo(expectedUrl.ShortNameUrl);
            Assert.IsType<UrlDto>(result.Value);
             Assert.Equal(result.Value.ShortNameUrl, expectedUrl.ShortNameUrl);
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
