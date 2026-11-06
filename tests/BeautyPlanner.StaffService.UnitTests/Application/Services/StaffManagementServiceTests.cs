namespace BeautyPlanner.StaffService.UnitTests.Application.Services;

public class StaffManagementServiceTests
{
    private readonly Mock<IStaffRepository> _staffRepositoryMock;
    private readonly Mock<IRepository<Profession>> _professionRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly StaffManagementService _sut;

    public StaffManagementServiceTests()
    {
        _staffRepositoryMock = new Mock<IStaffRepository>();
        _professionRepositoryMock = new Mock<IRepository<Profession>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new StaffManagementService(_staffRepositoryMock.Object, _professionRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Trait("Category", "Create")]
    [Fact]
    public async Task CreateStaffMemberAsync_Should_CreateStaffMember_When_ModelIsValid()
    {
        // Arrange
        var model = new StaffMemberBuilder().BuildCreateModel();

        var sequence = new MockSequence();

        _professionRepositoryMock.InSequence(sequence)
                                 .Setup(x => x.GetByVanityIdAsync(model.ProfessionId))
                                 .ReturnsAsync(new ProfessionBuilder().Build());

        _staffRepositoryMock.InSequence(sequence)
                            .Setup(x => x.AddAsync(It.IsAny<StaffMember>()))
                            .Returns(Task.CompletedTask);

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.CreateStaffMemberAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(model.FirstName, result.Value.FirstName);
        Assert.Equal(model.LastName, result.Value.LastName);
        Assert.Equal(model.Email, result.Value.Email);
        Assert.NotNull(result.Value.Address);
        Assert.Equal(model.Address!.Line1, result.Value.Address.Line1);

        _professionRepositoryMock.Verify(x => x.GetByVanityIdAsync(model.ProfessionId), Times.Once);
        _staffRepositoryMock.Verify(x => x.AddAsync(It.IsAny<StaffMember>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateStaffMemberAsync_Should_UpdateStaffMember_When_ModelIsValid()
    {
        // Arrange
        var model = new StaffMemberBuilder().BuildUpdateModel();

        var sequence = new MockSequence();

        _professionRepositoryMock.InSequence(sequence)
                                 .Setup(x => x.GetByVanityIdAsync(model.ProfessionId))
                                 .ReturnsAsync(new ProfessionBuilder().Build());

        _staffRepositoryMock.InSequence(sequence)
                            .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                            .ReturnsAsync(new StaffMemberBuilder().Build());

        _staffRepositoryMock.InSequence(sequence)
                            .Setup(x => x.Update(It.Is<StaffMember>(t => t.FirstName == model.FirstName && t.LastName == model.LastName)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateStaffMemberAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        _staffRepositoryMock.Verify(x => x.Update(It.Is<StaffMember>(t => t.FirstName == model.FirstName && t.LastName == model.LastName)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Update")]
    [Fact]
    public async Task UpdateStaffMemberAsync_Should_ThrowNotFoundException_When_StaffMemberNotFound()
    {
        // Arrange
        var model = new StaffMemberBuilder().BuildUpdateModel();
        _staffRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((StaffMember?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.UpdateStaffMemberAsync(model));
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteStaffMemberAsync_Should_DeleteStaffMember_When_StaffMemberFound()
    {
        // Arrange
        var model = new StaffMemberBuilder().Build();

        var sequence = new MockSequence();

        _staffRepositoryMock.InSequence(sequence)
                            .Setup(x => x.GetByVanityIdAsync(model.VanityId))
                            .ReturnsAsync(model);

        _staffRepositoryMock.InSequence(sequence)
                            .Setup(x => x.Delete(It.Is<StaffMember>(t => t.VanityId == model.VanityId)));

        _unitOfWorkMock.InSequence(sequence)
                       .Setup(x => x.SaveChangesAsync())
                       .ReturnsAsync(1);

        // Act
        await _sut.DeleteStaffMemberAsync(model.VanityId);

        // Assert
        _staffRepositoryMock.Verify(x => x.Delete(It.Is<StaffMember>(t => t.VanityId == model.VanityId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Trait("Category", "Delete")]
    [Fact]
    public async Task DeleteStaffMemberAsync_Should_ThrowNotFoundException_When_StaffMemberNotFound()
    {
        // Arrange
        var model = new StaffMemberBuilder().Build();
        _staffRepositoryMock.Setup(x => x.GetByVanityIdAsync(model.VanityId)).ReturnsAsync((StaffMember?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteStaffMemberAsync(model.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetStaffMemberAsyncById_Should_ReturnStaffMember_When_StaffMemberFound()
    {
        // Arrange
        var staffMember = new StaffMemberBuilder().Build();
        _staffRepositoryMock.Setup(x => x.GetByIdAsync(staffMember.Id)).ReturnsAsync(staffMember);

        // Act
        var result = await _sut.GetStaffMemberAsync(staffMember.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(staffMember.FirstName, result.Value.FirstName);
        Assert.Equal(staffMember.LastName, result.Value.LastName);

        _staffRepositoryMock.Verify(x => x.GetByIdAsync(staffMember.Id), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetStaffMemberAsyncById_Should_ThrowNotFoundException_When_StaffMemberNotFound()
    {
        // Arrange
        var staffMember = new StaffMemberBuilder().Build();
        _staffRepositoryMock.Setup(x => x.GetByIdAsync(staffMember.Id)).ReturnsAsync((StaffMember?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetStaffMemberAsync(staffMember.Id));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetStaffMemberAsyncByVanityId_Should_ReturnStaffMember_When_StaffMemberFound()
    {
        // Arrange
        var staffMember = new StaffMemberBuilder().Build();
        _staffRepositoryMock.Setup(x => x.GetByVanityIdAsync(staffMember.VanityId)).ReturnsAsync(staffMember);

        // Act
        var result = await _sut.GetStaffMemberAsync(staffMember.VanityId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(staffMember.FirstName, result.Value.FirstName);
        Assert.Equal(staffMember.LastName, result.Value.LastName);

        _staffRepositoryMock.Verify(x => x.GetByVanityIdAsync(staffMember.VanityId), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetStaffMemberAsyncByVanityId_Should_ThrowNotFoundException_When_StaffMemberNotFound()
    {
        // Arrange
        var staffMember = new StaffMemberBuilder().Build();
        _staffRepositoryMock.Setup(x => x.GetByVanityIdAsync(staffMember.VanityId)).ReturnsAsync((StaffMember?)null);

        // Act

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _sut.GetStaffMemberAsync(staffMember.VanityId));
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetStaffMembersAsync_Should_ReturnListOfStaffMembers_When_StaffMembersFound()
    {
        // Arrange
        var staffMembers = new List<StaffMember>
        {
            new StaffMemberBuilder().WithFirstName("StaffMember 1").Build(),
            new StaffMemberBuilder().WithFirstName("StaffMember 2").Build()
        };
        _staffRepositoryMock.Setup(x => x.ListWithProfessionAsync()).ReturnsAsync(staffMembers);

        // Act
        var result = await _sut.GetStaffMembersAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
                          x => Assert.Equal("StaffMember 1", x.FirstName),
                          x => Assert.Equal("StaffMember 2", x.FirstName));

        _staffRepositoryMock.Verify(x => x.ListWithProfessionAsync(), Times.Once);
    }

    [Trait("Category", "Get")]
    [Fact]
    public async Task GetStaffMembersAsync_Should_ReturnEmpty_When_StaffMembersNotFound()
    {
        // Arrange
        _staffRepositoryMock.Setup(x => x.ListWithProfessionAsync()).ReturnsAsync(new List<StaffMember>());

        // Act
        var result = await _sut.GetStaffMembersAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);

        _staffRepositoryMock.Verify(x => x.ListWithProfessionAsync(), Times.Once);
    }
}
