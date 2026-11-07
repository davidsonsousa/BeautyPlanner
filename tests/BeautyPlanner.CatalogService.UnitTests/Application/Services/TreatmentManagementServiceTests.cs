namespace BeautyPlanner.CatalogService.UnitTests.Application.Services;

public class TreatmentManagementServiceTests
{
    private readonly Mock<ITreatmentRepository> _treatmentRepositoryMock;
    private readonly Mock<IRepository<TreatmentCategory>> _treatmentCategoryRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly TreatmentManagementService _sut;

    public TreatmentManagementServiceTests()
    {
        _treatmentRepositoryMock = new Mock<ITreatmentRepository>();
        _treatmentCategoryRepositoryMock = new Mock<IRepository<TreatmentCategory>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new TreatmentManagementService(_treatmentRepositoryMock.Object, _treatmentCategoryRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Trait("Category", "Create")]
    [Fact]
    public async Task CreateTreatmentAsync_Should_CreateTreatment_When_ModelIsValid()
    {
        // Arrange
        var model = new TreatmentBuilder().BuildCreateModel();

        var sequence = new MockSequence();

        _treatmentCategoryRepositoryMock.InSequence(sequence)
                                        .Setup(x => x.GetByVanityIdAsync(model.TreatmentCategoryId))
                                        .ReturnsAsync(new TreatmentCategoryBuilder().Build());

        _treatmentRepositoryMock.InSequence(sequence)
                                .Setup(x => x.AddAsync(It.IsAny<Treatment>()))
                                .Returns(Task.CompletedTask);

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.CreateTreatmentAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(model.Name, result.Value.Name);
        Assert.Equal(model.Description, result.Value.Description);
        Assert.Equal(model.Price, result.Value.Price);
        Assert.Equal(model.DurationInMinutes, result.Value.DurationInMinutes);

        _treatmentCategoryRepositoryMock.Verify(x => x.GetByVanityIdAsync(model.TreatmentCategoryId), Times.Once);
        _treatmentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Treatment>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateTreatmentAsync_Should_UpdateTreatment_When_ModelIsValid()
    {
        // Arrange
        var model = new TreatmentBuilder().BuildUpdateModel();

        var sequence = new MockSequence();

        _treatmentRepositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(new TreatmentBuilder().Build());

        _treatmentRepositoryMock.InSequence(sequence)
                       .Setup(x => x.Update(It.Is<Treatment>(t => t.Name == model.Name
                                                            && t.Description == model.Description)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateTreatmentAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        _treatmentRepositoryMock.Verify(x => x.Update(It.Is<Treatment>(t => t.Name == model.Name
                                                             && t.Description == model.Description)
                                            ), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateTreatmentAsync_Should_ThrowNotFoundException_When_TreatmentNotFound()
    {
        // Arrange
        var model = new TreatmentBuilder().BuildUpdateModel();
        _treatmentRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Treatment?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.UpdateTreatmentAsync(model));
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteTreatmentAsync_Should_DeleteTreatment_When_TreatmentFound()
    {
        // Arrange
        var model = new TreatmentBuilder().Build();

        var sequence = new MockSequence();

        _treatmentRepositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(model);

        _treatmentRepositoryMock.InSequence(sequence)
                       .Setup(x => x.Delete(It.Is<Treatment>(t => t.VanityId == model.VanityId)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        await _sut.DeleteTreatmentAsync(model.VanityId);

        // Assert
        _treatmentRepositoryMock.Verify(x => x.Delete(It.Is<Treatment>(t => t.VanityId == model.VanityId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteTreatmentAsync_Should_ThrowNotFoundException_When_TreatmentNotFound()
    {
        // Arrange
        var model = new TreatmentBuilder().Build();
        _treatmentRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Treatment?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteTreatmentAsync(model.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentAsyncById_Should_ReturnTreatment_When_TreatmentFound()
    {
        // Arrange
        var treatment = new TreatmentBuilder().Build();
        _treatmentRepositoryMock.Setup(x => x.GetByIdAsync(treatment.Id)).ReturnsAsync(treatment);

        // Act
        var result = await _sut.GetTreatmentAsync(treatment.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(treatment.Name, result.Value.Name);

        _treatmentRepositoryMock.Verify(x => x.GetByIdAsync(treatment.Id), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentAsyncById_Should_ThrowNotFoundException_When_TreatmentNotFound()
    {
        // Arrange
        var treatment = new TreatmentBuilder().Build();
        _treatmentRepositoryMock.Setup(x => x.GetByIdAsync(treatment.Id)).ReturnsAsync((Treatment?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetTreatmentAsync(treatment.Id));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentAsyncByVanityId_Should_ReturnTreatment_When_TreatmentFound()
    {
        // Arrange
        var treatment = new TreatmentBuilder().Build();
        _treatmentRepositoryMock.Setup(x => x.GetByVanityIdAsync(treatment.VanityId)).ReturnsAsync(treatment);

        // Act
        var result = await _sut.GetTreatmentAsync(treatment.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(treatment.Name, result.Value.Name);

        _treatmentRepositoryMock.Verify(x => x.GetByVanityIdAsync(treatment.VanityId), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentAsyncByVanityId_Should_ThrowNotFoundException_When_TreatmentNotFound()
    {
        // Arrange
        var treatment = new TreatmentBuilder().Build();
        _treatmentRepositoryMock.Setup(x => x.GetByVanityIdAsync(treatment.VanityId)).ReturnsAsync((Treatment?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetTreatmentAsync(treatment.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentsAsync_Should_ReturnListOfTreatments_When_TreatmentsFound()
    {
        // Arrange
        var treatments = new List<Treatment>
        {
            new TreatmentBuilder().WithName("Treatment 1").Build(),
            new TreatmentBuilder().WithName("Treatment 2").Build()
        };
        _treatmentRepositoryMock.Setup(x => x.ListWithTreatmentCategoriesAsync()).ReturnsAsync(treatments);

        // Act
        var result = await _sut.GetTreatmentsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
                          x => Assert.Equal("Treatment 1", x.Name),
                          x => Assert.Equal("Treatment 2", x.Name));

        _treatmentRepositoryMock.Verify(x => x.ListWithTreatmentCategoriesAsync(), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetTreatmentsAsync_Should_ReturnEmpty_When_TreatmentsNotFound()
    {
        // Arrange
        _treatmentRepositoryMock.Setup(x => x.ListWithTreatmentCategoriesAsync()).ReturnsAsync(new List<Treatment>());

        // Act
        var result = await _sut.GetTreatmentsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);

        _treatmentRepositoryMock.Verify(x => x.ListWithTreatmentCategoriesAsync(), Times.Once);
    }
}
