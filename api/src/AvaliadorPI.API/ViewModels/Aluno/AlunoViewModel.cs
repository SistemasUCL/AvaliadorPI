using System;
using System.Collections.Generic;

namespace AvaliadorPI.API.ViewModels.Aluno
{
    public class AlunoViewModel
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }

        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public string Matricula { get; set; }

        public IEnumerable<GrupoViewModel> Grupos;
    }
}
