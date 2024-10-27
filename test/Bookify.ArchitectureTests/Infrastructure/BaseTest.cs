using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure;
using System.Reflection;

namespace Bookify.ArchitectureTests.Infrastructure;

public abstract class BaseTest
{
    protected static Assembly ApplicationAssembly => typeof(IBaseCommand).Assembly;

    protected static Assembly DomainAssembly => typeof(Entity).Assembly;

    protected static Assembly InfrastructureAssembly => typeof(ApplicationDbContext).Assembly;

    protected static Assembly PresentationAssembly => typeof(Program).Assembly;
}