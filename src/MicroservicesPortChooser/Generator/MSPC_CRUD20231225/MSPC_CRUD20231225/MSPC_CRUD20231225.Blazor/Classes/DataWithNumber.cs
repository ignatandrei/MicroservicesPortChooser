namespace MSPC_CRUD20231225.Blazor.Classes;

public record DataWithNumber<T>(int number, T data)
    where T : class
{
}
