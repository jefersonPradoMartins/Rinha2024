

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace Rinha2024.Entitdade
{
    public class Transacao
    {
        public long Id { get; set; }
        public int Valor { get; set; }
        public TipoTransacaoEnum Tipo { get; set; }
        public string Descricao { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime RealizadaEm { get; set; }

    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoTransacaoEnum
    {
        c,
        d
    }

}


//  [MaxLenght(10, messageError = "O campo descriccao deve ser do tipo string ou array com comprimento máximo de '10'.")]