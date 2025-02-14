namespace Application.Dto
{
    public record class MessageResponseDto<T>(bool Success, string Message, T? Data = default); 
    // Implementado o Objeto Generico para melhorar a legibilidade do código e padronização no 
}
