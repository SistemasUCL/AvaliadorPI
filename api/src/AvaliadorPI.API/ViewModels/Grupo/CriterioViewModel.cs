using System;

namespace AvaliadorPI.API.ViewModels.Grupo
{
    public class CriterioViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Peso { get; set; }
    }
}
