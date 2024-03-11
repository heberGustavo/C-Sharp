namespace SmartSchool_WEBAPI.Models
{
    public class AlunoDisciplina
    {
        //Vinculo entre Aluno e Disciplina, usada para fazer relacionamento de N para N

        public AlunoDisciplina() { }
        public AlunoDisciplina(int alunoId, int disciplinaId)
        {
            this.AlunoId = alunoId;
            this.DisciplinaId = disciplinaId;

        }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public Disciplina Disciplina { get; set; }
        public int DisciplinaId { get; set; }
    }
}