﻿using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Queries.Filters;
using System;
using System.Linq.Expressions;

namespace PreMatriculasParanoa.Domain.Queries
{
    public class UsuarioQuery : IUsuarioQuery
    {
        public Expression<Func<Usuario, bool>> ObterPesquisa(UsuarioFilter filtro)
        {
            if (string.IsNullOrEmpty(filtro?.Search))
                return _ => true;

            return x => x.Nome.Contains(filtro.Search) 
                || x.CpfUsuario == filtro.Search
                || x.Login.ToLower() == filtro.Search.ToLower() 
                || x.Email.ToLower() == filtro.Search.ToLower();
        }

        public Expression<Func<Usuario, object>> ObterOrdenacao(string campo)
        {
            switch (campo)
            {
                default:
                    return x => x.Nome;
            }
        }
    }
}
