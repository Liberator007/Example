using MassTransit;
using PostService.Application.Commands;
using PostService.Application.Commands.Handler;
using PostService.Domain.Models;
using PostService.Domain.Services;

namespace PostService.UnitTests.Application
{
    public class CreatePostCommandHandlerTests
    {
        [Fact]
        public async Task Should_Create_Post_When_Data_Is_Valid()
        {
            // Arrange
            var mockRepository = new Mock<IPostRepository>();
            var mockPublish = new Mock<IPublishEndpoint>();
            var handler = new CreatePostCommandHandler(mockRepository.Object, mockPublish.Object);
            var command = new CreatePostCommand("Заголовок", "Текст", AuthorId: Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Заголовок");
            mockRepository.Verify(r => r.CreateAsync(It.IsAny<Post>()), Times.Once);
        }
    }
}
