namespace Mongorize.Tests.Repositories;

using MongoDB.Driver;
using Mongorize.Contexts.Interfaces;
using Mongorize.Repositories;
using Mongorize.Tests.Entities;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

public class BaseRepositoryTests
{
    private readonly Mock<IMongoContext> contextMock;
    private readonly Mock<IMongoCollection<TestEntity>> collectionMock;

    private readonly BaseRepository<TestEntity> repository;

    public BaseRepositoryTests()
    {
        this.contextMock = new Mock<IMongoContext>();
        this.collectionMock = new Mock<IMongoCollection<TestEntity>>();

        this.contextMock
            .Setup(c => c.GetCollection<TestEntity>())
            .Returns(this.collectionMock.Object);

        this.repository = new TestRepository(this.contextMock.Object);
    }

    [Fact]
    public async Task Create_ShouldInsertEntityInCollection()
    {
        // Arrange
        var testEntity = new TestEntity();
        var cancellationToken = new CancellationToken();

        // Act
        await this.repository.CreateAsync(testEntity, cancellationToken);

        // Assert
        this.collectionMock.Verify(
            c => c.InsertOneAsync(testEntity, null, cancellationToken),
            Times.Once
        );
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentNullException_WhenEntityIsNull()
    {
        // Arrange
        TestEntity testEntity = null;
        CancellationToken cToken = new CancellationToken();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => this.repository.CreateAsync(testEntity, cToken));

        Assert.Equal("TestEntity object is null", exception.ParamName);
    }

    [Fact]
    public async Task CreateRange_ShouldInsertManyEntitiesInCollection()
    {
        // Arrange
        var testEntities = new List<TestEntity> { new TestEntity(), new TestEntity() };
        var cancellationToken = new CancellationToken();

        // Act
        await this.repository.CreateRangeAsync(testEntities, cancellationToken);

        // Assert
        this.collectionMock.Verify(
            c => c.InsertManyAsync(testEntities, null, cancellationToken),
            Times.Once
        );
    }

    [Fact]
    public async Task CreateRange_ShouldThrowArgumentNullException_WhenListIsEmpty()
    {
        // Arrange
        var emptyList = new List<TestEntity>();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => this.repository.CreateRangeAsync(emptyList, CancellationToken.None));

        Assert.Equal("TestEntity list is empty", exception.ParamName);
    }
}