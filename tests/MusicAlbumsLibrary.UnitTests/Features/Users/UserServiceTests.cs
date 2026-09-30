using Moq;
using MusicAlbumsLibrary.Application.Features.Users;
using MusicAlbumsLibrary.Core.Contracts;
using MusicAlbumsLibrary.Core.Entities;

namespace MusicAlbumsLibrary.UnitTests.Features.Users;

public class UserServiceTests
{
    private readonly Mock<IMusicAlbumsLibraryRepository> _musicAlbumsLibraryRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _musicAlbumsLibraryRepositoryMock = new Mock<IMusicAlbumsLibraryRepository>();
        _userService = new UserService(_musicAlbumsLibraryRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateNewUser_WhenUserNameIsGiven()
    {
        // Arrange
        var user = new User
        {
            Name = "Test User 1"
        };

        // Act
        var result = await _userService.CreateUserAsync(user.Name, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Name, result.Name);

        _musicAlbumsLibraryRepositoryMock.Verify(
                x => x.CreateUserAsync(
                    It.Is<User>(
                        s => s.Name == user.Name
                    ),
                    It.IsAny<CancellationToken>()), Times.Once);

        _musicAlbumsLibraryRepositoryMock.Verify(
                x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()), Times.Once);
    }
}
