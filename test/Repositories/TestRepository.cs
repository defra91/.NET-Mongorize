namespace Mongorize.Tests.Repositories;

using Mongorize.Contexts.Interfaces;
using Mongorize.Repositories;
using Mongorize.Tests.Entities;

public class TestRepository(IMongoContext context)
    : BaseRepository<TestEntity>(context)
{
}