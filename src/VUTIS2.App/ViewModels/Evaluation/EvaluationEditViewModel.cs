using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(activityId), nameof(activityId))]
[QueryProperty(nameof(Evaluation), nameof(Evaluation))]
public partial class EvaluationEditViewModel(IEvaluationFacade evaluationFacade, INavigationService navigationService,
    IMessengerService messengerService, IStudentFacade studentFacade)
    : ViewModelBase(messengerService), IRecipient<EvaluationEditMessage>, IRecipient<EvaluationDeleteMessage>
{
    public Guid activityId { get; set; }
    public EvaluationDetailModel Evaluation { get; set; } = EvaluationDetailModel.Empty;

    public IEnumerable<StudentListModel> Students { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Students = await studentFacade.GetAsync();
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        await evaluationFacade.SaveAsync(Evaluation);
        MessengerService.Send(new EvaluationEditMessage { EvaluationId = Evaluation.Id });
        navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    public void Cancel()
    {
        navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    public async Task AddStudentAsync(Guid StudentId)
    {
        Evaluation.StudentId = StudentId;
        await evaluationFacade.SaveAsync(Evaluation);
        MessengerService.Send(new EvaluationEditMessage { EvaluationId = Evaluation.Id });
        navigationService.SendBackButtonPressed();
    }

    public async void Receive(EvaluationEditMessage message)
    {
        if (message.EvaluationId == Evaluation.Id)
        {
            await LoadDataAsync();
        }
    }

    public void Receive(EvaluationDeleteMessage message)
    {
        navigationService.SendBackButtonPressed();
    }
}
