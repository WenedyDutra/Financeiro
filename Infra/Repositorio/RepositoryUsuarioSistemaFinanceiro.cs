﻿using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Entities.Entidades;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositoryUsuarioSistemaFinanceiro : RepositoryGenerics<UsuarioSistemaFinanceiro>, IUsuarioSistemaFinanceiro
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositoryUsuarioSistemaFinanceiro()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<IList<UsuarioSistemaFinanceiro>> ListarUsuariosSistema(int IdSistema)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    banco.UsuarioSistemaFinanceiro
                    .Where(s => s.IdSistema == IdSistema).AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<UsuarioSistemaFinanceiro> ObterUsuarioPorEmail(string emailUsuario)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    banco.UsuarioSistemaFinanceiro.AsNoTracking().FirstOrDefaultAsync(x => x.EmailUsuario.Equals(emailUsuario));
            }
        }

        public async Task RemoveUsuarios(List<UsuarioSistemaFinanceiro> usuarios)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                banco.UsuarioSistemaFinanceiro.RemoveRange(usuarios);
                await banco.SaveChangesAsync();
            }
        }
    }
}
