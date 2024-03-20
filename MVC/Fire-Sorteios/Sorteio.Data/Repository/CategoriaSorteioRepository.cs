using AutoMapper;
using Dapper;
using Sorteio.Data.EntityData;
using Sorteio.Data.Repository.Base;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Data.Repository
{
    public class CategoriaSorteioRepository : RepositoryBase<CategoriaSorteio, CategoriaSorteioData>, ICategoriaSorteioRepository
    {
        public CategoriaSorteioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<int> ExcluirCategoriaSorteio(int idCategoriaSorteio)
            => await _dataContext.Connection.ExecuteAsync(@"UPDATE CategoriaSorteio
                                                            SET status = 1
                                                            WHERE id_categoria_sorteio = @idCategoriaSorteio",
                                                            new { idCategoriaSorteio = idCategoriaSorteio });
    }
}
