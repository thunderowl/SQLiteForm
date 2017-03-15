namespace SQLite.Entidade
{
    class Pessoa
    {
        private string nome;
        private string sobrenome;
        private string dataNascimento;
        private string sexo;

        public Pessoa(string nome, string sobrenome, string dataNascimento, string sexo)
        {
            this.nome = nome;
            this.sobrenome = sobrenome;
            this.dataNascimento = dataNascimento;
            this.sexo = sexo;
        }

        public string Nome { get => nome; set => nome = value; }
        public string Sobrenome { get => sobrenome; set => sobrenome = value; }
        public string DataNascimento { get => dataNascimento; set => dataNascimento = value; }
        public string Sexo { get => sexo; set => sexo = value; }
    }
}
