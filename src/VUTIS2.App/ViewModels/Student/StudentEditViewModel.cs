using CommunityToolkit.Mvvm.Input;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Student), nameof(Student))]
public partial class StudentEditViewModel(IStudentFacade studentFacade, INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public Guid Id { get; set; }
    public StudentDetailModel Student { get; init; } = StudentDetailModel.Empty;
    
    [RelayCommand]
    public async Task SaveAsync()
    {
        await studentFacade.SaveAsync(Student with {Enrollments = default!});
        MessengerService.Send(new StudentEditMessage {StudentId = Student.Id});
        navigationService.SendBackButtonPressed();
    }
}
