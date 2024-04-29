using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Subject), nameof(Subject))]
public partial class SubjectEditViewModel(ISubjectFacade subjectFacade, INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public SubjectDetailModel Subject { get; init; } = SubjectDetailModel.Empty;
    public async Task SaveAsync()
    {
        await subjectFacade.SaveAsync(Subject);
        MessengerService.Send(new SubjectEditMessage { SubjectId = Subject.Id });
        navigationService.SendBackButtonPressed();
    }
}
