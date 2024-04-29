using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class StudentDetailViewModel( IStudentFacade studentFacade, INavigationService navigationService, IMessengerService messengerService, IAlertService alertService, IEnrollmentFacade enrollmentFacade, ISubjectFacade subjectFacade)
    : ViewModelBase(messengerService), IRecipient<StudentEditMessage>
{
    public Guid Id { get; set; }
    public StudentDetailModel? Student { get; private set; }

    public IEnumerable<SubjectListModel?> Subjects { get; private set; } = Enumerable.Empty<SubjectListModel>();
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Student = await studentFacade.GetAsync(Id);
        if (Student is not null)
        {
            foreach (EnrollmentListModel enrollment in Student.Enrollments)
            {
                SubjectListModel? subject = await subjectFacade.GetAsyncList(enrollment.SubjectId);
                Subjects = Subjects.Append(subject);
            }
        }
    }

    [RelayCommand]
    public async Task GoToSubjectDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<SubjectDetailViewModel>(
            new Dictionary<string, object?> { [nameof(SubjectDetailViewModel.Id)] = id });
    }
    [RelayCommand]

    public async Task GoToStudentEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(StudentEditViewModel.Student)] = Student });
    }
    [RelayCommand]
    public async Task DeleteAsync()
    {
        if (Student is not null)
        {
            try
            {
                await studentFacade.DeleteAsync(Student.Id);
                MessengerService.Send(new StudentDeleteMessage());
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
            new Dictionary<string, object?> { [nameof(StudentEditViewModel.Student)] = Student });
    }
    public async void Receive(StudentEditMessage message)
    {
        if (message.StudentId == Id)
        {
            await LoadDataAsync();
        }
    }
}
