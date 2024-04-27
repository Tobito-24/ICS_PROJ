﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;


public partial class StudentListViewModel(IStudentFacade studentFacade,  INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<StudentEditMessage>, IRecipient<StudentDeleteMessage>
{
    public IEnumerable<StudentListModel> Students { get; set; } = null!;
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Students = await studentFacade.GetAsync();
    }
    public async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<StudentDetailViewModel>(
            new Dictionary<string, object?> { [nameof(StudentDetailViewModel.Id)] = id });
    }
    public async Task GoToCreateAsync(Guid studentId)
    {
        await navigationService.GoToAsync("/edit");
    }
    public async void Receive(StudentEditMessage message)
    {
        await LoadDataAsync();
    }
    public async void Receive(StudentDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
