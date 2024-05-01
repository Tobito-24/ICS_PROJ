using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(activityId), nameof(activityId))]
public partial class EvaluationListViewModel(IEvaluationFacade evaluationFacade, INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<EvaluationEditMessage>, IRecipient<EvaluationDeleteMessage>
{
    public Guid activityId { get; set; }
    public IEnumerable<EvaluationListModel> Evaluations { get; set; } = null!;
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Evaluations = await evaluationFacade.GetAsyncFromActivity(activityId);
    }

    [RelayCommand]
    public async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/evaluationedit",
            new Dictionary<string, object?> { [nameof(EvaluationEditViewModel.activityId)] = activityId , [nameof(EvaluationEditViewModel.Evaluation)] = EvaluationDetailModel.Empty});
    }

    [RelayCommand]
    public async Task GoToDetailAsync(Guid evaluationId)
    {
        await navigationService.GoToAsync("/evaluationdetail",
            new Dictionary<string, object?> { [nameof(EvaluationDetailViewModel.Id)] = evaluationId });
    }

    [RelayCommand]
    public async Task DeleteAsync(Guid evaluationId)
    {
        await evaluationFacade.DeleteAsync(evaluationId);
        MessengerService.Send(new EvaluationDeleteMessage());
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
