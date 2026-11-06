namespace BeautyPlanner.AvailabilityPeriodService.UnitTests.Application.Services;

public class AvailabilityPeriodManagementServiceTests
{
    private readonly Mock<IAvailabilityPeriodRepository> _availabilityPeriodRepositoryMock;
    private readonly Mock<IStaffRepository> _staffRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AvailabilityPeriodManagementService _sut;

    public AvailabilityPeriodManagementServiceTests()
    {
        _availabilityPeriodRepositoryMock = new Mock<IAvailabilityPeriodRepository>();
        _staffRepositoryMock = new Mock<IStaffRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new AvailabilityPeriodManagementService(_availabilityPeriodRepositoryMock.Object, _staffRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Trait("Category", "Create")]
    [Fact]
    public async Task CreateAvailabilityPeriodAsync_Should_CreateAvailabilityPeriod_When_ModelIsValid()
    {
        // Arrange
        var model = new AvailabilityPeriodBuilder().BuildCreateModel();

        var sequence = new MockSequence();

        _staffRepositoryMock.InSequence(sequence)
                            .Setup(x => x.GetByVanityIdAsync(model.StaffMemberId))
                            .ReturnsAsync(new StaffMemberBuilder().Build());

        _availabilityPeriodRepositoryMock.InSequence(sequence)
                                         .Setup(x => x.AddAsync(It.IsAny<AvailabilityPeriod>()))
                                         .Returns(Task.CompletedTask);

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.CreateAvailabilityPeriodAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(model.DayOfWeek, result.Value.DayOfWeek);
        Assert.Equal(model.StartTime, result.Value.StartTime);
        Assert.Equal(model.EndTime, result.Value.EndTime);

        _staffRepositoryMock.Verify(x => x.GetByVanityIdAsync(model.StaffMemberId), Times.Once);
        _availabilityPeriodRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AvailabilityPeriod>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateAvailabilityPeriodAsync_Should_UpdateAvailabilityPeriod_When_ModelIsValid()
    {
        // Arrange
        var model = new AvailabilityPeriodBuilder().BuildUpdateModel();

        var sequence = new MockSequence();

        _availabilityPeriodRepositoryMock.InSequence(sequence)
                                         .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                                         .ReturnsAsync(new AvailabilityPeriodBuilder().Build());

        _availabilityPeriodRepositoryMock.InSequence(sequence)
                                         .Setup(x => x.Update(It.Is<AvailabilityPeriod>(t => t.DayOfWeek == model.DayOfWeek
                                                                                             && t.StartTime == model.StartTime
                                                                                             && t.EndTime == model.EndTime)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateAvailabilityPeriodAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        _availabilityPeriodRepositoryMock.Verify(x => x.Update(It.Is<AvailabilityPeriod>(t => t.DayOfWeek == model.DayOfWeek
                                                                                              && t.StartTime == model.StartTime
                                                                                              && t.EndTime == model.EndTime)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateAvailabilityPeriodAsync_Should_ThrowNotFoundException_When_AvailabilityPeriodNotFound()
    {
        // Arrange
        var model = new AvailabilityPeriodBuilder().BuildUpdateModel();
        _availabilityPeriodRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((AvailabilityPeriod?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.UpdateAvailabilityPeriodAsync(model));
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteAvailabilityPeriodAsync_Should_DeleteAvailabilityPeriod_When_AvailabilityPeriodFound()
    {
        // Arrange
        var model = new AvailabilityPeriodBuilder().Build();

        var sequence = new MockSequence();

        _availabilityPeriodRepositoryMock.InSequence(sequence)
                                         .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                                         .ReturnsAsync(model);

        _availabilityPeriodRepositoryMock.InSequence(sequence)
                                         .Setup(x => x.Delete(It.Is<AvailabilityPeriod>(t => t.VanityId == model.VanityId)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        await _sut.DeleteAvailabilityPeriodAsync(model.VanityId);

        // Assert
        _availabilityPeriodRepositoryMock.Verify(x => x.Delete(It.Is<AvailabilityPeriod>(t => t.VanityId == model.VanityId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteAvailabilityPeriodAsync_Should_ThrowNotFoundException_When_AvailabilityPeriodNotFound()
    {
        // Arrange
        var model = new AvailabilityPeriodBuilder().Build();
        _availabilityPeriodRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((AvailabilityPeriod?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteAvailabilityPeriodAsync(model.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetAvailabilityPeriodAsyncById_Should_ReturnAvailabilityPeriod_When_AvailabilityPeriodFound()
    {
        // Arrange
        var availabilityPeriod = new AvailabilityPeriodBuilder().Build();
        _availabilityPeriodRepositoryMock.Setup(x => x.GetByIdAsync(availabilityPeriod.Id)).ReturnsAsync(availabilityPeriod);

        // Act
        var result = await _sut.GetAvailabilityPeriodAsync(availabilityPeriod.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(availabilityPeriod.DayOfWeek, result.Value.DayOfWeek);
        Assert.Equal(availabilityPeriod.StartTime, result.Value.StartTime);
        Assert.Equal(availabilityPeriod.EndTime, result.Value.EndTime);

        _availabilityPeriodRepositoryMock.Verify(x => x.GetByIdAsync(availabilityPeriod.Id), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetAvailabilityPeriodAsyncById_Should_ThrowNotFoundException_When_AvailabilityPeriodNotFound()
    {
        // Arrange
        var availabilityPeriod = new AvailabilityPeriodBuilder().Build();
        _availabilityPeriodRepositoryMock.Setup(x => x.GetByIdAsync(availabilityPeriod.Id)).ReturnsAsync((AvailabilityPeriod?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetAvailabilityPeriodAsync(availabilityPeriod.Id));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetAvailabilityPeriodAsyncByVanityId_Should_ReturnAvailabilityPeriod_When_AvailabilityPeriodFound()
    {
        // Arrange
        var availabilityPeriod = new AvailabilityPeriodBuilder().Build();
        _availabilityPeriodRepositoryMock.Setup(x => x.GetByVanityIdAsync(availabilityPeriod.VanityId)).ReturnsAsync(availabilityPeriod);

        // Act
        var result = await _sut.GetAvailabilityPeriodAsync(availabilityPeriod.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(availabilityPeriod.DayOfWeek, result.Value.DayOfWeek);
        Assert.Equal(availabilityPeriod.StartTime, result.Value.StartTime);
        Assert.Equal(availabilityPeriod.EndTime, result.Value.EndTime);

        _availabilityPeriodRepositoryMock.Verify(x => x.GetByVanityIdAsync(availabilityPeriod.VanityId), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetAvailabilityPeriodAsyncByVanityId_Should_ThrowNotFoundException_When_AvailabilityPeriodNotFound()
    {
        // Arrange
        var availabilityPeriod = new AvailabilityPeriodBuilder().Build();
        _availabilityPeriodRepositoryMock.Setup(x => x.GetByVanityIdAsync(availabilityPeriod.VanityId)).ReturnsAsync((AvailabilityPeriod?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetAvailabilityPeriodAsync(availabilityPeriod.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetAvailabilityPeriodsAsync_Should_ReturnListOfAvailabilityPeriods_When_AvailabilityPeriodsFound()
    {
        // Arrange
        var availabilityPeriods = new List<AvailabilityPeriod>
        {
            new AvailabilityPeriodBuilder().WithDayOfWeek(DayOfWeek.Tuesday).Build(),
            new AvailabilityPeriodBuilder().WithDayOfWeek(DayOfWeek.Wednesday).Build()
        };
        _availabilityPeriodRepositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(availabilityPeriods);

        // Act
        var result = await _sut.GetAvailabilityPeriodsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
                          x => Assert.Equal(DayOfWeek.Tuesday, x.DayOfWeek),
                          x => Assert.Equal(DayOfWeek.Wednesday, x.DayOfWeek));

        _availabilityPeriodRepositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetAvailabilityPeriodsAsync_Should_ReturnEmpty_When_AvailabilityPeriodsNotFound()
    {
        // Arrange
        _availabilityPeriodRepositoryMock.Setup(x => x.ListAsync()).ReturnsAsync(new List<AvailabilityPeriod>());

        // Act
        var result = await _sut.GetAvailabilityPeriodsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);

        _availabilityPeriodRepositoryMock.Verify(x => x.ListAsync(), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetAvailabilityPeriodsForStaffMemberAsync_Should_ReturnListOfAvailabilityPeriods_When_AvailabilityPeriodsFound()
    {
        // Arrange
        var staffMember = new StaffMemberBuilder().Build();
        var availabilityPeriods = new List<AvailabilityPeriod>
        {
            new AvailabilityPeriodBuilder().WithDayOfWeek(DayOfWeek.Tuesday).Build(),
            new AvailabilityPeriodBuilder().WithDayOfWeek(DayOfWeek.Wednesday).Build()
        };
        _availabilityPeriodRepositoryMock.Setup(x => x.ListForStaffMember(staffMember.VanityId)).ReturnsAsync(availabilityPeriods);

        // Act
        var result = await _sut.GetAvailabilityPeriodsForStaffMemberAsync(staffMember.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
                          x => Assert.Equal(DayOfWeek.Tuesday, x.DayOfWeek),
                          x => Assert.Equal(DayOfWeek.Wednesday, x.DayOfWeek));

        _availabilityPeriodRepositoryMock.Verify(x => x.ListForStaffMember(staffMember.VanityId), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetAvailabilityPeriodsForStaffMemberAsync_Should_ReturnEmpty_When_AvailabilityPeriodsNotFound()
    {
        // Arrange
        var staffMember = new StaffMemberBuilder().Build();
        _availabilityPeriodRepositoryMock.Setup(x => x.ListForStaffMember(staffMember.VanityId)).ReturnsAsync(new List<AvailabilityPeriod>());

        // Act
        var result = await _sut.GetAvailabilityPeriodsForStaffMemberAsync(staffMember.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);

        _availabilityPeriodRepositoryMock.Verify(x => x.ListForStaffMember(staffMember.VanityId), Times.Once);
    }
}