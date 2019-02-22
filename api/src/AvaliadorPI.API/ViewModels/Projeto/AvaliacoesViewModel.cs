using System.Collections.Generic;

namespace AvaliadorPI.API.ViewModels.Projeto
{
    public class AvaliacoesViewModel
    {
        public AvaliacoesViewModel()
        {
            Notas = new List<string>();
        }

        public string Greupo { get; set; }
        public string Aluno { get; set; }

        public IEnumerable<string> Notas { get; set; }
    }
}
