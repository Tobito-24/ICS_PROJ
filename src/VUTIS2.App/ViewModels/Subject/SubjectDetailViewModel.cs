using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class SubjectDetailViewModel(ISubjectFacade subjectFacade, INavigationService navigationService, IMessengerService messengerService, IAlertService alertService, IEnrollmentFacade enrollmentFacade, IActivityFacade activityFacade)
    : ViewModelBase(messengerService), IRecipient<SubjectEditMessage>
{
    public Guid Id { get; set; }
    public SubjectDetailModel? Subject { get; private set; }
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Subject = await subjectFacade.GetAsync(Id);
        IEnumerable<EnrollmentListModel> enrollments = await enrollmentFacade.GetAsync();
        //IEnumerable<ActivityListModel> activities = await activityFacade.GetAsync();
    }
    [RelayCommand]
    public async Task DeleteAsync()
    {
        if (Subject is not null)
        {
            try
            {
                await subjectFacade.DeleteAsync(Subject.Id);
                MessengerService.Send(new SubjectDeleteMessage());
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
            new Dictionary<string, object?> { [nameof(SubjectEditViewModel.Subject)] = Subject });
    }
    public async void Receive(SubjectEditMessage message)
    {
        if (message.SubjectId == Id)
        {
            await LoadDataAsync();
        }
    }
}
