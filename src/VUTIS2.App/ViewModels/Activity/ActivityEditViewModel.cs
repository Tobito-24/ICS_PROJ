using CommunityToolkit.Mvvm.Input;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;
using VUTIS2.Common.Enums;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(SubjectId), nameof(SubjectId))]
public partial class ActivityEditViewModel(IActivityFacade activityFacade, INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty with { StartTime = DateTime.Now, EndTime = DateTime.Now };

    public List<ActivityType> ActivityTypes { get; } = Enum.GetValues<ActivityType>().ToList();
    public Guid SubjectId { get; set; }


    [RelayCommand]
    public async Task SaveAsync()
    {
        await activityFacade.SaveAsync(Activity with { SubjectId = SubjectId});
        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
        navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    public void Cancel()
    {
        navigationService.SendBackButtonPressed();
    }
}
