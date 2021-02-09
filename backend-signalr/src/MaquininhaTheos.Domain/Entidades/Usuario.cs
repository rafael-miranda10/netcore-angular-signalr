using System;

namespace MaquininhaTheos.Domain.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Int64 Chave { get; set; }
        public DateTime DataConexao { get; set; }

        public Usuario(string nome, Int64 chave)
        {
            Nome = nome;
            Chave = chave;
            DataConexao = DateTime.Now;
        }
    }
}
