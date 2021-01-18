using ApiMongo.Data.Collections;

namespace ApiMongo.Models.Extensions
{
    public static class InfectadoDTOExtension
    {
        public static Infectado ToDocument(this InfectadoDTO infectadoDTO) =>
            new Infectado(infectadoDTO.DataNascimento, infectadoDTO.Sexo, infectadoDTO.Latitude, infectadoDTO.Longitude);
    }
}