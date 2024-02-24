namespace ToDoList.Domain.Enum
{
    public enum ErrorCodes
    {


        PasswordNotEqualsConfirmPassword = 11,
        PasswordIsWrong = 12,


        UserIsAlreadyExists = 21,
        UserNotFound = 22,


        InternalServerError = 31,

        ProfileNotFound = 41,

        TaskListNotFound = 51,

    }
}
