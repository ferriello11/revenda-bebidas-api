public class ApiResponse
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }

    public ApiResponse(bool sucesso, string mensagem)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
    }
}