using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;
using VUTIS2.Common.Enums;
using VUTIS2.Common.Tests;
using VUTIS2.Common.Tests.Seeds;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using VUTIS2.BL.Tests;
using VUTIS2.Common.Tests;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.UnitOfWork;
using Xunit;
using Xunit.Abstractions;

namespace VUTIS2.BL.Tests;

public class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _facadeSUT;
    private readonly ISubjectFacade _subjectFacadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
        _subjectFacadeSUT = new SubjectFacade(UnitOfWorkFactory, SubjectModelMapper);
    }

    [Fact]
    public async Task Create_EqualsCreated()
    {
        //Arrange
        var model = new ActivityDetailModel()
        {
            StartTime = DateTime.Parse("2023-01-04 07:00 AM"),
            EndTime = DateTime.Parse("2023-01-04 08:00 AM"),
            RoomName = "A100",
            ActivityType = ActivityType.Exam,
            Description = "Test activity description",
            SubjectId = SubjectSeeds.SampleSubject1.Id,
        };

        //Act
        model = await _facadeSUT.SaveAsync(model);
        var dbContext = await DbContextFactory.CreateDbContextAsync();
        var entity = await dbContext.Activities.SingleAsync(e => e.Id == model.Id);
        //Assert
        DeepAssert.Equal<ActivityDetailModel>(model, ActivityModelMapper.MapToDetailModel(entity));
    }


}
