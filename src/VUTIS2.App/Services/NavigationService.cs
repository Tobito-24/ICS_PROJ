// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using VUTIS2.App.Models;
using VUTIS2.App.ViewModels;
using VUTIS2.App.Views;
using VUTIS2.App.Views.Student;
using VUTIS2.App.Views.Subject;
using VUTIS2.App.Views.Evaluation;
using VUTIS2.App.Views.Activity;

namespace VUTIS2.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("/students", typeof(StudentListView), typeof(StudentListViewModel)),
        new("/students/detail", typeof(StudentDetailView), typeof(StudentDetailViewModel)),
        new("/students/edit", typeof(StudentEditView), typeof(StudentEditViewModel)),
        new("/subjects", typeof(SubjectListView), typeof(SubjectListViewModel)),
        new("/subjects/detail", typeof(SubjectDetailView), typeof(SubjectDetailViewModel)),
        new("/subjects/edit", typeof(SubjectEditView), typeof(SubjectEditViewModel)),
        new("/evaluation", typeof(EvaluationDetailView), typeof(EvaluationDetailViewModel)),
        new("/evaluation/edit", typeof(EvaluationEditView), typeof(EvaluationEditViewModel)),
        new("/activity", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),
        new("/activity/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
