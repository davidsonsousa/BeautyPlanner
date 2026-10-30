namespace BeautyPlanner.TenantService.UnitTests.Application.Services;

public class TenantManagementServiceTests
{
    private readonly Mock<IRepository<Tenant>> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly TenantManagementService _sut;

    public TenantManagementServiceTests()
    {
        _repositoryMock = new Mock<IRepository<Tenant>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new TenantManagementService(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateTenantAsync_Should_CreateTenant_When_ModelIsValid()
    {
        // Arrange
        var model = new TenantBuilder().BuildCreateModel();

        var sequence = new MockSequence();

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.AddAsync(It.IsAny<Tenant>()))
                       .Returns(Task.CompletedTask);

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.CreateTenantAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(model.Name, result.Value.Name);
        Assert.Equal(model.Description, result.Value.Description);

        _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Tenant>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateTenantAsync_Should_UpdateTenant_When_ModelIsValid()
    {
        // Arrange
        var model = new TenantBuilder().BuildUpdateModel();

        var sequence = new MockSequence();

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(new TenantBuilder().Build());

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.Update(It.Is<Tenant>(t => t.Name == model.Name
                                                            && t.Description == model.Description)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateTenantAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        _repositoryMock.Verify(x => x.Update(It.Is<Tenant>(t => t.Name == model.Name
                                                             && t.Description == model.Description)
                                            ), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateTenantAsync_Should_ThrowNotFoundException_When_TenantNotFound()
    {
        // Arrange
        var model = new TenantBuilder().BuildUpdateModel();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Tenant?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.UpdateTenantAsync(model));
    }

    [Fact]
    public async Task DeleteTenantAsync_Should_DeleteTenant_When_TenantFound()
    {
        // Arrange
        var model = new TenantBuilder().Build();

        var sequence = new MockSequence();

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                       .ReturnsAsync(model);

        _repositoryMock.InSequence(sequence)
                       .Setup(x => x.Delete(It.Is<Tenant>(t => t.VanityId == model.VanityId)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        await _sut.DeleteTenantAsync(model.VanityId);

        // Assert
        _repositoryMock.Verify(x => x.Delete(It.Is<Tenant>(t => t.VanityId == model.VanityId)),Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteTenantAsync_Should_ThrowNotFoundException_When_TenantNotFound()
    {
        // Arrange
        var model = new TenantBuilder().Build();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((Tenant?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteTenantAsync(model.VanityId));
    }

    [Fact]
    public async Task GetTenantAsyncById_Should_ReturnTenant_When_TenantFound()
    {
        // Arrange
        var tenant = new TenantBuilder().Build();
        _repositoryMock.Setup(x => x.GetByIdAsync(tenant.Id)).ReturnsAsync(tenant);

        // Act
        var result = await _sut.GetTenantAsync(tenant.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(tenant.Name, result.Value.Name);

        _repositoryMock.Verify(x => x.GetByIdAsync(tenant.Id), Times.Once);
    }

    [Fact]
    public async Task GetTenantAsyncById_Should_ThrowNotFoundException_When_TenantNotFound()
    {
        // Arrange
        var tenant = new TenantBuilder().Build();
        _repositoryMock.Setup(x => x.GetByIdAsync(tenant.Id)).ReturnsAsync((Tenant?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetTenantAsync(tenant.Id));
    }

    [Fact]
    public async Task GetTenantAsyncByVanityId_Should_ReturnTenant_When_TenantFound()
    {
        // Arrange
        var tenant = new TenantBuilder().Build();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(tenant.VanityId)).ReturnsAsync(tenant);

        // Act
        var result = await _sut.GetTenantAsync(tenant.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(tenant.Name, result.Value.Name);

        _repositoryMock.Verify(x => x.GetByVanityIdAsync(tenant.VanityId), Times.Once);
    }

    [Fact]
    public async Task GetTenantAsyncByVanityId_Should_ThrowNotFoundException_When_TenantNotFound()
    {
        // Arrange
        var tenant = new TenantBuilder().Build();
        _repositoryMock.Setup(x => x.GetByVanityIdAsync(tenant.VanityId)).ReturnsAsync((Tenant?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetTenantAsync(tenant.VanityId));
    }

    [Fact]
    public async Task GetTenantsAsync_Should_ReturnListOfTenants_When_TenantsFound()
    {
        // Arrange
        var tenants = new List<Tenant>
        {
            new TenantBuilder().WithName("Tenant 1").Build(),
            new TenantBuilder().WithName("Tenant 2").Build()
        };
        _repositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(tenants);

        // Act
        var result = await _sut.GetTenantsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
                          x => Assert.Equal("Tenant 1", x.Name),
                          x => Assert.Equal("Tenant 2", x.Name));

        _repositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }

    [Fact]
    public async Task GetTenantsAsync_Should_ReturnEmpty_When_TenantsNotFound()
    {
        // Arrange
        _repositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(new List<Tenant>());

        // Act
        var result = await _sut.GetTenantsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);

        _repositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }
}
