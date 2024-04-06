using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;
using VUTIS2.Common.Enums;
using VUTIS2.Common.Tests;
using VUTIS2.Common.Tests.Seeds;
using System.Collections.ObjectModel;
using VUTIS2.BL.Tests;
using VUTIS2.Common.Tests;
using VUTIS2.DAL.UnitOfWork;
using Xunit;
using Xunit.Abstractions;

namespace VUTIS2.BL.Tests;

public class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _facadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task Create_EqualsCreated()
    {
        //Arrange
        var model = new ActivityDetailModel()
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(4),
            RoomName = "A100",
            ActivityType = ActivityType.Exam,
            Description = "Test activity description",
            SubjectId = SubjectSeeds.SampleSubject1.Id,
        };

        //Act
        var returnedModel = await _facadeSUT.SaveAsync(model, SubjectSeeds.SampleSubject1.Id);

        //Assert
        DeepAssert.Equal<Task>(model, returnedModel);
    }
}
