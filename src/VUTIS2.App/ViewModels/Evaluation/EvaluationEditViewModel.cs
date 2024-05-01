using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(activityId), nameof(activityId))]
[QueryProperty(nameof(Evaluation), nameof(Evaluation))]
public partial class EvaluationEditViewModel(IEvaluationFacade evaluationFacade, INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public Guid activityId { get; set; }
    public EvaluationDetailModel Evaluation { get; set; } = EvaluationDetailModel.Empty;
    public async Task SaveAsync()
    {
        await evaluationFacade.SaveAsync(Evaluation);
        MessengerService.Send(new EvaluationEditMessage { EvaluationId = Evaluation.Id });
        navigationService.SendBackButtonPressed();
    }
}
