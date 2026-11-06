namespace BeautyPlanner.ClientService.UnitTests.Application.Services;

public class ClientManagementServiceTests
{
    private readonly Mock<IRepository<Client>> _clientRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ClientManagementService _sut;

    public ClientManagementServiceTests()
    {
        _clientRepositoryMock = new Mock<IRepository<Client>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new ClientManagementService(_clientRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Trait("Category", "Create")]
    [Fact]
    public async Task CreateClientAsync_Should_CreateClient_When_ModelIsValid()
    {
        // Arrange
        var model = new ClientBuilder().BuildCreateModel();

        var sequence = new MockSequence();

        _clientRepositoryMock.InSequence(sequence)
                             .Setup(x => x.AddAsync(It.IsAny<Client>()))
                             .Returns(Task.CompletedTask);

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.CreateClientAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(model.FirstName, result.Value.FirstName);
        Assert.Equal(model.LastName, result.Value.LastName);
        Assert.Equal(model.Email, result.Value.Email);
        Assert.NotNull(result.Value.Address);
        Assert.Equal(model.Address!.Line1, result.Value.Address.Line1);

        _clientRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Client>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateClientAsync_Should_UpdateClient_When_ModelIsValid()
    {
        // Arrange
        var model = new ClientBuilder().BuildUpdateModel();

        var sequence = new MockSequence();

        _clientRepositoryMock.InSequence(sequence)
                             .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                             .ReturnsAsync(new ClientBuilder().Build());

        _clientRepositoryMock.InSequence(sequence)
                             .Setup(x => x.Update(It.Is<Client>(t => t.FirstName == model.FirstName && t.LastName == model.LastName)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateClientAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        _clientRepositoryMock.Verify(x => x.Update(It.Is<Client>(t => t.FirstName == model.FirstName && t.LastName == model.LastName)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateClientAsync_Should_ThrowNotFoundException_When_ClientNotFound()
    {
        // Arrange
        var model = new ClientBuilder().BuildUpdateModel();
        _clientRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Client?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.UpdateClientAsync(model));
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteClientAsync_Should_DeleteClient_When_ClientFound()
    {
        // Arrange
        var model = new ClientBuilder().Build();

        var sequence = new MockSequence();

        _clientRepositoryMock.InSequence(sequence)
                             .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                             .ReturnsAsync(model);

        _clientRepositoryMock.InSequence(sequence)
                             .Setup(x => x.Delete(It.Is<Client>(t => t.VanityId == model.VanityId)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        await _sut.DeleteClientAsync(model.VanityId);

        // Assert
        _clientRepositoryMock.Verify(x => x.Delete(It.Is<Client>(t => t.VanityId == model.VanityId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteClientAsync_Should_ThrowNotFoundException_When_ClientNotFound()
    {
        // Arrange
        var model = new ClientBuilder().Build();
        _clientRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Client?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteClientAsync(model.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetClientAsyncById_Should_ReturnClient_When_ClientFound()
    {
        // Arrange
        var client = new ClientBuilder().Build();
        _clientRepositoryMock.Setup(x => x.GetByIdAsync(client.Id)).ReturnsAsync(client);

        // Act
        var result = await _sut.GetClientAsync(client.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(client.FirstName, result.Value.FirstName);
        Assert.Equal(client.LastName, result.Value.LastName);

        _clientRepositoryMock.Verify(x => x.GetByIdAsync(client.Id), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetClientAsyncById_Should_ThrowNotFoundException_When_ClientNotFound()
    {
        // Arrange
        var client = new ClientBuilder().Build();
        _clientRepositoryMock.Setup(x => x.GetByIdAsync(client.Id)).ReturnsAsync((Client?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetClientAsync(client.Id));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetClientAsyncByVanityId_Should_ReturnClient_When_ClientFound()
    {
        // Arrange
        var client = new ClientBuilder().Build();
        _clientRepositoryMock.Setup(x => x.GetByVanityIdAsync(client.VanityId)).ReturnsAsync(client);

        // Act
        var result = await _sut.GetClientAsync(client.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(client.FirstName, result.Value.FirstName);
        Assert.Equal(client.LastName, result.Value.LastName);

        _clientRepositoryMock.Verify(x => x.GetByVanityIdAsync(client.VanityId), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetClientAsyncByVanityId_Should_ThrowNotFoundException_When_ClientNotFound()
    {
        // Arrange
        var client = new ClientBuilder().Build();
        _clientRepositoryMock.Setup(x => x.GetByVanityIdAsync(client.VanityId)).ReturnsAsync((Client?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetClientAsync(client.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetClientsAsync_Should_ReturnListOfClients_When_ClientsFound()
    {
        // Arrange
        var clients = new List<Client>
        {
            new ClientBuilder().WithFirstName("Client 1").Build(),
            new ClientBuilder().WithFirstName("Client 2").Build()
        };
        _clientRepositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(clients);

        // Act
        var result = await _sut.GetClientsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
                          x => Assert.Equal("Client 1", x.FirstName),
                          x => Assert.Equal("Client 2", x.FirstName));

        _clientRepositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetClientsAsync_Should_ReturnEmpty_When_ClientsNotFound()
    {
        // Arrange
        _clientRepositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(new List<Client>());

        // Act
        var result = await _sut.GetClientsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);

        _clientRepositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }
}
