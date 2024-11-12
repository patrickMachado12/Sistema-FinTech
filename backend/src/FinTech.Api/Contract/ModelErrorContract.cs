namespace FinTech.Api.Contract
{
    public class ModelErrorContract
    {
        public string Titulo { get; set; }
        public int Status { get; set; }
        public string Mensagem { get; set; }

        public ModelErrorContract(string titulo, int status, string mensagem)
        {
            Titulo = titulo;
            Status = status;
            Mensagem = mensagem;
        }
    }
}