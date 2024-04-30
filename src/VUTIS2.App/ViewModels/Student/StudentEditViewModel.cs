using CommunityToolkit.Mvvm.Input;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;
using VUTIS2.DAL.Seeds;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Student), nameof(Student))]
public partial class StudentEditViewModel(IStudentFacade studentFacade, INavigationService navigationService,
    IMessengerService messengerService, IEnrollmentFacade enrollmentFacade, ISubjectFacade subjectFacade)
    : ViewModelBase(messengerService)
{
    public Guid Id { get; set; }
    public EnrollmentDetailModel Enrollment { get; set; } = EnrollmentDetailModel.Empty;
    public StudentDetailModel Student { get; set; } = StudentDetailModel.Empty;

    public IEnumerable<SubjectListModel?> Subjects { get; private set; } = Enumerable.Empty<SubjectListModel>();
    public IEnumerable<SubjectListModel?> EnrolledSubjects { get; private set; } = Enumerable.Empty<SubjectListModel>();
    protected override async Task LoadDataAsync()
    {
        EnrolledSubjects = Enumerable.Empty<SubjectListModel>();
        Subjects = await subjectFacade.GetAsync();
        List<SubjectListModel?> subjectsList = Subjects.ToList();
        foreach (EnrollmentListModel enrollment in Student.Enrollments)
        {
            SubjectListModel? subject = await subjectFacade.GetAsyncList(enrollment.SubjectId);
            subjectsList.Remove(subject);
            EnrolledSubjects = EnrolledSubjects.Append(subject);
        }
        Subjects = subjectsList;
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        await studentFacade.SaveAsync(Student with {Enrollments = default!});
        MessengerService.Send(new StudentEditMessage {StudentId = Student.Id});
        navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    public Task CancelAsync()
    {
        navigationService.SendBackButtonPressed();
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task AddEnrollmentAsync(Guid SubjectId)
    {
        Enrollment = EnrollmentDetailModel.Empty with {SubjectId = SubjectId, StudentId = Student.Id};
        await enrollmentFacade.SaveAsync(Enrollment with {Student = default!, Subject = default!});
        await ReloadDataAsync();
        await LoadDataAsync();
    }

    [RelayCommand]
    public async Task RemoveEnrollmentAsync(Guid SubjectId)
    {
        var deletedEnrollment = Student.Enrollments.FirstOrDefault(e => e.SubjectId == SubjectId);
        if (deletedEnrollment is not null)
        {
            await enrollmentFacade.DeleteAsync(deletedEnrollment.Id);
        }
        await ReloadDataAsync();
        await LoadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        Student = await studentFacade.GetAsync(Student.Id) ?? StudentDetailModel.Empty;
    }
}
