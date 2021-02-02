﻿using SalaoDeCabelereiro.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco
{
    class ProdutoDAO
    {

        private readonly string _tabela = "Produto";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public ProdutoDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao() //ToDo: nome ideal para esta função
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private bool DadosProduto(ProdutoModel produto)
        {
            GetConexao();
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Nome", produto.Nome);
            Cmd.Parameters.AddWithValue("@Peso", produto.Peso);
            Cmd.Parameters.AddWithValue("@Medicao", produto.Medicao);
            Cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
            Cmd.Parameters.AddWithValue("@Ativo", true);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public void Inserir(ProdutoModel produto)
        {
            Cmd.CommandText = $@"{ConsultaHelper.GetInsertInto(_tabela)} (@Nome, @Peso @Medicao, @Quantidade, @Ativo)";
            DadosProduto(produto);
        }

        private List<ProdutoModel> GetProduto()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<ProdutoModel> produtos = new List<ProdutoModel>();

            while (rd.Read())
            {
                ProdutoModel produto = new ProdutoModel(
                        (int)rd[nameof(ProdutoModel.Id)],
                        (string)rd[nameof(ProdutoModel.Nome)],
                        (double)rd[nameof(ProdutoModel.Peso)],
                        (string)rd[nameof(ProdutoModel.Medicao)],
                        (int)rd[nameof(ProdutoModel.Quantidade)],
                        (bool)rd[nameof(ProdutoModel.Ativo)]);

                produtos.Add(produto);
            }
            rd.Close();
            return produtos;
        }

        public void Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";

            GetProduto();
        }

        public void Consultar(string busca)
        {
            GetConexao();
            busca = $"%{busca}%";
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)} WHERE Id LIKE @busca OR Nome LIKE @busca";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@busca", busca);

            GetProduto();
        }

        public void Atualizar(ProdutoModel produto)
        {
            GetConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetUpdateSet(_tabela)} Nome = @Nome, Peso = @Peso, Medicao = @Medicao, Quantidade = @Quantidade, Ativo = @Ativo  WHERE Id = @id";

            DadosProduto(produto);
        }
    }
}
