namespace BeautyPlanner.StaffService.UnitTests.Application.Services;

public class ProfessionManagementServiceTests
{
    private readonly Mock<IRepository<Profession>> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ProfessionManagementService _sut;

    public ProfessionManagementServiceTests()
    {
        _repositoryMock = new Mock<IRepository<Profession>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new ProfessionManagementService(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Trait("Category", "Create")]
    [Fact]
    public async Task CreateProfessionAsync_Should_CreateProfession_When_ModelIsValid()
    {
        // Arrange
        var model = new ProfessionBuilder().BuildCreateModel();

        var sequence = new MockSequence();

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.AddAsync(It.IsAny<Profession>()))
                       .Returns(Task.CompletedTask);

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.CreateProfessionAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(model.Name, result.Value.Name);
        Assert.Equal(model.Description, result.Value.Description);

        _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Profession>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateProfessionAsync_Should_UpdateProfession_When_ModelIsValid()
    {
        // Arrange
        var model = new ProfessionBuilder().BuildUpdateModel();

        var sequence = new MockSequence();

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(new ProfessionBuilder().Build());

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.Update(It.Is<Profession>(t => t.Name == model.Name
                                                            && t.Description == model.Description)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateProfessionAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        _repositoryMock.Verify(x => x.Update(It.Is<Profession>(t => t.Name == model.Name
                                                             && t.Description == model.Description)
                                            ), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateProfessionAsync_Should_ThrowNotFoundException_When_ProfessionNotFound()
    {
        // Arrange
        var model = new ProfessionBuilder().BuildUpdateModel();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Profession?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.UpdateProfessionAsync(model));
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteProfessionAsync_Should_DeleteProfession_When_ProfessionFound()
    {
        // Arrange
        var model = new ProfessionBuilder().Build();

        var sequence = new MockSequence();

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(model);

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.Delete(It.Is<Profession>(t => t.VanityId == model.VanityId)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        await _sut.DeleteProfessionAsync(model.VanityId);

        // Assert
        _repositoryMock.Verify(x => x.Delete(It.Is<Profession>(t => t.VanityId == model.VanityId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteProfessionAsync_Should_ThrowNotFoundException_When_ProfessionNotFound()
    {
        // Arrange
        var model = new ProfessionBuilder().Build();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Profession?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteProfessionAsync(model.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetProfessionAsyncById_Should_ReturnProfession_When_ProfessionFound()
    {
        // Arrange
        var profession = new ProfessionBuilder().Build();
        _repositoryMock.Setup(x => x.GetByIdAsync(profession.Id)).ReturnsAsync(profession);

        // Act
        var result = await _sut.GetProfessionAsync(profession.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(profession.Name, result.Value.Name);

        _repositoryMock.Verify(x => x.GetByIdAsync(profession.Id), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetProfessionAsyncById_Should_ThrowNotFoundException_When_ProfessionNotFound()
    {
        // Arrange
        var profession = new ProfessionBuilder().Build();
        _repositoryMock.Setup(x => x.GetByIdAsync(profession.Id)).ReturnsAsync((Profession?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetProfessionAsync(profession.Id));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetProfessionAsyncByVanityId_Should_ReturnProfession_When_ProfessionFound()
    {
        // Arrange
        var profession = new ProfessionBuilder().Build();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(profession.VanityId)).ReturnsAsync(profession);

        // Act
        var result = await _sut.GetProfessionAsync(profession.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(profession.Name, result.Value.Name);

        _repositoryMock.Verify(x => x.GetByVanityIdAsync(profession.VanityId), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetProfessionAsyncByVanityId_Should_ThrowNotFoundException_When_ProfessionNotFound()
    {
        // Arrange
        var profession = new ProfessionBuilder().Build();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(profession.VanityId)).ReturnsAsync((Profession?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetProfessionAsync(profession.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetProfessionsAsync_Should_ReturnListOfProfessions_When_ProfessionsFound()
    {
        // Arrange
        var professions = new List<Profession>
        {
            new ProfessionBuilder().WithName("Profession 1").Build(),
            new ProfessionBuilder().WithName("Profession 2").Build()
        };
        _repositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(professions);

        // Act
        var result = await _sut.GetProfessionsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
                          x => Assert.Equal("Profession 1", x.Name),
                          x => Assert.Equal("Profession 2", x.Name));

        _repositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetProfessionsAsync_Should_ReturnEmpty_When_ProfessionsNotFound()
    {
        // Arrange
        _repositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(new List<Profession>());

        // Act
        var result = await _sut.GetProfessionsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);

        _repositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }
}