using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Contexto;
using System.Data;

namespace LojaCarrosV2.Infra.Repositorio
{
    public class UsuarioDal
    {
        private Conexao contexto;

        private void Inserir(Usuario u)
        {
            try
            {
                var strQuery = "INSERT INTO Usuario";
                strQuery += "(Email, Senha, TokenUsuario, ValidadeTokenUsuario, DataCriacao, IdPermissao, IdEmpresa) ";
                strQuery += "VALUES(@p1, @p2, @p3, @p4, @p5, @p6, @p7)";

                using (contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", u.Email);
                        cmd.Parameters.AddWithValue("@p2", u.Senha);
                        cmd.Parameters.AddWithValue("@p3", u.TokenUsuario);
                        cmd.Parameters.AddWithValue("@p4", u.ValidadeTokenUsuario);
                        cmd.Parameters.AddWithValue("@p5", u.DataCriacao);
                        cmd.Parameters.AddWithValue("@p6", u.IdPermissao);
                        cmd.Parameters.AddWithValue("@p7", u.IdEmpresa);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Salvar(Usuario u)
        {
            if (u.IdUsuario > 0)
                Alterar(u);
            else
                Inserir(u);
        }

        private void Alterar(Usuario u)
        {
            try
            {
                var strQuery = "UPDATE Usuario SET ";
                strQuery += "Senha = IFNULL(@p1, Senha),";
                strQuery += "TokenUsuario = IFNULL(@p2, TokenUsuario),";
                strQuery += "ValidadeTokenUsuario = IFNULL(@p3, ValidadeTokenUsuario),";
                strQuery += "DataCriacao = IFNULL(@p4, DataCriacao), ";
                strQuery += "IdPermissao = IF(@p5 > 0, @p5, IdPermissao), ";
                strQuery += "IdEmpresa = IF(@p6 > 0, @p6, IdEmpresa), ";
                strQuery += "Email = IFNULL(@p7, Email) ";
                strQuery += string.Format("WHERE IdUsuario = {0} ", u.IdUsuario);
                using (contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {
                        cmd.Parameters.AddWithValue("@p1", u.Senha);
                        cmd.Parameters.AddWithValue("@p2", u.TokenUsuario);
                        cmd.Parameters.AddWithValue("@p3", u.ValidadeTokenUsuario);
                        cmd.Parameters.AddWithValue("@p4", u.DataCriacao);
                        cmd.Parameters.AddWithValue("@p5", u.IdPermissao);
                        cmd.Parameters.AddWithValue("@p6", u.IdEmpresa);
                        cmd.Parameters.AddWithValue("@p7", u.Email);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario ListarPorId(int id)
        {// lista todos usuarios da base de dados
            try
            {

                using (contexto = new Conexao())
                {

                    var strQuery = "SELECT ";
                    strQuery += "U.IdUsuario, U.Email, U.IdEmpresa, U.IdPermissao ";
                    strQuery += "FROM Usuario AS U ";
                    //strQuery += "INNER JOIN Empresa AS E ON E.IdEmpresa = U.IdEmpresa ";
                    strQuery += string.Format("WHERE U.IdUsuario = {0}", id);

                    contexto.Dr = contexto.ExecutaComandoComRetorno(strQuery);
                    Usuario usuario = new Usuario();
                    while (contexto.Dr.Read())
                    {
                        usuario.IdUsuario = Convert.ToInt32(contexto.Dr["IdUsuario"]);
                        usuario.Email = Convert.ToString(contexto.Dr["Email"]);
                        usuario.IdEmpresa = Convert.ToInt32(contexto.Dr["IdEmpresa"]);
                        usuario.IdPermissao = Convert.ToInt32(contexto.Dr["IdPermissao"]);
                    }
                    return usuario;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Usuario> Listar()
        {
            return Listar(null);
        }
        public List<Usuario> Listar(int? idEmpresa)
        {// lista todos usuarios da base de dados
            try
            {
                var strQuery = "SELECT U.IdUsuario, U.Email, U.Senha, U.TokenUsuario, U.DataCriacao, U.ValidadeTokenUsuario, P.Valor, U.IdEmpresa, E.Nome, P.Nome AS NomePermissao ";
                strQuery += "FROM Usuario AS U ";
                strQuery += "INNER JOIN Empresa AS E ON E.IdEmpresa = U.IdEmpresa ";
                strQuery += "INNER JOIN Permissao AS P ON P.IdPermissao = U.IdPermissao ";
                strQuery += "WHERE(U.IdEmpresa = @p1 OR @p1 IS NULL) AND  U.Email <> 'davi' ORDER BY Email ASC";
                using (Conexao contexto = new Conexao())
                {

                    MySqlCommand Cmd = new MySqlCommand
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = contexto.minhaConexao
                    };
                    Cmd.Parameters.AddWithValue("@p1", idEmpresa);
                    contexto.Dr = Cmd.ExecuteReader();
                    
                    List<Usuario> lista = new List<Usuario>();
                    while (contexto.Dr.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Permissao = new Permissao();
                        usuario.Empresa = new Empresa();

                        usuario.IdUsuario = Convert.ToInt32(contexto.Dr["IdUsuario"]);
                        usuario.IdEmpresa = Convert.ToInt32(contexto.Dr["IdEmpresa"]);
                        usuario.Email = Convert.ToString(contexto.Dr["Email"]);
                        usuario.Senha = Convert.ToString(contexto.Dr["Senha"]);
                        usuario.TokenUsuario = Convert.ToString(contexto.Dr["TokenUsuario"]);
                        usuario.DataCriacao = Convert.ToDateTime(contexto.Dr["DataCriacao"]);
                        usuario.ValidadeTokenUsuario = Convert.ToInt32(contexto.Dr["ValidadeTokenUsuario"]);
                        usuario.Permissao.Valor = Convert.ToString(contexto.Dr["Valor"]);
                        usuario.Permissao.Nome = Convert.ToString(contexto.Dr["NomePermissao"]);
                        usuario.Empresa.Nome = Convert.ToString(contexto.Dr["Nome"]);

                        lista.Add(usuario);
                    }
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool Existe(string Email)
        {// verifica se existe antes de alterar senha enviar emaiil etc
            try
            {
                using (contexto = new Conexao())
                {
                    var strQuery = "SELECT Email FROM Usuario WHERE Email = @p1";
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", Email);
                        contexto.Dr = cmd.ExecuteReader();
                        if (contexto.Dr.Read())
                        {
                            return true;
                        }
                        else { return false; }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool VerificarSenhaAtual(string senha, int idUsuario)
        {//VerificarSenhaAtual antes de trocar pela nova
            try
            {
                using (contexto = new Conexao())
                {
                    var strQuery = "SELECT IdUsuario, Senha FROM Usuario WHERE Senha = @p1 AND IdUsuario = @p2";

                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", senha);
                        cmd.Parameters.AddWithValue("@p2", idUsuario);
                        contexto.Dr = cmd.ExecuteReader();
                        while (contexto.Dr.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Usuario Autenticar(string email, string senha)
        {
            try
            {
                var strQuery = "SELECT IdUsuario, U.Email, Senha, U.IdEmpresa, P.Valor, E.Nome FROM Usuario AS U ";
                strQuery += "INNER JOIN Permissao AS P ON P.IdPermissao = U.IdPermissao ";
                strQuery += "LEFT JOIN Empresa AS E ON E.IdEmpresa = U.IdEmpresa ";
                strQuery += "WHERE U.Email = @p1 AND Senha = @p2 AND E.Ativa = 1";
                using (contexto = new Conexao())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, contexto.minhaConexao))
                    {

                        cmd.Parameters.AddWithValue("@p1", email);
                        cmd.Parameters.AddWithValue("@p2", senha);
                        contexto.Dr = cmd.ExecuteReader();
                        Usuario usuarioAutenticado = null;
                        if (contexto.Dr.Read())
                        {
                            usuarioAutenticado = new Usuario();
                            usuarioAutenticado.Permissao = new Permissao();
                            usuarioAutenticado.Empresa = new Empresa();
                            usuarioAutenticado.Email = Convert.ToString(contexto.Dr["Email"]);
                            usuarioAutenticado.IdUsuario = Convert.ToInt32(contexto.Dr["IdUsuario"]);
                            usuarioAutenticado.Permissao.Valor = Convert.ToString(contexto.Dr["Valor"]);
                            usuarioAutenticado.IdEmpresa = Convert.ToInt32(contexto.Dr["IdEmpresa"]);
                            usuarioAutenticado.Empresa.Nome= Convert.ToString(contexto.Dr["Nome"]);

                        }

                        return usuarioAutenticado;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Excluir(int idUsuario)
        {
            try
            {
                var strQuery = string.Format("DELETE FROM Usuario WHERE IdUsuario = {0}", idUsuario);

                using (Conexao contexto = new Conexao())
                {
                    contexto.ExecutaComando(strQuery);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
