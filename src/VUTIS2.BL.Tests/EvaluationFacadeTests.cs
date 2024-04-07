using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Mappers;
using VUTIS2.BL.Models;
using VUTIS2.Common.Enums;
using VUTIS2.Common.Tests;
using VUTIS2.Common.Tests.Seeds;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.UnitOfWork;
using Xunit.Abstractions;

namespace VUTIS2.BL.Tests;

public class EvaluationFacadeTests : FacadeTestsBase
{
    private readonly IEvaluationFacade _evaluationFacadeSUT;

    public EvaluationFacadeTests(ITestOutputHelper output) : base(output)
    {
        _evaluationFacadeSUT = new EvaluationFacade(UnitOfWorkFactory, EvaluationModelMapper);
    }

    [Fact]
    public async Task CreateEvaluation_WithSubject_EqualsCreated()
    {
        var model = new EvaluationDetailModel()
        {
            Points = 1,
            ActivityId = ActivitySeeds.SampleActivity1.Id,
            StudentId = StudentSeeds.SampleStudent1.Id,


        };

        model = await _evaluationFacadeSUT.SaveAsync(model);
        var dbx = await DbContextFactory<>.CreateDbContextAsync();
        var entity = await dbx.Activities.SingleAsync(e => e.Id == model.Id);
        DeepAssert.Equal(model, ActivityModelMapper.MapToDetailModel(entity));
    }
}
