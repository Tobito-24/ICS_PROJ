using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class EvaluationDetailViewModel(IEvaluationFacade evaluationFacade, INavigationService navigationService, IMessengerService messengerService, IAlertService alertService, IStudentFacade studentFacade, IActivityFacade activityFacade)
    : ViewModelBase(messengerService), IRecipient<EvaluationEditMessage>
{
    public Guid Id { get; set; }
    public EvaluationDetailModel? Evaluation { get; private set; }
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Evaluation = await evaluationFacade.GetAsync(Id);
        if(Evaluation is not null)
        {
            StudentListModel? student = await studentFacade.GetAsyncList(Evaluation.StudentId);
            ActivityDetailModel? activity = await activityFacade.GetAsync(Evaluation.ActivityId);
        }
        
    }
    [RelayCommand]
    public async Task DeleteAsync()
    {
        if (Evaluation is not null)
        {
            try
            {
                await evaluationFacade.DeleteAsync(Evaluation.Id);
                MessengerService.Send(new EvaluationDeleteMessage());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("filler", "filler");
            }
        }
    }

    public async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(EvaluationEditViewModel.Evaluation)] = Evaluation });
    }
    public async void Receive(EvaluationEditMessage message)
    {
        if (message.EvaluationId == Id)
        {
            await LoadDataAsync();
        }
    }
}
