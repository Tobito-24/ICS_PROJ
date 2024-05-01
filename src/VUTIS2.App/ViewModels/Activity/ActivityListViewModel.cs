using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;
[QueryProperty(nameof(subjectId), nameof(subjectId))]
public partial class ActivityListViewModel(IActivityFacade activityFacade, INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
{
    public Guid subjectId { get; set; }
    private bool sortedStartTimeAscending = true;
    private bool sortedEndTimeAscending = true;
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activities = await activityFacade.GetAsyncBySubject(subjectId);
    }

    [RelayCommand]
    public async Task GoToActivityDetailAsync(Guid id)
    {
        await navigationService.GoToAsync("/detail",
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });
    }

    [RelayCommand]
    public async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    public async Task DeleteAsync(Guid id)
    {
        await activityFacade.DeleteAsync(id);
        MessengerService.Send(new ActivityDeleteMessage());
    }
    [RelayCommand]
    public async Task SortByStartTimeAsync()
    {
        if (sortedStartTimeAscending)
        {
            Activities = activityFacade.GetOrderedByStartTimeAsc(Activities.ToList());
            sortedStartTimeAscending = false;
        }
        else
        {
            Activities = activityFacade.GetOrderedByStartTimeDesc(Activities.ToList());
            sortedStartTimeAscending = true;
        }

        await base.LoadDataAsync();
    }

    /*[RelayCommand]
    public async Task FilterByStartTimeAsync(DateTime startTime, bool from)
    {
        Activities = await activityFacade.GetActivitiesStartTime(startTime, from);
        await base.LoadDataAsync();
    }

    [RelayCommand]
    public async Task FilterByEndTimeAsync(DateTime endTime, bool from)
    {
        Activities = await activityFacade.GetActivitiesEndTime(endTime, from);
        await base.LoadDataAsync();
    }*/

    [RelayCommand]
    public async Task SortByEndTimeAsync()
    {
        if (sortedEndTimeAscending)
        {
            Activities = activityFacade.GetOrderedByEndTimeAsc(Activities.ToList());
            sortedEndTimeAscending = false;
        }
        else
        {
            Activities = activityFacade.GetOrderedByEndTimeDesc(Activities.ToList());
            sortedEndTimeAscending = true;
        }

        await base.LoadDataAsync();
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
