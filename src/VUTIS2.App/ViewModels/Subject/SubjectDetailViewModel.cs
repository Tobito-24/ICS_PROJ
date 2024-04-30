using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class SubjectDetailViewModel(ISubjectFacade subjectFacade, INavigationService navigationService, IMessengerService messengerService, IAlertService alertService, IEnrollmentFacade enrollmentFacade, IActivityFacade activityFacade, IStudentFacade studentFacade)
    : ViewModelBase(messengerService), IRecipient<SubjectEditMessage>
{
    public Guid Id { get; set; }
    public SubjectDetailModel? Subject { get; private set; }
    public IEnumerable<StudentListModel?> Students { get; private set; } = Enumerable.Empty<StudentListModel>();

    public IEnumerable<ActivityListModel> Activities { get; private set; } = Enumerable.Empty<ActivityListModel>();
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Subject = await subjectFacade.GetAsync(Id);
        if (Subject is not null)
        {
            foreach (EnrollmentListModel enrollment in Subject.Enrollments)
            {
                StudentListModel? student = await studentFacade.GetAsyncList(enrollment.StudentId);
                Students = Students.Append(student);
            }
            Activities = await activityFacade.GetAsyncFromSubject(Subject.Id);
        }

    }
    [RelayCommand]
    public async Task GoToStudentDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<StudentDetailViewModel>(
            new Dictionary<string, object?> { [nameof(StudentDetailViewModel.Id)] = id });
    }
    [RelayCommand]
    public async Task GoToActivityDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });
    }
    [RelayCommand]
    public async Task GoToSubjectEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(SubjectEditViewModel.Subject)] = Subject });
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
