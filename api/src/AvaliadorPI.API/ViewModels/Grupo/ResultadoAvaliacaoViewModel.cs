using System;
using System.Collections.Generic;

namespace AvaliadorPI.API.ViewModels.Grupo
{
    public class ResultadoAvaliacaoViewModel
    {
        public Guid GrupoId { get; set; }
        public IEnumerable<ResultadoCriterioViewModel> Criterios { get; set; }
    }

    public class ResultadoCriterioViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<ResultadoNotaViewModel> Avaliacoes { get; set; }
    }

    public class ResultadoNotaViewModel
    {
        public Guid AvaliadoId { get; set; }
        public int Nota { get; set; }
    }
}




