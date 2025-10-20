namespace EF_example.Validation;

public enum ErrorCode
{
    EmailIsCreated,
    LoginIsCreated,
    InvalidCredentials
}

public interface IValidationStorage
{
    void AddError(ErrorCode errorCode, string errorMessage);
    bool IsValid { get; }
    (ErrorCode, string) GetError();
    void Clear();
}

