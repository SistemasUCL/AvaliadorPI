using System;

namespace AvaliadorPI.API.ViewModels.Aluno
{
    public class AlunoFormViewModel
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Matricula { get; set; }
    }
}
