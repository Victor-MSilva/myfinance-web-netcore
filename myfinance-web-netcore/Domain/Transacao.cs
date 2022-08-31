using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Data;
using myfinance_web_netcore.infra;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain
{
    public class Transacao
    {
        private const int V = 0;

        public void Inserir(TransacaoModel formulario)
        {
            var objDAL = DAL.GetInstancia;
            objDAL.Conectar();

            var sql = "INSERT INTO TRANSACAO (data,valor,tipo, historico,id_plano_conta) " +
            "VALUES(" +
            $"'{formulario.Data.ToString("yyyy-MM-dd")}'," +
            $"{formulario.Valor}," +
            $"'{formulario.Tipo}'," +
            $"'{formulario.Historico}'," +
            $"{formulario.IdPlanoConta})";

            objDAL.ExecutarComandoSQL(sql);
            objDAL.Desconectar();
        }
         public void Atualizar(TransacaoModel formulario)
        {
            var objDAL = DAL.GetInstancia;
            objDAL.Conectar();
            var sql = $"UPDATE TRANSACAO SET " +
            $"Historico = '{formulario.Historico}'," +
            $"Tipo = '{formulario.Tipo}', " +
            $"Valor = {formulario.Valor} ," +
            $"Data = '{formulario.Data.ToString("yyyy-MM-dd")}', " +
            $"id_plano_conta = '{formulario.IdPlanoConta}' " +
            $"WHERE id = {formulario.Id}";

            objDAL.ExecutarComandoSQL(sql);
            objDAL.Desconectar();
        }

        public void Excluir(int id)
        {
            var objDAL = DAL.GetInstancia;
            objDAL.Conectar();
            var sql = $"DELETE FROM transacao WHERE ID = {id}";
            objDAL.ExecutarComandoSQL(sql);
            objDAL.Desconectar();
        }
        public TransacaoModel CarregarTransacaoPorId (int? id)
        {
         var objDAL = DAL.GetInstancia;
            objDAL.Conectar();

            var sql = $"select id, data, valor, tipo, historico, id_plano_conta from transacao WHERE ID = {id}";
            var dataTable = objDAL.RetornarDataTable(sql);
            Console.WriteLine(dataTable);
            var transacao = new TransacaoModel()
            {
                Id = int.Parse(dataTable.Rows[0]["id"].ToString()),
                Historico = dataTable.Rows[0]["historico"].ToString(),
                Tipo = dataTable.Rows[0]["tipo"].ToString(),
                Data = DateTime.Parse(dataTable.Rows[0]["data"].ToString()),
                Valor = decimal.Parse(dataTable.Rows[0]["valor"].ToString()),
                IdPlanoConta = int.Parse(dataTable.Rows[0]["id_plano_conta"].ToString()),
            };
             objDAL.Desconectar();
            return transacao;
        }

         public List<TransacaoModel> ListaTransacoes()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();

            var objDAL = DAL.GetInstancia;
            objDAL.Conectar();

            var sql = "select id, data, valor, tipo, historico, id_plano_conta from transacao";
            var dataTable = objDAL.RetornarDataTable(sql);

             for (int i=0; i < dataTable.Rows.Count; i++)
             {
                var transacao = new TransacaoModel()
                {
                    Id = int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Data = DateTime.Parse(dataTable.Rows[i]["DATA"].ToString()),
                    Valor = decimal.Parse(dataTable.Rows[i]["VALOR"].ToString()),
                    Tipo = dataTable.Rows[i]["TIPO"].ToString(),
                    Historico = dataTable.Rows[i]["HISTORICO"].ToString(),
                    IdPlanoConta = int.Parse(dataTable.Rows[i]["ID_PLANO_CONTA"].ToString()),
                    
                };
                lista.Add(transacao);
             }
             objDAL.Desconectar();
            return lista;
        }

        public List<TransacaoModelMin> filterTransacao(DateTime? startDate, DateTime? endDate)
        {
            List<TransacaoModelMin> Transacao = new List<TransacaoModelMin>();
            DAL dalInstance = DAL.GetInstancia;
            dalInstance.Conectar();

            StringBuilder sql = new StringBuilder("SELECT sum(valor) as valors, tipo");
            sql.Append(" FROM Transacao ");

            if (startDate != null)
            {
                sql.Append($" WHERE data >= '{startDate?.ToString("yyyy-MM-dd")}'");

                if (endDate != null)
                {
                    sql.Append($" AND data <= '{endDate?.ToString("yyyy-MM-dd")}'");
                }
            
            }
            else
            {
                sql.Append($" WHERE data <= '{endDate?.ToString("yyyy-MM-dd")}'");
            }
            sql.Append(" GROUP BY tipo");

            DataTable dataTable = dalInstance.RetornarDataTable(sql.ToString());

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TransacaoModelMin TransacaoObj = new TransacaoModelMin()
                {
                    Valor = decimal.Parse(dataTable.Rows[i]["valors"].ToString()),
                    Tipo = dataTable.Rows[i]["tipo"].ToString()
                };

                Transacao.Add(TransacaoObj);
            }

            dalInstance.Desconectar();

            return Transacao;
        }
        
    }
}