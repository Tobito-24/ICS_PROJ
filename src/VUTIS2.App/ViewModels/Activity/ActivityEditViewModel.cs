using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Activity), nameof(Activity))]
public partial class ActivityEditViewModel(IActivityFacade activityFacade, INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;
    public async Task SaveAsync()
    {
        await activityFacade.SaveAsync(Activity);
        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
        navigationService.SendBackButtonPressed();
    }
}
