using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityDetailViewModel(IActivityFacade activityFacade, INavigationService navigationService, IMessengerService messengerService, IAlertService alertService, IEvaluationFacade evaluationFacade, ISubjectFacade subjectFacade)
    : ViewModelBase(messengerService), IRecipient<ActivityEditMessage>
{
    public Guid Id { get; set; }
    public ActivityDetailModel? Activity { get; private set; }
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activity = await activityFacade.GetAsync(Id);
        if (Activity is not null)
        {
            SubjectDetailModel? subject = await subjectFacade.GetAsync(Activity.SubjectId);
            IEnumerable<EvaluationListModel> evaluations = await evaluationFacade.GetAsyncFromActivity(Activity.Id);
        }

    }
    [RelayCommand]
    public async Task DeleteAsync()
    {
        if (Activity is not null)
        {
            try
            {
                await activityFacade.DeleteAsync(Activity.Id);
                MessengerService.Send(new ActivityDeleteMessage());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("filler", "filler");
            }
        }
    }
    [RelayCommand]
    public async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(ActivityEditViewModel.Activity)] = Activity });
    }
    public async void Receive(ActivityEditMessage message)
    {
        if (message.ActivityId == Id)
        {
            await LoadDataAsync();
        }
    }
}
