using CommunityToolkit.Mvvm.Input;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Subject), nameof(Subject))]
public partial class SubjectEditViewModel(ISubjectFacade subjectFacade, INavigationService navigationService,
    IMessengerService messengerService, IStudentFacade studentFacade, IEnrollmentFacade enrollmentFacade)
    : ViewModelBase(messengerService)
{   public Guid Id { get; set; }
    public IEnumerable<StudentListModel?> Students { get; private set; } = Enumerable.Empty<StudentListModel>();
    public IEnumerable<StudentListModel?> EnrolledStudents { get; private set; } = Enumerable.Empty<StudentListModel>();
    public EnrollmentDetailModel Enrollment { get; set; } = EnrollmentDetailModel.Empty;
    public SubjectDetailModel Subject { get; set; } = SubjectDetailModel.Empty;

    protected override async Task LoadDataAsync()
    {
        Students = await studentFacade.GetAsync();
        List<StudentListModel?> studentsList = Students.ToList();
        foreach (EnrollmentListModel enrollment in Subject.Enrollments)
        {
            StudentListModel? student = await studentFacade.GetAsyncList(enrollment.SubjectId);
            studentsList.Remove(student);
            EnrolledStudents = EnrolledStudents.Append(student);
        }
        Students = studentsList;
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        await subjectFacade.SaveAsync(Subject);
        MessengerService.Send(new SubjectEditMessage { SubjectId = Subject.Id });
        navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    public Task CancelAsync()
    {
        navigationService.SendBackButtonPressed();
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task AddEnrollmentAsync(Guid StudentId)
    {
        Enrollment = EnrollmentDetailModel.Empty with {SubjectId = StudentId, StudentId = Subject.Id};
        await enrollmentFacade.SaveAsync(Enrollment with {Student = default!, Subject = default!});
        await LoadDataAsync();
    }
    private async Task ReloadDataAsync()
    {
        Subject = await subjectFacade.GetAsync(Id) ?? SubjectDetailModel.Empty;
    }
}
