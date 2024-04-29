using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;


public partial class SubjectListViewModel(ISubjectFacade subjectFacade, INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<SubjectEditMessage>, IRecipient<SubjectDeleteMessage>
{
    public IEnumerable<SubjectListModel> Subjects { get; set; } = null!;
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Subjects = await subjectFacade.GetAsync();
    }
    [RelayCommand]
    public async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<SubjectDetailViewModel>(
            new Dictionary<string, object?> { [nameof(SubjectDetailViewModel.Id)] = id });
    }
    [RelayCommand]
    public async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }
    public async void Receive(SubjectEditMessage message)
    {
        await LoadDataAsync();
    }
    public async void Receive(SubjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
