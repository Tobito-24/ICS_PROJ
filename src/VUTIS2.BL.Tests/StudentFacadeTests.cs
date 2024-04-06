// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;
using VUTIS2.Common.Tests;
using VUTIS2.Common.Tests.Seeds;
using Xunit.Abstractions;

namespace VUTIS2.BL.Tests;

public sealed class StudentFacadeTests : FacadeTestsBase
{
    private readonly IStudentFacade _studentFacadeSUT;

    public StudentFacadeTests(ITestOutputHelper output) : base(output)
    {
        _studentFacadeSUT = new StudentFacade(UnitOfWorkFactory, StudentModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new StudentDetailModel()
        {
            Id = Guid.Empty,
            FirstName = "Name",
            LastName = "Lastname",
        };

        var _ = await _studentFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededStudent()
    {
        var students = await _studentFacadeSUT.GetAsync();
        var student = students.Single(i => i.Id == StudentSeeds.SampleStudent1.Id);
        DeepAssert.Equal(StudentModelMapper.MapToListModel(StudentSeeds.SampleStudent1), student);
    }

    [Fact]
    public async Task GetById_SeededStudent()
    {
        var student = await _studentFacadeSUT.GetAsync(StudentSeeds.SampleStudent1.Id);
        DeepAssert.Equal(StudentModelMapper.MapToListModel(StudentSeeds.SampleStudent1), student);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var student = await _studentFacadeSUT.GetAsync(StudentSeeds.EmptyStudent.Id);

        Assert.Null(student);
    }
}
