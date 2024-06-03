using MassTransit;
using Moq;
using Tennis.Dto.Events;
using Tennis.Dto.Requests;
using Tennis.Repositories;
using Tennis.Services;

namespace Tennis.Test.Unit.Services;

public class MatchServiceTests
{
    private readonly Mock<IBus> _busMock;
    private readonly Mock<IMatchRepository> _matchRepositoryMock;
    private readonly MatchService _matchService;

    public MatchServiceTests()
    {
        _busMock = new Mock<IBus>();
        _matchRepositoryMock = new Mock<IMatchRepository>();
        _matchService = new MatchService(_busMock.Object, _matchRepositoryMock.Object);
    }

    [Fact]
    public async Task StartMatchAsync_ShouldStartMatch()
    {
        // Arrange
        var startMatchRequest = new StartMatchRequest
        {
            Name = "Test Match",
            FirstPlayerExperience = 3,
            SecondPlayerExperience = 2
        };

        _matchRepositoryMock.Setup(repo => repo.IsMatchExistAsync(It.IsAny<string>())).ReturnsAsync(false);

        // Act
        await _matchService.StartMatchAsync(startMatchRequest);

        // Assert
        _matchRepositoryMock.Verify(repo => repo.SaveMatchAsync(It.IsAny<Entities.Match>()), Times.Once);
        _busMock.Verify(bus => bus.Publish(It.IsAny<PlayGameEvent>(), default), Times.Once);
    }

    [Fact]
    public async Task GetMatchProgressAsync_ShouldReturnMatchProgress()
    {
        // Arrange
        var matchName = "Test Match";

        _matchRepositoryMock.Setup(repo => repo.GetMatchAsync(It.IsAny<string>())).ReturnsAsync(Entities.Match.Create(matchName, Entities.Player.Create(3), Entities.Player.Create(2)));

        // Act
        var result = await _matchService.GetMatchProgressAsync(matchName);

        // Assert
        Assert.NotNull(result);
        _matchRepositoryMock.Verify(repo => repo.GetMatchAsync(It.IsAny<string>()), Times.Once);
    }
}