namespace Mongorize.Tests.Repositories;

using MongoDB.Driver;
using Mongorize.Contexts.Interfaces;
using Mongorize.Tests.Entities;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using Mongorize.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Mongorize.Builders;

public class BaseRepositoryTests
{
    private readonly Mock<IMongoContext> contextMock;
    private readonly Mock<IMongoCollection<TestEntity>> collectionMock;

    private readonly IRepository<TestEntity> repository;

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

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTheEntity()
    {
        // Arrange
        var expectedObjectId = ObjectId.GenerateNewId().ToString();
        var testEntity = new TestEntity { Id = expectedObjectId };

        var cToken = new CancellationToken();

        var cursorMock = new Mock<IAsyncCursor<TestEntity>>();
        cursorMock.Setup(c => c.Current).Returns(new List<TestEntity> { testEntity });
        cursorMock
            .SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);

        var serializer = BsonSerializer.SerializerRegistry.GetSerializer<TestEntity>();
        var renderArgs = new RenderArgs<TestEntity>(serializer, BsonSerializer.SerializerRegistry);

        this.collectionMock
            .Setup(c => c.FindAsync(
                It.Is<FilterDefinition<TestEntity>>(f =>
                    f.Render(renderArgs).ToString().Contains(expectedObjectId)),
                It.IsAny<FindOptions<TestEntity, TestEntity>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursorMock.Object);

        // Act
        var result = await this.repository.GetByIdAsync(testEntity.Id, cToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testEntity.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNullIfNotFound()
    {
         // Arrange
        var invalidObjectId = ObjectId.GenerateNewId().ToString();

        var cursorMock = new Mock<IAsyncCursor<TestEntity>>();

        cursorMock.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(false); // Nessun risultato trovato

        cursorMock.Setup(x => x.Current).Returns(new List<TestEntity>()); // Lista vuota

        var serializer = BsonSerializer.SerializerRegistry.GetSerializer<TestEntity>();
        var renderArgs = new RenderArgs<TestEntity>(serializer, BsonSerializer.SerializerRegistry);

        this.collectionMock
            .Setup(c => c.FindAsync(
                It.Is<FilterDefinition<TestEntity>>(f =>
                    f.Render(renderArgs).ToString().Contains(invalidObjectId)),
                It.IsAny<FindOptions<TestEntity, TestEntity>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursorMock.Object);

        // Act
        var result = await this.repository.GetByIdAsync(invalidObjectId, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowFormatException_WhenInvalidIdFormat()
    {
        // Arrange
        var invalidId = "invalid_object_id";
        var cToken = new CancellationToken();

        // Act & Assert
        await Assert.ThrowsAsync<FormatException>(() => this.repository.GetByIdAsync(invalidId, cToken));
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnTheListOfEntities()
    {
        // Arrange
        var options = new QueryOptionsBuilder<TestEntity>()
            .WithPagination(1, 10)
            .Build();

        var entities = new List<TestEntity>
        {
            new () { Id = ObjectId.GenerateNewId().ToString(), Name = "test1", },
            new () { Id = ObjectId.GenerateNewId().ToString(), Name = "test2", },
        };
        var cToken = new CancellationToken();

        var cursorMock = new Mock<IAsyncCursor<TestEntity>>();
        cursorMock.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        cursorMock.SetupGet(_ => _.Current).Returns(entities);

        this.collectionMock.Setup(c => c.FindAsync(
            It.IsAny<FilterDefinition<TestEntity>>(),
            It.IsAny<FindOptions<TestEntity, TestEntity>>(),
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(cursorMock.Object);

        // Act
        var result = await this.repository.GetListAsync(options, cToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("test1", result[0].Name);
        Assert.Equal("test2", result[1].Name);
    }
}