namespace BeautyPlanner.TenantService.UnitTests.Application.Services;

public class SalonManagementServiceTests
{
    private readonly Mock<IRepository<Salon>> _salonRepositoryMock;
    private readonly Mock<IRepository<Tenant>> _tenantRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly SalonManagementService _sut;

    public SalonManagementServiceTests()
    {
        _salonRepositoryMock = new Mock<IRepository<Salon>>();
        _tenantRepositoryMock = new Mock<IRepository<Tenant>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new SalonManagementService(_salonRepositoryMock.Object, _tenantRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Trait("Category", "Create")]
    [Fact]
    public async Task CreateSalonAsync_Should_CreateSalon_When_ModelIsValid()
    {
        // Arrange
        var model = new SalonBuilder().BuildCreateModel();

        var sequence = new MockSequence();

        _tenantRepositoryMock.InSequence(sequence)
                             .Setup(x => x.GetByVanityIdAsync(model.TenantId))
                             .ReturnsAsync(new TenantBuilder().Build());

        _salonRepositoryMock.InSequence(sequence)
                       .Setup(x => x.AddAsync(It.IsAny<Salon>()))
                       .Returns(Task.CompletedTask);

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.CreateSalonAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(model.Name, result.Value.Name);
        Assert.Equal(model.Description, result.Value.Description);
        Assert.Equal(model.Email, result.Value.Email);

        _tenantRepositoryMock.Verify(x => x.GetByVanityIdAsync(model.TenantId), Times.Once);
        _salonRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Salon>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateSalonAsync_Should_UpdateSalon_When_ModelIsValid()
    {
        // Arrange
        var model = new SalonBuilder().BuildUpdateModel();

        var sequence = new MockSequence();

        _salonRepositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(new SalonBuilder().Build());

        _salonRepositoryMock.InSequence(sequence)
                       .Setup(x => x.Update(It.Is<Salon>(t => t.Name == model.Name
                                                            && t.Description == model.Description)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateSalonAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        _salonRepositoryMock.Verify(x => x.Update(It.Is<Salon>(t => t.Name == model.Name
                                                             && t.Description == model.Description)
                                            ), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateSalonAsync_Should_ThrowNotFoundException_When_SalonNotFound()
    {
        // Arrange
        var model = new SalonBuilder().BuildUpdateModel();
        _salonRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Salon?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.UpdateSalonAsync(model));
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteSalonAsync_Should_DeleteSalon_When_SalonFound()
    {
        // Arrange
        var model = new SalonBuilder().Build();

        var sequence = new MockSequence();

        _salonRepositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(model);

        _salonRepositoryMock.InSequence(sequence)
                       .Setup(x => x.Delete(It.Is<Salon>(t => t.VanityId == model.VanityId)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        await _sut.DeleteSalonAsync(model.VanityId);

        // Assert
        _salonRepositoryMock.Verify(x => x.Delete(It.Is<Salon>(t => t.VanityId == model.VanityId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteSalonAsync_Should_ThrowNotFoundException_When_SalonNotFound()
    {
        // Arrange
        var model = new SalonBuilder().Build();
        _salonRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Salon?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteSalonAsync(model.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetSalonAsyncById_Should_ReturnSalon_When_SalonFound()
    {
        // Arrange
        var salon = new SalonBuilder().Build();
        _salonRepositoryMock.Setup(x => x.GetByIdAsync(salon.Id)).ReturnsAsync(salon);

        // Act
        var result = await _sut.GetSalonAsync(salon.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(salon.Name, result.Value.Name);

        _salonRepositoryMock.Verify(x => x.GetByIdAsync(salon.Id), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetSalonAsyncById_Should_ThrowNotFoundException_When_SalonNotFound()
    {
        // Arrange
        var salon = new SalonBuilder().Build();
        _salonRepositoryMock.Setup(x => x.GetByIdAsync(salon.Id)).ReturnsAsync((Salon?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetSalonAsync(salon.Id));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetSalonAsyncByVanityId_Should_ReturnSalon_When_SalonFound()
    {
        // Arrange
        var salon = new SalonBuilder().Build();
        _salonRepositoryMock.Setup(x => x.GetByVanityIdAsync(salon.VanityId)).ReturnsAsync(salon);

        // Act
        var result = await _sut.GetSalonAsync(salon.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(salon.Name, result.Value.Name);

        _salonRepositoryMock.Verify(x => x.GetByVanityIdAsync(salon.VanityId), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetSalonAsyncByVanityId_Should_ThrowNotFoundException_When_SalonNotFound()
    {
        // Arrange
        var salon = new SalonBuilder().Build();
        _salonRepositoryMock.Setup(x => x.GetByVanityIdAsync(salon.VanityId)).ReturnsAsync((Salon?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetSalonAsync(salon.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetSalonsAsync_Should_ReturnListOfSalons_When_SalonsFound()
    {
        // Arrange
        var salons = new List<Salon>
        {
            new SalonBuilder().WithName("Salon 1").Build(),
            new SalonBuilder().WithName("Salon 2").Build()
        };
        _salonRepositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(salons);

        // Act
        var result = await _sut.GetSalonsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
                          x => Assert.Equal("Salon 1", x.Name),
                          x => Assert.Equal("Salon 2", x.Name));

        _salonRepositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetSalonsAsync_Should_ReturnEmpty_When_SalonsNotFound()
    {
        // Arrange
        _salonRepositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(new List<Salon>());

        // Act
        var result = await _sut.GetSalonsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);

        _salonRepositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }
}
