using System;
using System.Collections.Generic;
using System.Text;

namespace AdministrativoImperial.Domain.Models.Common
{
    public class ResultResponseModel
    {
        public bool erro { get; set; }
        public string mensagem { get; set; }
        public int id_cadastrado { get; set; }

        public ResultResponseModel()
        {

        }
        public ResultResponseModel(bool erro, string mensagem)
        {
            this.erro = erro;
            this.mensagem = mensagem;
            this.id_cadastrado = 0;
        }
        public ResultResponseModel(bool erro, string mensagem, int idCadastrado)
        {
            this.erro = erro;
            this.mensagem = mensagem;
            this.id_cadastrado = idCadastrado;
        }
    }

    public class ResultResponseModel<T> : ResultResponseModel
               where T : class
    {
        public T model { get; set; }

        public ResultResponseModel(bool erro, string mensagem, T model)
        {
            this.erro = erro;
            this.mensagem = mensagem;
            this.id_cadastrado = 0;
            this.model = model;
        }
        public ResultResponseModel(bool erro, string mensagem, int idCadastrado, T model)
        {
            this.erro = erro;
            this.mensagem = mensagem;
            this.id_cadastrado = idCadastrado;
            this.model = model;
        }

        public ResultResponseModel(T model)
        {
            this.model = model;
        }

    }
}
