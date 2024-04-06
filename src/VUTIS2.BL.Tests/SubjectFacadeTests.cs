// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;
using VUTIS2.Common.Tests;
using VUTIS2.Common.Tests.Seeds;
using Xunit.Abstractions;

namespace VUTIS2.BL.Tests;

public class SubjectFacadeTests : FacadeTestsBase
{
    private readonly ISubjectFacade _subjectFacadeSUT;

    public SubjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _subjectFacadeSUT = new SubjectFacade(UnitOfWorkFactory, SubjectModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new SubjectDetailModel()
        {
            Id = Guid.Empty,
            Name = "Sub",
            Abbreviation = "Abbr",
        };

        var _ = await _subjectFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededSubject()
    {
        var subjects = await _subjectFacadeSUT.GetAsync();
        var subject = subjects.Single(i => i.Id == SubjectSeeds.SampleSubject1.Id);
        DeepAssert.Equal(SubjectModelMapper.MapToListModel(SubjectSeeds.SampleSubject1), subject);
    }

    [Fact]
    public async Task GetById_SeededSubject()
    {
        var subject = await _subjectFacadeSUT.GetAsync(SubjectSeeds.SampleSubject1.Id);
        DeepAssert.Equal(SubjectModelMapper.MapToDetailModel(SubjectSeeds.SampleSubject1), subject);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var subject = await _subjectFacadeSUT.GetAsync(SubjectSeeds.EmptySubject.Id);

        Assert.Null(subject);
    }
}
