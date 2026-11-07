namespace BeautyPlanner.CatalogService.UnitTests.Application.Services;

public class TreatmentCategoryManagementServiceTests
{
    private readonly Mock<IRepository<TreatmentCategory>> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly TreatmentCategoryManagementService _sut;

    public TreatmentCategoryManagementServiceTests()
    {
        _repositoryMock = new Mock<IRepository<TreatmentCategory>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new TreatmentCategoryManagementService(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Trait("Category", "Create")]
    [Fact]
    public async Task CreateTreatmentCategoryAsync_Should_CreateTreatmentCategory_When_ModelIsValid()
    {
        // Arrange
        var model = new TreatmentCategoryBuilder().BuildCreateModel();

        var sequence = new MockSequence();

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.AddAsync(It.IsAny<TreatmentCategory>()))
                       .Returns(Task.CompletedTask);

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.CreateTreatmentCategoryAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(model.Name, result.Value.Name);
        Assert.Equal(model.Description, result.Value.Description);

        _repositoryMock.Verify(x => x.AddAsync(It.IsAny<TreatmentCategory>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateTreatmentCategoryAsync_Should_UpdateTreatmentCategory_When_ModelIsValid()
    {
        // Arrange
        var model = new TreatmentCategoryBuilder().BuildUpdateModel();

        var sequence = new MockSequence();

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(new TreatmentCategoryBuilder().Build());

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.Update(It.Is<TreatmentCategory>(t => t.Name == model.Name
                                                            && t.Description == model.Description)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateTreatmentCategoryAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        _repositoryMock.Verify(x => x.Update(It.Is<TreatmentCategory>(t => t.Name == model.Name
                                                             && t.Description == model.Description)
                                            ), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateTreatmentCategoryAsync_Should_ThrowNotFoundException_When_TreatmentCategoryNotFound()
    {
        // Arrange
        var model = new TreatmentCategoryBuilder().BuildUpdateModel();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((TreatmentCategory?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.UpdateTreatmentCategoryAsync(model));
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteTreatmentCategoryAsync_Should_DeleteTreatmentCategory_When_TreatmentCategoryFound()
    {
        // Arrange
        var model = new TreatmentCategoryBuilder().Build();

        var sequence = new MockSequence();

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(model);

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.Delete(It.Is<TreatmentCategory>(t => t.VanityId == model.VanityId)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        await _sut.DeleteTreatmentCategoryAsync(model.VanityId);

        // Assert
        _repositoryMock.Verify(x => x.Delete(It.Is<TreatmentCategory>(t => t.VanityId == model.VanityId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteTreatmentCategoryAsync_Should_ThrowNotFoundException_When_TreatmentCategoryNotFound()
    {
        // Arrange
        var model = new TreatmentCategoryBuilder().Build();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((TreatmentCategory?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteTreatmentCategoryAsync(model.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentCategoryAsyncById_Should_ReturnTreatmentCategory_When_TreatmentCategoryFound()
    {
        // Arrange
        var treatmentCategory = new TreatmentCategoryBuilder().Build();
        _repositoryMock.Setup(x => x.GetByIdAsync(treatmentCategory.Id)).ReturnsAsync(treatmentCategory);

        // Act
        var result = await _sut.GetTreatmentCategoryAsync(treatmentCategory.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(treatmentCategory.Name, result.Value.Name);

        _repositoryMock.Verify(x => x.GetByIdAsync(treatmentCategory.Id), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentCategoryAsyncById_Should_ThrowNotFoundException_When_TreatmentCategoryNotFound()
    {
        // Arrange
        var treatmentCategory = new TreatmentCategoryBuilder().Build();
        _repositoryMock.Setup(x => x.GetByIdAsync(treatmentCategory.Id)).ReturnsAsync((TreatmentCategory?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetTreatmentCategoryAsync(treatmentCategory.Id));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentCategoryAsyncByVanityId_Should_ReturnTreatmentCategory_When_TreatmentCategoryFound()
    {
        // Arrange
        var treatmentCategory = new TreatmentCategoryBuilder().Build();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(treatmentCategory.VanityId)).ReturnsAsync(treatmentCategory);

        // Act
        var result = await _sut.GetTreatmentCategoryAsync(treatmentCategory.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(treatmentCategory.Name, result.Value.Name);

        _repositoryMock.Verify(x => x.GetByVanityIdAsync(treatmentCategory.VanityId), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentCategoryAsyncByVanityId_Should_ThrowNotFoundException_When_TreatmentCategoryNotFound()
    {
        // Arrange
        var treatmentCategory = new TreatmentCategoryBuilder().Build();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(treatmentCategory.VanityId)).ReturnsAsync((TreatmentCategory?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetTreatmentCategoryAsync(treatmentCategory.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentCategorysAsync_Should_ReturnListOfTreatmentCategorys_When_TreatmentCategorysFound()
    {
        // Arrange
        var treatmentCategorys = new List<TreatmentCategory>
        {
            new TreatmentCategoryBuilder().WithName("TreatmentCategory 1").Build(),
            new TreatmentCategoryBuilder().WithName("TreatmentCategory 2").Build()
        };
        _repositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(treatmentCategorys);

        // Act
        var result = await _sut.GetTreatmentCategorysAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
                          x => Assert.Equal("TreatmentCategory 1", x.Name),
                          x => Assert.Equal("TreatmentCategory 2", x.Name));

        _repositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentCategorysAsync_Should_ReturnEmpty_When_TreatmentCategorysNotFound()
    {
        // Arrange
        _repositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(new List<TreatmentCategory>());

        // Act
        var result = await _sut.GetTreatmentCategorysAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);

        _repositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }
}
