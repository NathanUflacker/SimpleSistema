namespace Cpoint
{
    public class Pessoa
    {
        public string Nome { get; set; }

        public Pessoa()
        {
            Nome = string.Empty;
        }

        public Pessoa(string nome)
        {
            Nome = nome;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
