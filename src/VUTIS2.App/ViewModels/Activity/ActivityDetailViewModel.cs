using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityDetailViewModel(IActivityFacade activityFacade, INavigationService navigationService, IMessengerService messengerService, IAlertService alertService, IEvaluationFacade evaluationFacade, ISubjectFacade subjectFacade)
    : ViewModelBase(messengerService), IRecipient<ActivityEditMessage>, IRecipient<EvaluationEditMessage>, IRecipient<EvaluationDeleteMessage>
{
    public Guid Id { get; set; }
    public ActivityDetailModel? Activity { get; private set; }

    public SubjectDetailModel? Subject { get; private set; }

    public IEnumerable<EvaluationListModel> Evaluations { get; private set; } = Enumerable.Empty<EvaluationListModel>();
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activity = await activityFacade.GetAsync(Id);
        if (Activity is not null)
        {
            Subject = await subjectFacade.GetAsync(Activity.SubjectId);
            Evaluations = await evaluationFacade.GetAsyncFromActivity(Activity.Id);
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
    public async Task DeleteEvaluationAsync(Guid evaluationId)
    {
        await evaluationFacade.DeleteAsync(evaluationId);
        MessengerService.Send(new EvaluationDeleteMessage());
        await base.LoadDataAsync();
    }
    
    [RelayCommand]
    public async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/activityedit",
            new Dictionary<string, object?> { [nameof(ActivityEditViewModel.SubjectId)] = Activity.SubjectId, [nameof(ActivityEditViewModel.Activity)] = Activity });
    }
    [RelayCommand]
    public async Task GoToCreateEvaluationAsync()
    {
        await navigationService.GoToAsync("/evaluationedit",
            new Dictionary<string, object?> { [nameof(EvaluationEditViewModel.activityId)] = Activity.Id, [nameof(EvaluationEditViewModel.Evaluation)] = EvaluationDetailModel.Empty });
    }

    [RelayCommand]
    public async Task GoToEvaluationDetailAsync(Guid evaluationId)
    {
        await navigationService.GoToAsync("/evaluationdetail",
            new Dictionary<string, object?> { [nameof(EvaluationDetailViewModel.Id)] = evaluationId });
    }

    public async void Receive(ActivityEditMessage message)
    {
        if (message.ActivityId == Id)
        {
            await LoadDataAsync();
        }
    }

    public async void Receive(EvaluationEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(EvaluationDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
