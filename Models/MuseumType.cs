using System.Text.Json.Serialization;

namespace MuseumIstanbul.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MuseumType
    {
        SanatMuzeleri = 0,
        TarihMuzeleri = 1,
        AntropolojikMuzeler = 2,
        DogaTarihiMuzeleri = 3,
        BilimVeEndustriMuzeleri = 4,
        UzmanlikDallariylaIlgiliMuzeler = 5,
        AnitMuzeler = 6,
        AskeriMuzeler = 7,
        OzelMuzeler = 8
    }
}
