using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PwMidiaCaptacaoLeads.Dados.Repositorios.Interfaces;
using PwMidiaCaptacaoLeads.Dominio.Entidades;

namespace PwMidiaCaptacaoLeads.Dados.Repositorios
{
    public abstract class Repositorio<TEntidade> : IRepositorio<TEntidade> where TEntidade : EntidadeBase
    {
        protected Repositorio(PwMidiaLeadsDbContexto contexto)
        {
            Contexto = contexto;
            Consulta = Contexto.Set<TEntidade>();
            Lista = Consulta.AsNoTracking().AsQueryable();
        }


        protected PwMidiaLeadsDbContexto Contexto { get; }
        protected DbSet<TEntidade> Consulta { get; }
        public IQueryable<TEntidade> Lista { get; }

        public async Task<TEntidade> Buscar(int id) =>
            await Consulta.FindAsync(id);

        public async Task<TEntidade> BuscarPorId(int id) =>
            await Consulta.FirstOrDefaultAsync(x => x.Id == id);

        public virtual async Task<TEntidade> Buscar(Guid chave) =>
            await Consulta.Where(x => x.Guid == chave).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntidade>> ObterTodosPaginados(int page, int pageSize)
        {
            return await Consulta
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarTotal()
        {
            return await Consulta.CountAsync();
        }

        public async Task<TEntidade> Buscar(Expression<Func<TEntidade, bool>> expressao) =>
            await Consulta.Where(expressao).FirstOrDefaultAsync();

        public Task<bool> Existe(Expression<Func<TEntidade, bool>> expressao) => Consulta.AnyAsync(expressao);

     

        //caso haja a necessidade, o SaveChanges pode ser movido para outro método, permitindo que haja múltiplas alterações no contexto e salvando tudo em apenas um save.
        public async Task Inserir(TEntidade entidade)
        {

            entidade.Guid = Guid.NewGuid();
            await Consulta.AddAsync(entidade);
            await Contexto.SaveChangesAsync(CancellationToken.None);
        }

        public async Task Inserir(TEntidade[] entidades)
        {
            foreach (var item in entidades)
            {
                item.Guid = Guid.NewGuid();
            }

            Consulta.AddRange(entidades);
            await Contexto.SaveChangesAsync(CancellationToken.None);
        }

        public async Task Atualizar(TEntidade entidade)
        {
            Consulta.Update(entidade);
            await Contexto.SaveChangesAsync(CancellationToken.None);
        }

        public bool AtualizarSincrono(TEntidade entidade)
        {
            Consulta.Update(entidade);
            return (Contexto.SaveChanges()) > 0;
        }

        public IQueryable<TEntidade> Listar() =>
                        Contexto.Set<TEntidade>().AsNoTracking();

        public IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> expressao) =>
                Contexto.Set<TEntidade>().Where(expressao).AsNoTracking();

        public async Task<IEnumerable<TEntidade>> ListarAsync() =>
            await Lista.ToListAsync();

        public async Task<IEnumerable<TEntidade>> ListarAtivosAsync() =>
           await Lista
            .Where(x => x.Ativo).ToListAsync();

        public async Task<IEnumerable<TEntidade>> ListarAsync(Expression<Func<TEntidade, bool>> expressao) =>
            await Lista.Where(expressao).ToListAsync();

        public async Task<TEntidade> InserirERecuperar(TEntidade entidade)
        {
            entidade.Guid = Guid.NewGuid();
            await Consulta.AddAsync(entidade);
            await Contexto.SaveChangesAsync(CancellationToken.None);

            // Get the inserted entity's identifier
            int id = entidade.Id; // Assuming 'Id' is the property that holds the identifier

            // Retrieve the entity using the identifier
            TEntidade insertedEntity = await RecuperarPorId(id);
            return insertedEntity;
        }

        public async Task<TEntidade> RecuperarPorId(int id)
        {
            return await Contexto.Set<TEntidade>().FindAsync(id);
        }

        public async Task Excluir(int id)
        {
            var registro = await Consulta.FindAsync(id);
            Consulta.Remove(registro!);
            await Contexto.SaveChangesAsync(CancellationToken.None);
        }
    }
}
