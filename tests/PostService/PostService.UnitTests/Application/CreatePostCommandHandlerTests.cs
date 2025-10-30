namespace PostService.UnitTests.Application
{
    public class CreatePostCommandHandlerTests
    {
        [Fact]
        public async Task Should_Create_Post_When_Data_Is_Valid()
        {
            // Arrange
            var mockRepo = new Mock<IPostRepository>();
            var handler = new CreatePostCommandHandler(mockRepo.Object);
            var command = new CreatePostCommand("Заголовок", "Текст", authorId: Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Заголовок");
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Post>()), Times.Once);
        }
    }
}
