using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PwMidiaCaptacaoLeads.Dominio.Entidades;

namespace PwMidiaCaptacaoLeads.Dados.Repositorios.Interfaces
{
    public interface IRepositorio<TEntidade> where TEntidade : EntidadeBase
    {
        /// <summary>
        ///   Busca uma entidade por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntidade> Buscar(int id);

        /// <summary>
        /// Busca uma entidade por uma chave
        /// </summary>
        /// <param name="chave"></param>
        /// <returns></returns>
        Task<TEntidade> Buscar(Guid chave);

        Task<TEntidade> BuscarPorId(int id);


        /// <summary>
        /// Busca uma entidade por uma expressão usando FirstOrDefault
        /// </summary>
        /// <param name="expressao"></param>
        /// <returns></returns>
        Task<TEntidade> Buscar(Expression<Func<TEntidade, bool>> expressao);



        Task<IEnumerable<TEntidade>> ObterTodosPaginados(int page, int pageSize);

        Task<int> ContarTotal();

        /// <summary>
        /// Realiza uma busca paginada, retornando um objeto contendo o total de registros encontrados e uma
        /// lista com o resultado de acordo com a página e as propriedades selecionadas no filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>{total: int, lista: ienumerable<objeto>}</returns>
        //Task<dynamic> Buscar(Filtro<TEntidade> filtro);

        /// <summary>
        ///   Verifica se um objeto existe na base a partir da expressão.
        ///   Utiliza o .Any() do IQueryable.
        /// </summary>
        /// <param name="expressao"></param>
        /// <returns></returns>
        Task<bool> Existe(Expression<Func<TEntidade, bool>> expressao);

        IQueryable<TEntidade> Listar();
        IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> expressao);
        Task<IEnumerable<TEntidade>> ListarAsync(Expression<Func<TEntidade, bool>> expressao);
        Task<IEnumerable<TEntidade>> ListarAsync();
        Task<IEnumerable<TEntidade>> ListarAtivosAsync();
        Task Inserir(TEntidade entidade);
        Task Inserir(TEntidade[] entidades);
        Task Atualizar(TEntidade entidade);
        Task<TEntidade> InserirERecuperar(TEntidade entidade);
        Task<TEntidade> RecuperarPorId(int id);
        Task Excluir(int id);
        bool AtualizarSincrono(TEntidade entidade);
    }
}
