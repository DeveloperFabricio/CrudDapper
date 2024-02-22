using CrudDapper.Models;
using Dapper;
using System.Data.SqlClient;

namespace CrudDapper.Services.LivroServices
{
    public class LivroService : ILivroInterface
    {
        private readonly IConfiguration _configuration;
        private readonly string getConnection;
        public LivroService(IConfiguration configuration) 
        {
            _configuration = configuration;
            getConnection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Livro>> CreateLivro(Livro livro)
        {
            using(var com = new SqlConnection(getConnection))
            {
                var sql = "insert into Livros (titulo, autor) values (@titulo, @autor)";
                await com.ExecuteAsync(sql, livro);

                return await com.QueryAsync<Livro>("select * from Livros");


            }
        }

        public async Task<IEnumerable<Livro>> DeleteLivro(int livroid)
        {
            using (var com = new SqlConnection(getConnection))
            {
                var sql = "delete from Livros where id = @id";
                await com.ExecuteAsync(sql, new  { Id = livroid });

                return await com.QueryAsync<Livro>("select * from Livros");
            }
        }

        public async Task<IEnumerable<Livro>> GetAllLivros()
        {
            using(var com = new SqlConnection(getConnection))
            {
                var sql = "select * from Livros";
                return await com.QueryAsync<Livro>(sql);
            }
        }

        public async Task<Livro> GetLivroById(int livroId)
        {
            using(var com = new SqlConnection(getConnection))
            {
                var sql = "select * from Livros where id = @id";
                return await com.QueryFirstOrDefaultAsync<Livro>(sql, new {  Id = livroId });
            }
        }

        public async Task<IEnumerable<Livro>> UpdateLivro(Livro livro)
        {
            using(var com = new SqlConnection(getConnection))
            {
                var sql = "update Livros set titulo = @titulo, autor = @autor where id = @id";
                await com.ExecuteAsync(sql, livro);

                return await com.QueryAsync<Livro>("select * from Livros");
            }
        }
    }
}
