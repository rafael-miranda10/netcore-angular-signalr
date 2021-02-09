using System;

namespace MaquininhaTheos.Domain.Entidades
{
    public class QrCode
    {
        public int Id { get; set; }
        public string Imagem { get; set; }
        public DateTime DataCriacao { get; set; }

        public QrCode(string imagem)
        {
            Imagem = imagem;
            DataCriacao = DateTime.Now;
        }
    }
}
